using System;
using System.Collections;
using UnityEngine;

public abstract class Gun : MonoBehaviour
{
    [Space]
    [Header("Need Refactor")]
    [SerializeField] protected Recoil _recoil;
    [SerializeField] protected GunSoundController _sound;
    [SerializeField] protected GunVFXController _gunVFX;
    [SerializeField] protected GameObject _hitParticles;
    [SerializeField] protected Transform _shootingPoint;
    [SerializeField] protected GunConfig _gunConfig;

    public GunConfig Config => _gunConfig;

    private float _damage;
    private int _maxAmmo;
    private float _intervalTime;
    protected float _range;

    private int _currentAmmo;
    protected bool _isCanShoot = true;

    protected Camera _playerCamera;
    protected BulletHolesPool _holePool;
    protected IDamageDealer _bullet;
    private ModuleManager _moduleManager;

    public float Damage
    {
        get => _gunConfig.DefaultDamage;
        set
        {
            if (value >= 0)
            {
                _damage = value;
            }
        }
    }
    public int Ammo
    {
        get => _gunConfig.DefaultMaxAmmo;
        set
        {
            if (value > 0)
            {
                _maxAmmo = value;
                _currentAmmo = value;
            }

        }
    }
    public float Range
    {
        get => _gunConfig.DefaultRange;
        set
        {
            if (value > 0)
            {
                _range = value;
            }
        }
    }
    public float Interval
    {
        get => _gunConfig.DefaultIntervalTime;
        set
        {
            if (value >= 0)
            {
                _intervalTime = value;
            }
        }
    }

    public Recoil Rec => _recoil;

    protected void Start()
    {
        _playerCamera = Camera.main;
        _holePool = FindObjectOfType<BulletHolesPool>();
        _moduleManager = new ModuleManager(this);

        SetStatsFromConfig();

        SetBullet(new Bullet(0));
        //SetBullet(new EffectBullet<Freeze>(0));
    }

    protected virtual void SetStatsFromConfig()
    {
        SetModule(typeof(NullModule));
    }

    public void TryReload()
    {
        if (_currentAmmo != _maxAmmo)
        {
            Reload();
        }
    }

    public void TryShoot()
    {
        if (!_isCanShoot)
        {
            return;
        }

        if (TryDecreaseAmmo())
        {
            _sound.PlayShootSound();
            _gunVFX.ShowShootParticles();
            _recoil.RecoilFire();
            Shoot();
        }
        else
        {
            _sound.PlayOutOfAmmoSound();
        }

        StartCoroutine(IntervalBetweenShoots());

    }

    private bool IsOutOfAmmo() => _currentAmmo-- <= 0;

    protected bool TryDecreaseAmmo() => !IsOutOfAmmo();

    protected abstract void Shoot();

    private void Reload()
    {
        _isCanShoot = false;
        //StartReloadAnim();
        _currentAmmo = _maxAmmo;
        _isCanShoot = true;
    }

    protected float CalculateDamage(float len) => Mathf.Clamp01(_gunConfig.DamageByDistance.Evaluate(len / _range)) * _damage;

    private IEnumerator IntervalBetweenShoots()
    {
        _isCanShoot = false;
        yield return new WaitForSeconds(_intervalTime);
        _isCanShoot = true;
    }

    public void SetModule(Type type)
    {
        _moduleManager.SetModule(type);
        _currentAmmo = _maxAmmo;
    }
    public void SetBullet(IDamageDealer bullet)
    {
        _bullet = bullet;
        _gunVFX.SetBulletVFX(bullet.GetType());
    }


    public void LockShooting() => _isCanShoot = false;
    public void UnlockShooting() => _isCanShoot = true;

}