public abstract class EffectBullet : Bullet
{
    public EffectBullet()
    {
        _additionalDamagePrecent = 0;
    }
    public override void ShootBullet(IApplyableDamage target)
    {
        if(target is IApplyableEffect effectTarget)
            ApplyBulletEffect(effectTarget);
    }
    public abstract void ApplyBulletEffect(IApplyableEffect effectTarget);
}
