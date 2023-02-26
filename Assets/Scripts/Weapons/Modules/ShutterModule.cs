public class ShutterModule : GunModule
{
    public ShutterModule(Gun gun) : base(gun)
    {
        _gunModifiers.Add(new DamageModifier(gun.Config.Damage.DamagePrecentMultiplier));
        _gunModifiers.Add(new RangeModifier(gun.Config.Damage.AdditionalRange));
    }
}
