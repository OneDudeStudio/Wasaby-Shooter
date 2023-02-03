using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using NodeCanvas.Tasks.Actions;
using PlayerController;
using UnityEngine;
using UnityEngine.AI;

namespace Enemies
{
    public abstract class Enemy : MonoBehaviour, IApplyableDamage
    {
        [SerializeField] protected float speed;
        [SerializeField] private Material _hitMaterial;
        [SerializeField] private float _health;

        private List<SkinnedMeshRenderer> _meshRenderers;

        private bool _canApplyDamage = true;
        private float _maxHealth;

        private NavMeshAgent _navMeshAgent;
        private PlayerManager _playerManager;

        public event Action Died;
        public event Action Damaged;

        private void Awake()
        {
            _navMeshAgent = GetComponent<NavMeshAgent>();
            _meshRenderers = GetComponentsInChildren<SkinnedMeshRenderer>().ToList();
        }

        private void Start()
        {
            _playerManager = FindObjectOfType<PlayerManager>();
            
            _maxHealth = _health;
            SetSpeed(speed);
            _navMeshAgent.enabled = true;
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

            if(Math.Abs(_health - _maxHealth) < 1e-5)
                Damaged?.Invoke();

            _health -= damage;
            if (_health <= 0)
            {
                Die();
                _canApplyDamage = false;
                return false;
            }
            StartCoroutine(Hit());
            return true;
        }
        
        private IEnumerator Hit()
        {
            List<Material> materials = new List<Material>();

            foreach (var meshRenderer in _meshRenderers)
            {
                materials.Add(meshRenderer.material);
                meshRenderer.material = _hitMaterial;
            }
            
            yield return new WaitForSeconds(.05f);
            
            for (int i = 0; i < _meshRenderers.Count; i++)
                _meshRenderers[i].material = materials[i];
            
            StartCoroutine(nameof(Push));
        }

        private IEnumerator Push()
        {
            Vector3 start = transform.position;
            Vector3 direction = (start - _playerManager.transform.position).normalized * 1.5f;
            Vector3 end = new Vector3((start + direction).x, start.y, (start + direction).z);

            float pushTime = 0.5f;
            float timer = pushTime;
            
            _navMeshAgent.isStopped = true;
            
            while (timer > 0)
            {
                float interpolant = (pushTime - timer) / pushTime;
                transform.position = Vector3.Lerp(start, end, interpolant);
                timer -= Time.deltaTime;
                yield return null;
            }
            
            _navMeshAgent.isStopped = false;
        }

        public void Die()
        {
            Died?.Invoke();
            Destroy(gameObject);
        }
    }
}

