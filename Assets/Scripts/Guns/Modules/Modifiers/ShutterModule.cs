public class ShutterModule : GunModule
{
    public ShutterModule(Gun gun) : base(gun)
    {
        _gunModifiers.Add(new DamageModifier(gun.ThisGunConfig.Range.DamagePrecentMultiplier));
        _gunModifiers.Add(new RangeModifier(gun.ThisGunConfig.Range.AdditionalRange));
    }
}
