using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using NodeCanvas.BehaviourTrees;
using PlayerController;
using UnityEngine;
using UnityEngine.AI;

namespace Enemies
{
    public abstract class Enemy : MonoBehaviour, IApplyableDamage, ISpeedChangeable, IApplyableEffect
    {
        [SerializeField] protected float _defaultSpeed;
        [SerializeField] private Material _hitMaterial;
        [SerializeField] private float _health;
        
        [SerializeField] private float _pushForce;
        [SerializeField] private float _pushTime;

        protected BehaviourTreeOwner behaviourTreeOwner;
        private List<SkinnedMeshRenderer> _meshRenderers;

        private bool _isHit;
        private bool _canApplyDamage = true;
        private float _maxHealth;

        private NavMeshAgent _navMeshAgent;
        protected PlayerManager playerManager;
       
        private Animator _animator;
        private float _walkingAnimationSpeed;
        private const string EnemySpeedModifier = "EnemySpeed";

        public event Action Died;
        public event Action Damaged;

        private List<Effect> _applyableEffects = new List<Effect>();

        private void Awake()
        {
            _navMeshAgent = GetComponent<NavMeshAgent>();
            _meshRenderers = GetComponentsInChildren<SkinnedMeshRenderer>().ToList();
            _animator = GetComponent<Animator>();
            behaviourTreeOwner = GetComponent<BehaviourTreeOwner>();

            _applyableEffects.Add(new Burning(this));
            _applyableEffects.Add(new Freeze(this));
            _applyableEffects.Add(new Poison(this));
            _applyableEffects.Add(new Electricity(this, FindObjectOfType<ElectricityController>()));
            _applyableEffects.Add(new Stan(this));
        }

        private void Start()
        {
            playerManager = FindObjectOfType<PlayerManager>();
            
            _maxHealth = _health;
            SetSpeed(_defaultSpeed);
            _navMeshAgent.enabled = true;
            
            if(_animator)
                _walkingAnimationSpeed = _animator.GetFloat(EnemySpeedModifier);

            FindObjectOfType<EffectsController>().AddVictim(this);
        }


        public void StartEffect<T>() where T : Effect
        {
            var effect = _applyableEffects.OfType<T>().FirstOrDefault();
            if (effect != null)
                effect.StartEffect();
        }

        public void ApplyEffects(float currentTime)
        {
            foreach (var item in _applyableEffects)
            {
                item.Apply(currentTime);
            }
        }

        public abstract void Attack(IApplyableDamage player);

        public void SetSpeed(float speed)
        {
            //_navMeshAgent.speed = _navMeshAgent.acceleration = speed;
            if(_navMeshAgent)
                _navMeshAgent.speed = speed;
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

            if (_isHit)
                return true;

            _isHit = true;
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

            _isHit = false;
            StartCoroutine(nameof(Push));
        }

        private IEnumerator Push()
        {
            Vector3 start = transform.position;
            Vector3 direction = (start - playerManager.transform.position).normalized * _pushForce;
            Vector3 end = new Vector3((start + direction).x, start.y, (start + direction).z);
            
            float timer = _pushTime;
            
            _navMeshAgent.enabled = false;
            
            while (timer > 0)
            {
                float interpolant = (_pushTime - timer) / _pushTime;
                transform.position = Vector3.Lerp(start, end, interpolant);
                timer -= Time.deltaTime;
                yield return null;
            }
            
            _navMeshAgent.enabled = true;
        }

        public void Die()
        {
            Died?.Invoke();
            Destroy(gameObject);
        }

        public void ModifySpeed(float modifier)
        {
            SetSpeed(_defaultSpeed * modifier);
  
            if(_animator)
                _animator.SetFloat(EnemySpeedModifier, modifier);
        }

        public void ResetSpeed()
        {
            SetSpeed(_defaultSpeed);
            
            if(_animator)
                _animator.SetFloat(EnemySpeedModifier, _walkingAnimationSpeed);
        }
    }
}

