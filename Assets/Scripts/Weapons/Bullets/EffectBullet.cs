public class EffectBullet<T> : IBullet where T : Effect
{
    private IBullet _bullet;
    

    public EffectBullet(int additioanlDamagePrecent)
    {
        _bullet = new Bullet(additioanlDamagePrecent);
    }

    public EffectBullet(IBullet bullet)
    {
        _bullet = bullet;
    }

    public void DealDamage(IApplyableDamage target, float damage)
    {
        _bullet.DealDamage(target, damage);

        if(target is IApplyableEffect effectTarget)
        {
            effectTarget.StartEffect<T>();
        }
    }
}
