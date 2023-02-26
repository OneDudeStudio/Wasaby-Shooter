public class Bullet : IDamageDealer
{
    private float _damageMultiplyer;

    public Bullet(int additioanlDamagePrecent)
    {
        _damageMultiplyer = SupportFunctions.PrecentToFloat(additioanlDamagePrecent);
    }
   
    public void DealDamage(IApplyableDamage target, float damage)
    {
        target.TryApplyDamage(_damageMultiplyer * damage);
    }
}
