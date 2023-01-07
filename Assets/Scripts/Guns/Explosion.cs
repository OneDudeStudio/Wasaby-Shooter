using UnityEngine;

public class Explosion : MonoBehaviour
{
    [SerializeField] private float _explosionDamage;
    [SerializeField] private float _explosionRadius;
    [SerializeField] private ParticleSystem _explosionParticles;
    [SerializeField] private int _maxTargets;
    [SerializeField] private LayerMask _checkLayers;
    public void Explode()
    {
        Instantiate(_explosionParticles, transform.position, Quaternion.identity);
        Collider[] colliders = new Collider[_maxTargets];
        int collidersNumber = Physics.OverlapSphereNonAlloc(transform.position, _explosionRadius, colliders);
        for(int i = 0; i< collidersNumber; i++)
        {
            if (colliders[i].TryGetComponent(out IApplyableDamage damaged))
            {
                float distance = Vector3.Distance(transform.position, colliders[i].transform.position);
                if(!Physics.Raycast(transform.position, (colliders[i].transform.position - transform.position).normalized, distance, _checkLayers))
                    damaged.TryApplyDamage(_explosionDamage);
            }                
        }
        Destroy(gameObject);
    }
}
