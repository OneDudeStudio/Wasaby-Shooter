
public class AmmoModifier : GunModifier
{
    private readonly int _additionalAmmo;
    public AmmoModifier(int additional)
    {
        _additionalAmmo = additional;
    }
    public override void Modify(Gun gun) => gun.Ammo += _additionalAmmo;
    public override void Reset(Gun gun) => gun.Ammo = gun.Ammo;
}