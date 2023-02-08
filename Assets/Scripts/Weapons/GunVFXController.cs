using System;
using UnityEngine;

public class GunVFXController : MonoBehaviour
{
    [SerializeField] private Transform _shootPoint;
    [SerializeField] private TrailRenderer[] _tracerPrefabs;
    private TrailRenderer _currentTracerPrefab;


    [SerializeField] private ParticleSystem[] _shootParticles;
    private ParticleSystem _curreentShootParticles;

    public void SetTracer(int num)
    {
        _currentTracerPrefab = _tracerPrefabs[num];
    }

    public void SetShootParticles(int num)
    {
        if (_curreentShootParticles != null)
        {
            Destroy(_curreentShootParticles.gameObject);
        }
        _curreentShootParticles = Instantiate(_shootParticles[num], _shootPoint.position, Quaternion.LookRotation(_shootPoint.forward));
        _curreentShootParticles.transform.parent = _shootPoint;
    }


    public void SetBulletVFX(Type type)
    {
        if (type == typeof(Bullet))
        {
            SetTracer(0);
            SetShootParticles(0);
        }
        if (type == typeof(EffectBullet<Burning>))
        {
            SetTracer(1);
            SetShootParticles(1);
        }
        if (type == typeof(EffectBullet<Freeze>))
        {
            SetTracer(2);
            SetShootParticles(2);
        }
        if (type == typeof(EffectBullet<Electricity>))
        {
            SetTracer(3);
            SetShootParticles(3);
        }
    }

    public void ShowGunTracer(Vector3 offset)
    {
        TrailRenderer tracer = Instantiate(_currentTracerPrefab, _shootPoint.position, Quaternion.identity);
        tracer.AddPosition(_shootPoint.position);
        tracer.transform.position = _shootPoint.position + offset;
    }

    public void ShowShootParticles()
    {
        _curreentShootParticles.Play();
    }

    public void ShowHitParticles(LayerMask layer) 
    { 
        
    }

}
