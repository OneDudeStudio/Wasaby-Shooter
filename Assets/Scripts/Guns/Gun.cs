using System;
using System.Collections;
using UnityEngine;

public abstract class Gun : MonoBehaviour
{
    [Header("Gun Keys")]
    [SerializeField] private KeyCode _shootKey;
    [SerializeField] private KeyCode _reloadKey;
    
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
    protected Bullet _bullet;
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
            if(value>=0)
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
        _moduleManager.SetModule(typeof(NullModule));
        _bullet = new DefaultBullet();
        CalculateCharacteristics();
        _currentAmmo = ThisGunConfig._defaultMaxAmmo;
    }   
    
    private void Update()
    {
        if (Input.GetKey(_shootKey) && (_isCanShoot))
        {
            TryShoot();
            StartCoroutine(IntervalBetweenShoots());
        }
        
        if (Input.GetKeyDown(_reloadKey) && _currentAmmo != _maxAmmo)
        {
            Reload();
        }

        if (Input.GetKeyDown(KeyCode.M))
        {
            SetModule(typeof(ExtendedMag));
        }
    }

    public void SetModule(Type moduleType)
    {
        _moduleManager.SetModule(moduleType);
    }

    private bool IsOutOfAmmo() => _currentAmmo-- <= 0;

    protected bool TryDecreaseAmmo()
    {
        if (IsOutOfAmmo())
        {
            _sound.PlayOutOfAmmoSound();
            return false;
        }

        _shootParticles.Play();
        _recoil.RecoilFire();
        return true;
    }

    protected abstract void TryShoot();  

    private void Reload()
    {
        _isCanShoot = false;
        //StartReloadAnim();
        _currentAmmo = _maxAmmo;
        _isCanShoot = true;
    }
    
    private void CalculateCharacteristics()
    {
        _damage *= _bullet.GetAdditionalDamage();
        //Refactor?
        _currentAmmo = _maxAmmo;
    }

    protected float CalculateDamage(float len) => Mathf.Clamp01(ThisGunConfig._damageByDistance.Evaluate(len / _range)) * _damage;

    private IEnumerator IntervalBetweenShoots()
    {
        _isCanShoot = false;
        yield return new WaitForSeconds(_intervalTime);
        _isCanShoot = true;
    }

}