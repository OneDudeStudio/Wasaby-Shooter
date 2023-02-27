using PlayerController;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    [SerializeField] private bool isAffectingPlayer;

    private float _explosionDamage;
    private float _explosionRadius;
    private LayerMask _obstacles;

    private EnemyController _enemyController;
    private Transform _player;
    private IDamageDealer _explosionDamageDealer;
    [SerializeField] private ParticleSystem _explosionParticles;
    

    private void Start()
    {
        _enemyController = FindObjectOfType<EnemyController>();
        _enemyController.AddVictim(transform);
        _player = FindObjectOfType<PlayerManager>().transform;
    }

    public void Explode()
    {
        List<Transform> affectedAreaTargets = _enemyController.AffectAreaVictims(transform.position, _explosionRadius);
        affectedAreaTargets.Remove(transform);

        ParticleSystem particles = Instantiate(_explosionParticles, transform.position, Quaternion.identity);
        particles.transform.localScale = new Vector3(_explosionRadius * 1.5f, _explosionRadius * 1.5f, _explosionRadius * 1.5f);

        if (isAffectingPlayer)
        {
            if ((_player.position - transform.position).sqrMagnitude < _explosionRadius * _explosionRadius)
            {
                affectedAreaTargets.Add(_player);
            }
        }

        foreach (var target in affectedAreaTargets)
        {
            if (!Physics.Raycast(transform.position, (target.position - transform.position).normalized, (target.position - transform.position).magnitude, _obstacles))
            {
                _explosionDamageDealer.DealDamage(target.GetComponent<IApplyableDamage>(), _explosionDamage);
            }
        }

        _enemyController.DestroyVictim(transform);
    }

    public void SetConfig(ExplosionConfig config)
    {
        _explosionDamage = config.ExplosionDamage;
        _explosionRadius = config.ExplosionRadius;
        _obstacles = config.CheckLayers;
    }

    public void SetDamageDealer(IDamageDealer dealer) => _explosionDamageDealer = dealer;
}
