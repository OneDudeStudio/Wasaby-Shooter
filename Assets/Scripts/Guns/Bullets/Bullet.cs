public abstract class Bullet
{
    
    protected float _additionalDamagePrecent = 0;
   // public abstract void ShootBullet(IApplyableDamage target);
    public float GetAdditionalDamage() => SupportFunctions.PrecentToFloat(_additionalDamagePrecent);
}
