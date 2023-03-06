using Enemies;
using UnityEngine;
using UnityEngine.Serialization;

namespace Railways
{
    public class TrainCollision : MonoBehaviour
    {
        [SerializeField] private float _pushForce = 5;
        [SerializeField] private float _pushHeight = 5;
        [SerializeField] private float _pushDamage = 5;

        private float _pushTime = 0.5f;


        private void OnTriggerEnter(Collider other)
        {
            if (TryGetComponent(out Move move) && move.enabled)
            {
                var direction = GetPushDirection(other) * _pushForce;
                _pushTime = direction.magnitude / move.Speed;
                Debug.Log(_pushTime);

                if (other.gameObject.TryGetComponent<Ragdoll>(out var player))
                {
                    StartCoroutine(VictimPusher.Push(player.transform, direction, _pushTime));
                    player.PlaySound();
                }

                else if (other.gameObject.TryGetComponent<Enemy>(out var enemy))
                {
                    enemy.ApplyPush(direction, _pushTime);
                }

                TakeDamage(other.gameObject, _pushDamage);
            }
        }

        private void PushObjectByPhysics(Ragdoll ragdoll, Vector3 direction)
        {
            ragdoll.ActivateRagdoll();

            ragdoll.ApplyForce(new Vector3(direction.x * _pushForce, direction.y,
                direction.z * _pushForce));
        }

        private Vector3 GetPushDirection(Collision collision)
        {
            Vector3 vectorFromCollisionPointToCenter = new Vector3(transform.position.x - collision.contacts[0].point.x,
                0,
                transform.position.z - collision.contacts[0].point.z);

            Vector3 direction = GetRotation(vectorFromCollisionPointToCenter);

            direction.y = _pushHeight;

            return direction;
        }

        private Vector3 GetPushDirection(Collider collider)
        {
            Vector3 vectorFromCollisionPointToCenter = new Vector3(transform.position.x - collider.transform.position.x,
                0,
                transform.position.z - collider.transform.position.z);

            Vector3 direction = GetRotation(vectorFromCollisionPointToCenter);

            direction.y = _pushHeight;

            return direction;
        }

        private Vector3 GetRotation(Vector3 vector)
        {
            Vector3 vectorWithRotation;
            if (Vector3.Dot(vector, transform.right) <= 0)
            {
                vectorWithRotation = Quaternion.Euler(0, 45, 0) * transform.forward;
            }
            else
            {
                vectorWithRotation = Quaternion.Euler(0, -45, 0) * transform.forward;
            }

            return vectorWithRotation;
        }

        private void TakeDamage(GameObject damagedObject, float amount)
        {
            IApplyableDamage iApplyableDamage = damagedObject.GetComponent<IApplyableDamage>();
            if (iApplyableDamage != null) iApplyableDamage.TryApplyDamage(amount);
        }
    }
}