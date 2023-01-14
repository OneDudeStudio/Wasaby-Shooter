using System.Collections;
using UnityEngine;

public abstract class Gun : MonoBehaviour
{
    public enum GunType
    {
        Rifle,
        Shotgun
    }
    
    [Header("Basic Gun Stats")]
    [SerializeField] private float _defaultDamage;
    [SerializeField] private int _defaultMaxAmmo;
    [SerializeField] private float _defaultRange;
    [SerializeField] private float _defaultIntervalTime;
    [SerializeField] private AnimationCurve _damageByDistance;
    
    [Space] 
    [Header("Gun Keys")]
    [SerializeField] private KeyCode _shootKey;
    [SerializeField] private KeyCode _reloadKey;
    
    [Space] 
    [Header("Need Refactor")]
    [SerializeField] protected ParticleSystem _shootParticles;
    [SerializeField] protected Recoil _recoil;
    [SerializeField] protected GameObject _hitParticles;

    [SerializeField] protected GunConfig _thisGunConfig;
    //[SerializeField] private GunModulesConfig _thisGunModuleConfig;
    
    private float _damage;
    private int _maxAmmo;
    private float _intervalTime;
    protected float _range;

    protected int _currentAmmo;    
    protected bool _isCanShoot = true;
    protected Camera _playerCamera;
    protected BulletHolesPool _holePool;
    private SupportModule _supportModule;
    protected Bullet _bullet;


    //public GunModulesConfig ThisGunModuleConfig => _thisGunModuleConfig;
    
    public float GetDamage() => _defaultDamage;
    public int GetAmmo() => _defaultMaxAmmo;
    public float GetRange() => _defaultRange;
    public float GetInterval() => _defaultIntervalTime;

    public void SetDamage(float damage)
    {
        if (damage > 0)
            _damage = damage;
    }
    public void SetAmmo(int ammo)
    {
        if (ammo > 0)
            _maxAmmo = ammo;
    }
    public void SetRange(float range)
    {
        if (range > 0)
            _range = range;
    }
    public void SetInterval(float interval)
    {
        if (interval > 0)
            _intervalTime = interval;
    }

    private void Start()
    {
        _currentAmmo = _defaultMaxAmmo;
        _playerCamera = Camera.main; 
        //_shootParticles = GetComponentInChildren<ParticleSystem>();
        //_recoil = FindObjectOfType<Recoil>();
        _holePool = FindObjectOfType<BulletHolesPool>();
        _supportModule = new NullModule(this, GunType.Rifle, _recoil);
        //_supportModule = new ExtendedMag(this, GunType.Rifle, _recoil);
        //_bullet = new FireBullet();
        //_bullet = new ElectricBullet();
        _bullet = new DefaultBullet();
        CalculateCharacteristics();

        //DefaultGun d =_thisGunConfig.Riffle;
        //d._defaultDamage = 1;
    }


    protected void SetNewGunStats()
    {
        float dmg = _thisGunConfig._defaultDamage;

        /// modify 

        _damage = dmg;
    }

    protected void SetNewGunDamage(float damageMultiplier)
    {
        _damage = _thisGunConfig._defaultDamage;
        _damage *= damageMultiplier;
    }

    protected void SetNewGunRange(float rangeMultiplier)
    {
        _range = _thisGunConfig._defaultRange;
        _range *= rangeMultiplier;
    }

    protected void SetNewGunMag(int magMultiplier)
    {
        _maxAmmo = _thisGunConfig._defaultMaxAmmo;
        _maxAmmo += magMultiplier;
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
    }

    private bool IsOutOfAmmo() => _currentAmmo-- <= 0;

    protected virtual void TryShoot()
    {
        if (IsOutOfAmmo())
        {
            return;
        }
        
        _shootParticles.Play();
        _recoil.RecoilFire();
    }

    private void Reload()
    {
        _isCanShoot = false;
        //StartReloadAnim();
        _currentAmmo = _maxAmmo;
        _isCanShoot = true;
    }
    
    private void CalculateCharacteristics()
    {
        _supportModule.CalculateModuleCharacteristics();
        _damage *= _bullet.GetAdditionalDamage();
        //Refactor?
        _currentAmmo = _maxAmmo;
    }

    protected float CalculateDamage(float len) => Mathf.Clamp01(_damageByDistance.Evaluate(len / _range)) * _damage;

    private IEnumerator IntervalBetweenShoots()
    {
        _isCanShoot = false;
        yield return new WaitForSeconds(_intervalTime);
        _isCanShoot = true;
    }

}