using System;
using System.Collections;
using UnityEngine;
using UnityEngine.AI;

namespace Enemies
{
    public abstract class Enemy : MonoBehaviour, IApplyableDamage
    {
        [SerializeField] protected float speed;

        [SerializeField] private Material _hitMaterial;

        [SerializeField] private float _health;
        
        private Renderer _renderer;
        private Material _defaultMaterial;
        
        private bool _canApplyDamage = true;

        private NavMeshAgent _navMeshAgent;

        public event Action Died;

        private void Awake()
        {
            _navMeshAgent = GetComponent<NavMeshAgent>();
            _renderer = GetComponent<Renderer>();
        }

        private void Start()
        {
            _defaultMaterial = _renderer.material;
            SetSpeed(speed);
        }

        public abstract void Attack(IApplyableDamage player);

        public void SetSpeed(float speed)
        {
            _navMeshAgent.speed = _navMeshAgent.acceleration = speed;
        }

        public bool TryApplyDamage(float damage)
        {
            if (!_canApplyDamage)
                return false;
            if (damage < 0)
                return true;
            _health -= damage;
            if (_health <= 0)
            {
                Die();
                _canApplyDamage = false;
                return false;
            }
            StartCoroutine(hit());
            return true;
        }
        
        private IEnumerator hit()
        {
            _renderer.material = _hitMaterial;
            yield return new WaitForSeconds(.05f);
            _renderer.material = _defaultMaterial;
        }

        public void Die()
        {
            Destroy(gameObject);
            Died?.Invoke();
        }
    }
}

