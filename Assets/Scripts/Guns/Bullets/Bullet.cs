public interface IBullet
{
    public void DealDamage(IApplyableDamage target, float damage);
}

public class Bullet : IBullet
{
    private float _additionalDamagePrecent = 0f;

    public void SetAdditionalDamagePrecent(int precent) => _additionalDamagePrecent = precent;

    public void DealDamage(IApplyableDamage target, float damage)
    {
        target.TryApplyDamage(SupportFunctions.PrecentToFloat(_additionalDamagePrecent) * damage);
    }
}
