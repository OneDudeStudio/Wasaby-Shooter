using System.Collections.Generic;

public class ExtendedMag : SupportModule
{
    private readonly Dictionary<Gun.GunType, int> _addedAmmo = new Dictionary<Gun.GunType, int>()
    {
        { Gun.GunType.Rifle, 10},
        { Gun.GunType.Shotgun, 5}
    };

    public ExtendedMag(Gun gun, Gun.GunType type, Recoil recoil) : base(gun, type, recoil)
    {
    }

    public override void ModifyMag(int maxAmmo) => _gun.SetAmmo(maxAmmo + _addedAmmo[_gunType]);
}
