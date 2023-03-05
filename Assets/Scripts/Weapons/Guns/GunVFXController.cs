using System;
using UnityEngine;

public class GunVFXController : MonoBehaviour
{
    [SerializeField] private Transform _shootPoint;
    [SerializeField] private TrailRenderer[] _tracerPrefabs;
    private TrailRenderer _currentTracerPrefab;

    [SerializeField] private ParticleSystem[] _shootParticles;
    private ParticleSystem _curreentShootParticles;

    private PoolsController _poolsController;
    private MaterialChoise _materialChoise;
    [SerializeField] private Animator _animator;

    private int _shootAnimationId;

    private void Start()
    {       
        _shootAnimationId = Animator.StringToHash("Shoot");
        _materialChoise = FindObjectOfType<MaterialChoise>();
        _poolsController = _materialChoise.PoolsController;
    }
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

    public void ShowBulletHole(RaycastHit hit)
    {
        BulletHole hole = _poolsController.GetBulletHole();
        hole.gameObject.SetActive(true);
        hole.transform.SetPositionAndRotation(hit.point + hit.normal * 0.001f, Quaternion.LookRotation(hit.normal));
        hole.transform.parent = hit.transform;
    }

    public void ShowHitParticles(RaycastHit hit)
    {
        if (hit.transform.TryGetComponent(out Renderer renderer))
        {
            ImpactParticle particle = _materialChoise.GetMaterialsParticles(renderer.sharedMaterial);
            particle.transform.SetPositionAndRotation(hit.point, Quaternion.LookRotation(hit.normal));
            particle.gameObject.SetActive(true);
        }
    }

    public void PlayShootAnimation()
    {
        _animator.Play(_shootAnimationId);
    }

}
