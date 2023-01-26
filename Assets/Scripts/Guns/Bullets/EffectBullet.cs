public class EffectBullet : IBullet
{
    private IBullet _bullet;
    private Effect _effect;

    public EffectBullet(IBullet bullet, Effect effect)
    {
        _bullet = bullet;
        _effect = effect;
    }

    public void DealDamage(IApplyableDamage target, float damage)
    {
        _bullet.DealDamage(target, damage);

        if(target is IApplyableEffect effectTarget)
        {
            _effect.Apply(effectTarget);
        }
    }
}
