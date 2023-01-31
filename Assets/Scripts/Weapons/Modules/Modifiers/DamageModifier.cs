public class DamageModifier : GunModifier
{
    private readonly float _damagePrecentModifier;
    public DamageModifier(float precent)
    {
        _damagePrecentModifier = precent;
    }
    public override void Modify(Gun gun) => gun.Damage *= SupportFunctions.PrecentToFloat(_damagePrecentModifier);
    public override void Reset(Gun gun) => gun.Damage = gun.Damage;
}