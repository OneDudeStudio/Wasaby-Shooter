using UnityEngine;

public class Explosion : MonoBehaviour
{
    [SerializeField] private float _explosionDamage;
    [SerializeField] private float _explosionRadius;
    [SerializeField] private ParticleSystem _explosionParticles;
    [SerializeField] private int _maxTargets;
    public void Explode()
    {
        Instantiate(_explosionParticles, transform.position, Quaternion.identity);
        Collider[] colliders = new Collider[_maxTargets];
        int collidersNumber = Physics.OverlapSphereNonAlloc(transform.position, _explosionRadius, colliders);
        for(int i = 0; i< collidersNumber; i++)
        {
            if (colliders[i].gameObject.Equals(this))
            {
                Debug.Log("same");
                continue;
            }
            if (colliders[i].TryGetComponent(out IApplyableDamage damaged))
                damaged.TryApplyDamage(_explosionDamage);
        }
        Destroy(gameObject);
    }
}
