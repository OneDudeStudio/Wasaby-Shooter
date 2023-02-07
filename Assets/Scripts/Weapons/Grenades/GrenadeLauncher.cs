using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrenadeLauncher : MonoBehaviour
{
    [SerializeField] private Grenade[] _grenadePrefabs;
    [SerializeField] private Transform _grenadeSpawnPoint;
    [SerializeField] private Transform _cameraTransform;

    [SerializeField] private float _throwForce;
    [SerializeField] private float _upwardForce;

    private float _currentInterval;
    private Grenade _currentGrenade;
    private bool _isCanLauncherShoot = true;
   
    private readonly Dictionary<Type, float> _interval = new Dictionary<Type, float>()
    {
        //poison grenade cooldown
        { typeof(PoisonGrenade), 5f},
        //explosion grenade cooldown
        { typeof(DefaultExplosionGrenade), 5f},
        //flash grenade cooldown
        { typeof(FlashGrenade), 5f }
    };

    private void Start()
    {
        //SetGrenade(null);
        SetGrenade(typeof(FlashGrenade));
    }

    private IEnumerator LauncherIntrval()
    {
        _isCanLauncherShoot = false;
        yield return new WaitForSeconds(_currentInterval);
        _isCanLauncherShoot = true;
    }

    public void SetGrenade(Type type)
    {
        _isCanLauncherShoot = false;
        if (type == null)
            return;

        _currentInterval = _interval[type];
        if (type == typeof(PoisonGrenade))
            _currentGrenade = _grenadePrefabs[0];
        if (type == typeof(DefaultExplosionGrenade))
            _currentGrenade = _grenadePrefabs[1];
        if (type == typeof(FlashGrenade))
            _currentGrenade = _grenadePrefabs[2];
        _isCanLauncherShoot = true;
    }

    private void ShootGranade()
    {
        Grenade grenade = Instantiate(_currentGrenade, _grenadeSpawnPoint.position, _cameraTransform.rotation);
        Rigidbody rb = grenade.GetComponent<Rigidbody>();

        Vector3 direction = _cameraTransform.forward;
        if (Physics.Raycast(_cameraTransform.position, _cameraTransform.forward, out RaycastHit hit, 300f))
            direction = (hit.point - _grenadeSpawnPoint.position).normalized;

        Vector3 force = direction * _throwForce + transform.up * _upwardForce;
        rb.AddForce(force, ForceMode.Impulse);
        rb.angularVelocity = new Vector3(0, 0, 3f);
    }

    public void TryShootGrenade()
    {
        if(!_isCanLauncherShoot)
        {
            return;
        }

        ShootGranade();
        StartCoroutine(LauncherIntrval());
    }
}
