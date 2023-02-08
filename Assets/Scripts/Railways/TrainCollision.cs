using UnityEngine;
using Random = UnityEngine.Random;

public class TrainCollision : MonoBehaviour
{
    [SerializeField] private float _pushForce = 5;
    [SerializeField] private float _pushHeight = 5;
    [SerializeField] private float _damage = 5;
    [SerializeField] private AudioSource _soundForCollision;

    private Ragdoll _otherObjectRagdoll;

    private void OnCollisionEnter(UnityEngine.Collision collision)
    {
        _otherObjectRagdoll = collision.gameObject.GetComponent<Ragdoll>();

        PushObject(collision);
        PlaySound();
        TakeDamage(collision.gameObject, _damage);
    }

    private void PlaySound()
    {
        _soundForCollision.pitch = Random.Range(0.9f, 1.1f);
        _soundForCollision.Play();
    }

    private void PushObject(Collision collision)
    {
        _otherObjectRagdoll.ActivateRagdoll();

        var direction = GetPushDirection(collision);

        _otherObjectRagdoll.ApplyForce(new Vector3(direction.x * _pushForce, direction.y, direction.z * _pushForce));
    }

    private Vector3 GetPushDirection(Collision collision)
    {
        Vector3 vectorFromCollisionPointToCenter = new Vector3(transform.position.x - collision.contacts[0].point.x,
            0,
            transform.position.z - collision.contacts[0].point.z);

        Vector3 direction;
        if (Vector3.Dot(vectorFromCollisionPointToCenter, transform.right) <= 0)
        {
            direction = Quaternion.Euler(0, 45, 0) * transform.forward;
        }
        else
        {
            direction = Quaternion.Euler(0, -45, 0) * transform.forward;
        }

        direction.y = _pushHeight;

        return direction;
    }

    private void TakeDamage(GameObject damagedObject, float amount)
    {
        IApplyableDamage iApplyableDamage = damagedObject.GetComponent<IApplyableDamage>();
        if (iApplyableDamage != null) iApplyableDamage.TryApplyDamage(amount);
    }
}