using UnityEngine;

public class ShootParticlesController : MonoBehaviour
{
    private ParticleSystem[] _particles;
    private void Start()
    {
        _particles = GetComponentsInChildren<ParticleSystem>();
    }


    public void ShowParticles()
    {
        foreach(ParticleSystem particle in _particles)
        {
            particle.Play();
        }
    }
}
