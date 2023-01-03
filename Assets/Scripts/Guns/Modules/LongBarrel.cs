using System.Collections.Generic;

public class LongBarrel : SupportModule
{
    private readonly Dictionary<Gun.GunType, float> _addedRangePrecent = new Dictionary<Gun.GunType, float>()
    {
        { Gun.GunType.Rifle, 50},
        { Gun.GunType.Shotgun, 20}
    };

    private readonly Dictionary<Gun.GunType, float> _addedDamagePrecent = new Dictionary<Gun.GunType, float>()
    {
        { Gun.GunType.Rifle, 10},
        { Gun.GunType.Shotgun, 5}
    };
    private readonly Dictionary<Gun.GunType, float> _addedIntervalPrecent = new Dictionary<Gun.GunType, float>()
    {
        { Gun.GunType.Rifle, 10},
        { Gun.GunType.Shotgun, 20}
    };

    public LongBarrel(Gun gun, Gun.GunType type, Recoil recoil) : base(gun, type, recoil)
    {
    }

    public override void ModifyRange(float range) => _gun.SetRange(range * SupportFunctions.PrecentToFloat(_addedRangePrecent[_gunType]));
    public override void ModifyDamage(float damage) => _gun.SetDamage(damage * SupportFunctions.PrecentToFloat(_addedDamagePrecent[_gunType]));
    public override void ModifyInterval(float interval) => _gun.SetInterval(interval * SupportFunctions.PrecentToFloat(_addedIntervalPrecent[_gunType]));
    

}
