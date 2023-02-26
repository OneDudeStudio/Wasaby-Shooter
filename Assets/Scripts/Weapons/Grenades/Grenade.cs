using UnityEngine;
public abstract class Grenade : MonoBehaviour
{
    protected Explosion _explosion;
    
    private void Start()
    {
        _explosion = GetComponent<Explosion>();
        SetExplosionConfig();
        SetExplosionDamageDealer();
    }

    private void OnCollisionEnter(Collision collision)
    {
        Stabilization(collision.contacts[0]);
        ExplodeGrenade(collision);
    }

    private void Stabilization(ContactPoint contactPoint) => transform.position = contactPoint.point + contactPoint.normal * .2f;

    protected virtual void SetExplosionDamageDealer()
    {
        _explosion.SetDamageDealer(new ExplosionDamageDealer());
    }

    private void SetExplosionConfig()
    {
        _explosion.SetConfig(GetConfig());
    }

    protected virtual void ExplodeGrenade(Collision collision)
    {
        _explosion.Explode();
    }

    protected abstract ExplosionConfig GetConfig();

    protected ExplosionItemsConfig LoadConfig() => FindObjectOfType<ConfigsLoader>().RootConfig.ExplosionItemsConfig;
}