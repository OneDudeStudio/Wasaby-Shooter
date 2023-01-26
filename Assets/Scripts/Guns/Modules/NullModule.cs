using UnityEngine.SocialPlatforms;

public class NullModule : GunModule
{
    public NullModule(Gun gun) : base(gun)
    {
        gun.Damage = gun.ThisGunConfig._defaultDamage;
        gun.Ammo = gun.ThisGunConfig._defaultMaxAmmo;
        gun.Interval = gun.ThisGunConfig._defaultIntervalTime;
        gun.Range = gun.ThisGunConfig._defaultRange;
        gun.Rec.RecoilValue = gun.ThisGunConfig._defaultRecoil;
        gun.Rec.ReturnSpeedValue = gun.ThisGunConfig._defaultReturnSpeed;
        gun.Rec.SnappinesValue = gun.ThisGunConfig._defaultSnappines;
        gun.Rec.PositionRecoilValue = gun.ThisGunConfig._defaultPositionRecoil;
        gun.Rec.PositionReturnSpeedValue = gun.ThisGunConfig._defaultPositionReturnSpeed;
        gun.Rec.PositionSnappinesValue = gun.ThisGunConfig._defaultPositionSnappines;
    }
}
