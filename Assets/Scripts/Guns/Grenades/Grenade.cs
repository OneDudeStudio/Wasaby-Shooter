using UnityEngine;

public abstract class Grenade : MonoBehaviour
{
    private Explosion _explosion;

    private void Start() => _explosion = GetComponent<Explosion>();

    private void OnCollisionEnter(Collision collision)
    {
        Stabilization(collision.contacts[0]);
        _explosion.Explode();
        ExplosionPostEffects(IsCeilingHit(collision.contacts[0].normal, transform.position), transform.position);
    }

    private void Stabilization(ContactPoint contactPoint) => transform.position = contactPoint.point + contactPoint.normal * .1f;

    public virtual void ExplosionPostEffects(bool isCeilingHit, Vector3 point)
    {
    }

    private bool IsCeilingHit(Vector3 noraml, Vector3 position)
    {
        if (noraml.y < -.7f)
            return true;
        if (Physics.Raycast(position, Vector3.up, 1.5f))
            return true;
        return false;
    }
}
