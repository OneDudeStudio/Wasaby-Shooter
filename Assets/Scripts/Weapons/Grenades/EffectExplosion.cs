public class EffectExplosionDamageDealer<T> : IDamageDealer where T : Effect
{
    private IDamageDealer _explosion;

    public EffectExplosionDamageDealer()
    {
        _explosion = new ExplosionDamageDealer();
    }

    public void DealDamage(IApplyableDamage target, float damage)
    {
        _explosion.DealDamage(target, damage);
        if (target is IApplyableEffect effectTarget)
        {
            effectTarget.StartEffect<T>();
        }
    }
}