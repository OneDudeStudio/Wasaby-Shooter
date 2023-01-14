using UnityEngine;

public abstract class SupportModule
{
    protected Gun _gun;
    protected Gun.GunType _gunType;
    protected Recoil _recoil;
   // protected GunModulesConfig _thisGunModuleConfig;
    public SupportModule(Gun gun, Gun.GunType type, Recoil recoil)
    {
        _gun = gun;
        _gunType = type;
        _recoil = recoil;
        //_thisGunModuleConfig = _gun.ThisGunModuleConfig;
    }

    public void CalculateModuleCharacteristics()
    {
        ModifyGun();
        ModifyGunRecoil();
    }

    private void ModifyGun()
    {
        ModifyDamage(_gun.GetDamage());
        ModifyMag(_gun.GetAmmo());
        ModifyRange(_gun.GetRange());
        ModifyInterval(_gun.GetInterval());
    }
    private void ModifyGunRecoil()
    {
        ModifyRecoil(_recoil.GetRecoil());
        ModifyReturnSpeed(_recoil.GetReturnSpeed());
        ModifySnappines(_recoil.GetSnappines());
    }

    public virtual void ModifyDamage(float damage) => _gun.SetDamage(damage);
    public virtual void ModifyMag(int maxAmmo) => _gun.SetAmmo(maxAmmo);
    public virtual void ModifyRange(float range) => _gun.SetRange(range);
    public virtual void ModifyInterval(float interval) => _gun.SetInterval(interval);
    public virtual void ModifyRecoil(Vector3 recoil) => _recoil.SetRecoil(recoil);
    public virtual void ModifyReturnSpeed(float speed) => _recoil.SetReturnSpeed(speed);
    public virtual void ModifySnappines(float snappines) => _recoil.SetSnappines(snappines);
}