using UnityEngine;

public class Explosion : MonoBehaviour
{
    private float _explosionDamage;
    private float _explosionRadius;
    private int _maxTargets;
    private LayerMask _checkLayers;
    private ParticleSystem _explosionParticles;

    public IApplyableDamage[] Explode()
    {
        Instantiate(_explosionParticles, transform.position, Quaternion.identity);

        Collider[] colliders = new Collider[_maxTargets];
        int collidersNumber = Physics.OverlapSphereNonAlloc(transform.position, _explosionRadius, colliders);
        IApplyableDamage[] victims = new IApplyableDamage[collidersNumber];
        int counter = 0;

        for (int i = 0; i < collidersNumber; i++)
        {
            if (colliders[i].TryGetComponent(out IApplyableDamage damaged))
            {
                float distance = Vector3.Distance(transform.position, colliders[i].transform.position);

                if (!Physics.Raycast(transform.position, (colliders[i].transform.position - transform.position).normalized, distance, _checkLayers))
                {
                    damaged.TryApplyDamage(_explosionDamage);

                    if (damaged != null)
                    {
                        victims[counter++] = damaged;
                    }
                }
            }
        }
        Destroy(gameObject);
        return victims;
    }

    public void SetConfig(GrenadeConfig config)
    {
        _explosionDamage = config.ExplosionDamage;
        _explosionRadius = config.ExplosionRadius;
        _maxTargets = config.MaxTargets;
        _checkLayers = config.CheckLayers;
        _explosionParticles = config.Particles;
    }
}
