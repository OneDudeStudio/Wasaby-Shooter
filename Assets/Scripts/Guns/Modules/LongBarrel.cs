using System.Collections.Generic;
using UnityEngine;

public class LongBarrel : SupportModule
{
    private readonly Dictionary<Gun.GunType, float> _addedRange = new Dictionary<Gun.GunType, float>()
    {
        { Gun.GunType.Rifle, 100},
        { Gun.GunType.Shotgun, 15}
    };

    private readonly Dictionary<Gun.GunType, float> _addedDamage = new Dictionary<Gun.GunType, float>()
    {
        { Gun.GunType.Rifle, .5f},
        { Gun.GunType.Shotgun, .1f}
    };
    private readonly Dictionary<Gun.GunType, float> _addedInterval = new Dictionary<Gun.GunType, float>()
    {
        { Gun.GunType.Rifle, .05f},
        { Gun.GunType.Shotgun, .3f}
    };

    public LongBarrel(Gun gun, Gun.GunType type) : base(gun, type)
    {
    }

    public override void ModifyRange(float range) => _gun.SetRange(range + _addedRange[_gunType]);
    public override void ModifyDamage(float damage) => _gun.SetDamage(damage + _addedDamage[_gunType]);
    public override void ModifyInterval(float interval) => _gun.SetInterval(interval + _addedInterval[_gunType]);
    

}
