using System;
using System.Collections;
using UnityEngine;

namespace Enemies
{
    public class ProceduralEnemyMovement : MonoBehaviour
    {
        private Enemy _enemy;
        private Animator _enemyAnimator;
        
        private readonly int WalkingAnimaitonId = Animator.StringToHash("Walking");
        private readonly int JumpingTriggerId = Animator.StringToHash("Jump");

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
            _enemyAnimator = _enemy.GetAnimator();
            
            if(_enemyAnimator)
                _enemyAnimator.SetBool(WalkingAnimaitonId, true);
            
            StartCoroutine(MoveToLandingPoint(jumpPoint, landingPoint));
        }

        private IEnumerator MoveToLandingPoint(Transform jumpPoint, Transform landingPoint)
        {
            yield return MoveToJumpPoint(jumpPoint);
            
            while (Mathf.Abs(_enemy.transform.position.y - landingPoint.transform.position.y) >= .05f)
            {
                var direction = landingPoint.transform.position - _enemy.transform.position;
                RotateToTarget(_enemy.transform, landingPoint, 5);
                _enemy.transform.position += direction.normalized * (Time.deltaTime * _enemy.Speed);
                yield return null;
            }
            FinishRoute();
        }
        
        private IEnumerator MoveToJumpPoint(Transform jumpPoint)
        {
            while (Vector3.Distance(_enemy.transform.position, jumpPoint.transform.position) >= .5f)
            {
                var direction = jumpPoint.transform.position - _enemy.transform.position;
                RotateToTarget(_enemy.transform, jumpPoint, 5);
                _enemy.transform.position += direction.normalized * (Time.deltaTime * _enemy.Speed);
                yield return null;
            }

            if (_enemyAnimator)
            {
                _enemyAnimator.SetBool(WalkingAnimaitonId, false);
                _enemyAnimator.SetTrigger(JumpingTriggerId);
            }
        }

        private void FinishRoute()
        {
            Jumped?.Invoke();
            
            if(_enemyAnimator)
                _enemyAnimator.SetBool(WalkingAnimaitonId, false);
            
            _enemy.SetNavmeshAgent(true);
        }
        
        private void RotateToTarget(Transform origin, Transform target, float rotationSpeed)
        {
            Vector3 direction = (target.position - origin.position).normalized;
            direction.y = 0;
            Quaternion rotation = Quaternion.LookRotation(direction);
			
            origin.transform.rotation =
                Quaternion.Lerp(origin.rotation, rotation, rotationSpeed * Time.deltaTime);
        }
    }
}