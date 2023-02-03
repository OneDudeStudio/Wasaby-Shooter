using UnityEngine;

public abstract class Grenade : MonoBehaviour
{
    private Explosion _explosion;
    [SerializeField] protected GrenadesConfig _config;
    protected IApplyableDamage[] _victims;
    protected Collision _collision;

    private void Start()
    {
        _explosion = GetComponent<Explosion>();
        _explosion.SetConfig(GetConfig());
    }

    private void OnCollisionEnter(Collision collision)
    {
        Stabilization(collision.contacts[0]);

        _victims = _explosion.Explode();
        _collision = collision;
        ExplosionPostEffects();
    }

    private void Stabilization(ContactPoint contactPoint) => transform.position = contactPoint.point + contactPoint.normal * .2f;

    public virtual void ExplosionPostEffects()
    {
    }
    protected abstract GrenadeConfig GetConfig(); 
}
