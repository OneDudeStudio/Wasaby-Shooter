public class EffectBullet<T> : IDamageDealer where T : Effect
{
    private IDamageDealer _bullet;    

    public EffectBullet(int additioanlDamagePrecent)
    {
        _bullet = new Bullet(additioanlDamagePrecent);
    }

    public EffectBullet(IDamageDealer bullet)
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
