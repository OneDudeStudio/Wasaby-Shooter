public class ExplosionDamageDealer : IDamageDealer
{
    public void DealDamage(IApplyableDamage target, float damage)
    {
        target.TryApplyDamage(damage);
    }
}
