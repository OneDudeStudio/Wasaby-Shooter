public class ExtendedMag : GunModule
{
    public ExtendedMag(Gun gun) : base(gun)
    {
        _gunModifiers.Add(new AmmoModifier(gun.Config.Mag.AdditioanalAmmo));
    }
}
