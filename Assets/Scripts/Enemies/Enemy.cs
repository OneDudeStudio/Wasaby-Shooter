using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using NodeCanvas.BehaviourTrees;
using UnityEngine;
using UnityEngine.AI;

namespace Enemies
{
    public abstract class Enemy : MonoBehaviour, IApplyableDamage, ISpeedChangeable, IApplyableEffect
    {
        [SerializeField] protected float defaultSpeed;
        [SerializeField] protected float damage;

        [SerializeField] private Material _hitMaterial;
        [SerializeField] private float _health;
        
        [SerializeField] private float _pushForce;
        [SerializeField] private float _pushTime;

        [SerializeField] private float _flashTime;
        
        protected BehaviourTreeOwner behaviourTreeOwner;
        private List<Renderer> _meshRenderers;

        private bool _canApplyDamage = true;
        private bool isFlashing;

        private ProceduralEnemyMovement _proceduralMovement;

        private NavMeshAgent _navMeshAgent;
        protected IEnemyTarget target;

        private Animator _animator;
        private float _walkingAnimationSpeed;
        
        private const string EnemySpeedModifier = "EnemySpeed";

        public ProceduralEnemyMovement ProceduralMovement => _proceduralMovement;
        
        public float Speed
        {
            get => _navMeshAgent.speed;
            set
            {
                if(_navMeshAgent)
                    _navMeshAgent.speed = value;
            }
        }
        
        public event Action Died;
        public event Action Damaged;

        private List<Effect> _applyableEffects = new List<Effect>();

        private void Awake()
        {
            _navMeshAgent = GetComponent<NavMeshAgent>();
            _meshRenderers = GetComponentsInChildren<Renderer>().ToList();
            _animator = GetComponent<Animator>();
            _proceduralMovement = GetComponent<ProceduralEnemyMovement>();
            
            behaviourTreeOwner = GetComponent<BehaviourTreeOwner>();

            EffectsConfig config = FindObjectOfType<ConfigsLoader>().RootConfig.EffectsConfig;
            _applyableEffects.Add(new Burning(this, config));
            _applyableEffects.Add(new Freeze(this, config));
            _applyableEffects.Add(new Poison(this, config));
            _applyableEffects.Add(new Electricity(this, FindObjectOfType<ElectricityController>(), config));
            _applyableEffects.Add(new Stan(this, config));
        }

        private void Start()
        {
            Speed = defaultSpeed;
            
            if(!_proceduralMovement.enabled)
                _navMeshAgent.enabled = true;
            
            if(_animator)
                _walkingAnimationSpeed = _animator.GetFloat(EnemySpeedModifier);
            
            var effectController = FindObjectOfType<EffectsController>();
            
            effectController.AddVictim(this);
        }

        public void SetTarget(IEnemyTarget target) => this.target = target;

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

        public abstract void TryAttack(IApplyableDamage player);

        public bool TryApplyDamage(float damage)
        {
            if (!_canApplyDamage)
                return false;

            if (damage <= 0)
                return true;

            Damaged?.Invoke();
            _health -= damage;
            
            if (_health <= 0)
            {
                _canApplyDamage = false;
                Die();
                return false;
            }

            if (!isFlashing)
            {
                isFlashing = true;
                StartCoroutine(ApplyFlash());
            }

            if (!_proceduralMovement.enabled)
            {
                Transform victimTransform = transform;
                Vector3 pushVector = (victimTransform.position - ((MonoBehaviour)target).transform.position).normalized * _pushForce;
                ApplyPush(pushVector, _pushTime);
            }
            
            return true;
        }

        public void ApplyPush(Vector3 pushVector, float time)
        {
            StartCoroutine(PushCoroutine(transform, pushVector, time));
        }
        
        private IEnumerator PushCoroutine(Transform victimTransform, Vector3 pushVector, float pushTime)
        {
            _navMeshAgent.enabled = false;
            StartCoroutine(VictimPusher.Push(victimTransform, pushVector, _pushTime));
            yield return new WaitForSeconds(_pushTime);
            _navMeshAgent.enabled = true;
        }
        
        private IEnumerator ApplyFlash()
        {
            StartCoroutine(VictimFlasher.Flash(_meshRenderers, _hitMaterial, _flashTime));
            yield return new WaitForSeconds(_flashTime);
            isFlashing = false;
        }

        public void Die()
        {
            Died?.Invoke();
            Destroy(gameObject);
        }

        public void ModifySpeed(float modifier)
        {
            Speed = defaultSpeed * modifier;
  
            if(_animator)
                _animator.SetFloat(EnemySpeedModifier, modifier);
        }

        public void ResetSpeed()
        {
            Speed = defaultSpeed;
            
            if(_animator)
                _animator.SetFloat(EnemySpeedModifier, _walkingAnimationSpeed);
        }

        public void SetNavmeshAgent(bool state)
        {
            _navMeshAgent.enabled = state;
        }

        public Animator GetAnimator()
        {
            return _animator;
        }
    }
}