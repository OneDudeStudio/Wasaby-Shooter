public class DefaultBullet : Bullet
{
    public DefaultBullet()
    {
        _additionalDamagePrecent = 30;
    }
    public override void ShootBullet(IApplyableDamage target)
    {
    }
}
