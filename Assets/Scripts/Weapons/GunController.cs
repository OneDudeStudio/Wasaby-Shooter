using System;
using UnityEngine;

public class GunController : MonoBehaviour
{
    private Gun _currentGun;
    private GrenadeLauncher _currentLauncher;

    [SerializeField] private BulletsConfig _bulletsConfig;

    private IBullet[] _bullets = new IBullet[4];
    private Type[] _moduleTypes = new Type[] { typeof(NullModule), typeof(ExtendedMag), typeof(BarrelModule), typeof(ShutterModule) };
    private Type[] _grenadeTypes = new Type[] { null, typeof(DefaultExplosionGrenade), typeof(PoisonGrenade), typeof(FlashGrenade) };

    [SerializeField] private Gun _rifle;
    [SerializeField] private Gun _shotgun;



    private void Start()
    {
        _bullets[0] = new Bullet(_bulletsConfig.Default.AdditionalDamagePrecent);
        _bullets[1] = new EffectBullet<Burning>(_bulletsConfig.Fire.AdditionalDamagePrecent);
        _bullets[2] = new EffectBullet<Freeze>(_bulletsConfig.Freeze.AdditionalDamagePrecent);
        _bullets[3] = new EffectBullet<Electricity>(_bulletsConfig.Electric.AdditionalDamagePrecent);

        SetGun(_shotgun);
    }

    public void SetGun(Gun newGun)
    {
        if (_currentGun != null)
        {
            _currentGun.gameObject.SetActive(false);
        }
        _currentGun = newGun;
        _currentGun.gameObject.SetActive(true);
        _currentLauncher = _currentGun.GetComponent<GrenadeLauncher>();
        
    }


    public void SetModule(int number) => _currentGun.SetModule(_moduleTypes[number]);

    public void SetBullet(int number) => _currentGun.SetBullet(_bullets[number]);

    public void SetGrenade(int number) => _currentLauncher.SetGrenade(_grenadeTypes[number]);

    public void TryReloadGun() => _currentGun.TryReload();
    public void TryShoot() => _currentGun.TryShoot();
    public void TryShootGrenade() => _currentLauncher.TryShootGrenade();
}
