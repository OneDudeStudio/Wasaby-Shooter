public class BarrelModule : GunModule
{
    public BarrelModule(Gun gun) : base(gun)
    {
        _gunModifiers.Add(new DamageModifier(gun.Config.Range.DamagePrecentMultiplier));
        _gunModifiers.Add(new RangeModifier(gun.Config.Range.AdditionalRange));
    }
}
