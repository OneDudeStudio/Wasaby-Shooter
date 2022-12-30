using UnityEngine;

public abstract class SupportModule
{
    protected Gun _gun;
    protected Gun.GunType _gunType;
    public SupportModule(Gun gun, Gun.GunType type)
    {
        _gun = gun;
        _gunType = type;
    }

    public void CalculateModuleCharacteristics()
    {
        ModifyDamage(_gun.GetDamage());
        ModifyMag(_gun.GetAmmo());
        ModifyRange(_gun.GetRange());        
        ModifyInterval(_gun.GetInterval());
    }

    public virtual void ModifyDamage(float damage) => _gun.SetDamage(damage);
    public virtual void ModifyMag(int maxAmmo) => _gun.SetAmmo(maxAmmo);
    public virtual void ModifyRange(float range) => _gun.SetRange(range);
    public virtual void ModifyInterval(float interval) => _gun.SetInterval(interval); 
}