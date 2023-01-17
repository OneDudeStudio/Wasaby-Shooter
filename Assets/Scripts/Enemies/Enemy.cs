using System;
using UnityEngine;
using UnityEngine.AI;

namespace Enemies
{
    public abstract class Enemy : MonoBehaviour
    {
        [SerializeField] protected float speed;
        [SerializeField] protected float speedModifier = 1;

        private NavMeshAgent _navMeshAgent;

        public event Action Died;

        private void Awake()
        {
            _navMeshAgent = GetComponent<NavMeshAgent>();
        }

        private void Start()
        {
            SetSpeed(speed);
        }

        public abstract void Attack(IApplyableDamage player);

        public void SetSpeed(float speed)
        {
            _navMeshAgent.speed = _navMeshAgent.acceleration = speed * speedModifier;
        }

        public void Die()
        {
            Died?.Invoke();
        }
    }
}

