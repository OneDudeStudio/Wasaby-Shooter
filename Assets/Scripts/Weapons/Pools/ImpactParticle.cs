using UnityEngine;

public class ImpactParticle : MonoBehaviour
{
    private Transform _container;
    private void Awake()
    {
        _container = transform.parent;
    }
   
    private void OnParticleSystemStopped()
    {
        gameObject.SetActive(false);
        transform.parent = _container;
    }
}
