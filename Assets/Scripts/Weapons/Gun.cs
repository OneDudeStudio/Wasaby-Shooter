using System.Collections;
using UnityEngine;
using System;

public abstract class Gun : MonoBehaviour
{
    [Header("Gun Keys")]
    [SerializeField] private KeyCode _shootKey;

    [Space]
    [Header("Need Refactor")]
    [SerializeField] protected ParticleSystem _shootParticles;
    [SerializeField] protected Recoil _recoil;
    [SerializeField] protected GunSoundController _sound;
    [SerializeField] protected GameObject _hitParticles;
    public GunConfig ThisGunConfig;

    private float _damage;
    private int _maxAmmo;
    private float _intervalTime;
    protected float _range;

    private int _currentAmmo;
    protected bool _isCanShoot = true;


    protected Camera _playerCamera;
    protected BulletHolesPool _holePool;
    protected IBullet _bullet;
    private ModuleManager _moduleManager;

    public float Damage
    {
        get => ThisGunConfig._defaultDamage;
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
        get => ThisGunConfig._defaultMaxAmmo;
        set
        {
            if (value > 0)
            {
                _maxAmmo = value;
            }

        }
    }
    public float Range
    {
        get => ThisGunConfig._defaultRange;
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
        get => ThisGunConfig._defaultIntervalTime;
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
        _currentAmmo = ThisGunConfig._defaultMaxAmmo;
        
        
        _bullet = new Bullet(0);
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
            _shootParticles.Play();
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

    protected float CalculateDamage(float len) => Mathf.Clamp01(ThisGunConfig._damageByDistance.Evaluate(len / _range)) * _damage;

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
    public void SetBullet(IBullet bullet) => _bullet = bullet;
}