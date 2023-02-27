public class NullModule : GunModule
{
    public NullModule(Gun gun) : base(gun)
    {
        gun.Damage = gun.Config.DefaultDamage;
        gun.Ammo = gun.Config.DefaultMaxAmmo;
        gun.Interval = gun.Config.DefaultIntervalTime;
        gun.Range = gun.Config.DefaultRange;
        gun.ReloadDuration = gun.Config.ReloadDuration;

        gun.Rec.RecoilValue = gun.Config.DefaultRecoil;
        gun.Rec.ReturnSpeedValue = gun.Config.DefaultReturnSpeed;
        gun.Rec.SnappinesValue = gun.Config.DefaultSnappines;
        gun.Rec.PositionRecoilValue = gun.Config.DefaultPositionRecoil;
        gun.Rec.PositionReturnSpeedValue = gun.Config.DefaultPositionReturnSpeed;
        gun.Rec.PositionSnappinesValue = gun.Config.DefaultPositionSnappines;
    }
}
