using System;
using System.Collections;
using UnityEngine;

namespace Enemies
{
    public class ProceduralEnemyMovement : MonoBehaviour
    {
        private Enemy _enemy;
        private Animator _enemyAnimator;

        private Transform _jumpPoint;
        private Transform _landingPoint;
        
        private readonly int WalkingAnimaitonId = Animator.StringToHash("Walking");
        
        private const float RotationSpeed = 5f;

        public event Action Jumped;
        
        private void Awake()
        {
            _enemy = GetComponent<Enemy>();
        }

        private void OnEnable()
        {
            _enemy.SetNavmeshAgent(false);
        }

        public void StartRoute(Transform jumpPoint, Transform landingPoint)
        {
            _jumpPoint = jumpPoint;
            _landingPoint = landingPoint;
            
            _enemyAnimator = _enemy.GetAnimator();

            if (_enemyAnimator)
            {
                 _enemyAnimator.SetBool(WalkingAnimaitonId, true);
                 StartCoroutine(MoveToJumpPoint(_jumpPoint));
            }
            else
            {
                StartCoroutine(RouteWithoutAnimation(_jumpPoint, _landingPoint));
            } 
        }

        private IEnumerator RouteWithoutAnimation(Transform jumpPoint, Transform landingPoint)
        {
            yield return MoveToJumpPoint(jumpPoint);
            yield return MoveToLandingPoint(landingPoint);
   
            FinishRoute();
        }

        private IEnumerator MoveToJumpPoint(Transform jumpPoint)
        {
            const float comparisonDistance = .5f;
            
            bool Condition() => 
                Vector3.Distance(_enemy.transform.position, jumpPoint.transform.position) >= comparisonDistance;

            yield return MoveToPoint(jumpPoint, Condition);

            if (_enemyAnimator)
            {
                int jumpingAnimationId = Animator.StringToHash("Jumping");
                
                _enemyAnimator.SetBool(WalkingAnimaitonId, false);

                if (_enemyAnimator.HasState(0, jumpingAnimationId))
                    _enemyAnimator.Play(jumpingAnimationId);
            }
        }
        
        private IEnumerator MoveToLandingPoint(Transform landingPoint)
        {
            const float comparisonDistance = .05f;
            
            bool Condition() => 
                Mathf.Abs(_enemy.transform.position.y - landingPoint.transform.position.y) >= comparisonDistance;
            
            yield return MoveToPoint(landingPoint, Condition);
            FinishRoute();
        }

        private void FinishRoute()
        {
            if(_enemyAnimator)
                _enemyAnimator.SetBool(WalkingAnimaitonId, false);
            
            _enemy.SetNavmeshAgent(true);
            enabled = false;
        }

        private IEnumerator MoveToPoint(Transform point, Func<bool> condition)
        {
            while (condition())
            {
                var direction = point.transform.position - _enemy.transform.position;
                RotateToTarget(_enemy.transform, point, RotationSpeed);
                _enemy.transform.position += direction.normalized * (Time.deltaTime * _enemy.Speed);
                yield return null;
            }
        }
        
        private void RotateToTarget(Transform origin, Transform target, float rotationSpeed)
        {
            Vector3 direction = (target.position - origin.position).normalized;
            direction.y = 0;
            Quaternion rotation = Quaternion.LookRotation(direction);
			
            origin.transform.rotation =
                Quaternion.Lerp(origin.rotation, rotation, rotationSpeed * Time.deltaTime);
        }
        
        private void Jump()
        {
            StartCoroutine(MoveToLandingPoint(_landingPoint));
        }

        private void OnDisable()
        {
            Jumped?.Invoke();
        }
    }
}