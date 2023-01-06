using System.Collections;
using UnityEngine;

public abstract class Gun : MonoBehaviour
{
    [SerializeField] private float _defaultDamage;
    [SerializeField] private int _defaultMaxAmmo;
    [SerializeField] private float _defaultRange;
    [SerializeField] private float _defaultIntervalTime;
    [SerializeField] private AnimationCurve _damageByDistance;
    [SerializeField] private float _GrenadeLauncherInterval;

    private float _damage;
    private int _maxAmmo;
    protected float _range;
    private float _intervalTime;
    
    protected Camera _playerCamera;
    protected ParticleSystem _shootParticles;
    protected Recoil _recoil;
    protected BulletHolesPool _holePool;
    [SerializeField] protected GameObject _hitParticles;

    protected int _currentAmmo;    
    protected bool _isCanShoot = true;
    private bool _isCanLauncherShoot = true;

    private SupportModule _supportModule;
    protected Bullet _bullet;
    private GrenadeLauncher _grenadeLauncher;

    public enum GunType
    {
        Rifle,
        Shotgun
    }

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

    public void CalculateCharacteristics()
    {
        _supportModule.CalculateModuleCharacteristics();
        _damage *= _bullet.GetAdditionalDamage();
        _currentAmmo = _maxAmmo;
    }

    private void Start()
    {
        _currentAmmo = _defaultMaxAmmo;
        _playerCamera = FindObjectOfType<Camera>();  
        _shootParticles = GetComponentInChildren<ParticleSystem>();
        _recoil = FindObjectOfType<Recoil>();
        _holePool = FindObjectOfType<BulletHolesPool>();
        _supportModule = new ExtendedMag(this, GunType.Rifle, _recoil);
        _bullet = new FireBullet();
        //_bullet = new DefaultBullet();
        CalculateCharacteristics();
        _grenadeLauncher = GetComponent<GrenadeLauncher>();
    }

    protected bool IsOutOfAmmo() => _currentAmmo-- <= 0;
   
    public abstract void TryShoot();

    public void Reload()
    {
        _isCanShoot = false;
        ////
        ///
        ///
        _currentAmmo = _maxAmmo;
        _isCanShoot = true;
    }

    protected float CalculateDamage(float len) => Mathf.Clamp01(_damageByDistance.Evaluate(len / _range)) * _damage;

    protected IEnumerator Interval()
    {
        _isCanShoot = false;
        yield return new WaitForSeconds(_intervalTime);
        _isCanShoot = true;
    }
    private IEnumerator LauncherIntrval()
    {
        _isCanLauncherShoot = false;
        yield return new WaitForSeconds(_GrenadeLauncherInterval);
        _isCanLauncherShoot = true;
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.Mouse0) && (_isCanShoot))
        {
            TryShoot();
            StartCoroutine(Interval());
        }
        if (Input.GetKeyDown(KeyCode.R) && _currentAmmo != _maxAmmo)
        {
            Reload();
        }
        if (Input.GetKeyDown(KeyCode.Mouse1) && (_isCanLauncherShoot))
        {
            _grenadeLauncher.ShootGranade();
            StartCoroutine(LauncherIntrval());
        }

    }
}
