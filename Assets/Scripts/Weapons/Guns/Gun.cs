using System;
using System.Collections;
using TMPro;
using UnityEngine;

public abstract class Gun : MonoBehaviour
{
    [Space]
    [Header("Need Refactor")]
    [SerializeField] protected Recoil _recoil;
    [SerializeField] protected GunSoundController _sound;
    [SerializeField] protected GunVFXController _gunVFX;
    [SerializeField] protected Transform _shootingPoint;
    [SerializeField] protected GunConfig _gunConfig;

    /// <summary>
    /// !!!! kostil
    /// </summary>
    [SerializeField] private TextMeshProUGUI _reloadCooldown;

    public GunConfig Config => _gunConfig;

    private float _damage;
    private int _maxAmmo;
    private float _intervalTime;
    protected float _range;
    private float _reloadDuration;
    private int _currentAmmo;


    protected bool _isCanShoot = true;
    private bool _isReloading = false;
    private float _lastShotTime = 0;
    private float _reloadStartTime = 0;

    protected Camera _playerCamera;
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
    public float ReloadDuration
    {
        get => _gunConfig.ReloadDuration;
        set
        {
            if (value > 0)
            {
                _reloadDuration = value;
            }
        }
    }

    public Recoil Rec => _recoil;

    protected void Start()
    {
        _playerCamera = Camera.main;
        _moduleManager = new ModuleManager(this);
        SetStatsFromConfig();

        SetBullet(new Bullet(0));
    }

    private void Update()
    {
        if (!_isReloading)
            return;

        _reloadCooldown.text = string.Format("{0:f1}", _reloadDuration + _reloadStartTime - Time.time);
    }


    protected virtual void SetStatsFromConfig()
    {
        SetModule(typeof(NullModule));
    }

    public void TryReload()
    {
        if (_currentAmmo != _maxAmmo && !_isReloading)
        {
            Reload();
        }
    }

    public void TryShoot()
    {
        if (!_isCanShoot || Time.time - _lastShotTime < _intervalTime)
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

        _lastShotTime = Time.time;

    }

    private bool IsOutOfAmmo() => _currentAmmo-- <= 0;

    protected bool TryDecreaseAmmo() => !IsOutOfAmmo();

    protected abstract void Shoot();

    private void Reload()
    {
        _isCanShoot = false;
        _isReloading = true;
        _reloadStartTime = Time.time;
        StartCoroutine(ReloadingCoroutine());
        //StartReloadAnim();        
    }

    private IEnumerator ReloadingCoroutine()
    {
        yield return new WaitForSeconds(_reloadDuration);
        _currentAmmo = _maxAmmo;
        _isReloading = false;
        _isCanShoot = true;
        _reloadCooldown.text = "";
    }

    protected float CalculateDamage(float len) => Mathf.Clamp01(_gunConfig.DamageByDistance.Evaluate(len / _range)) * _damage;

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