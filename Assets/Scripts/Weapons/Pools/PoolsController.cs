using UnityEngine;

public class PoolsController : MonoBehaviour
{
    [SerializeField] private BulletHole _bulletHolePrefab;
    [SerializeField] private ImpactParticle[] _impactPrefabs;

    [SerializeField] private int _holesCount;
    [SerializeField] private int _impactsCount;

    private CirclePool<BulletHole> _holesPool;
    private CirclePool<ImpactParticle>[] _particlesPools;

    private void Start()
    {
        _holesPool = new CirclePool<BulletHole>(_bulletHolePrefab, _holesCount, transform);

        _particlesPools = new CirclePool<ImpactParticle>[_impactPrefabs.Length];
        for (int i = 0; i < _impactPrefabs.Length; i++)
        {
            _particlesPools[i] = new CirclePool<ImpactParticle>(_impactPrefabs[i], _impactsCount, transform);
        }
    }

    public BulletHole GetBulletHole() => _holesPool.GetPoolObject();
    public ImpactParticle GetHitImpact(ImpactType type) => _particlesPools[(int)type].GetPoolObject();
}
