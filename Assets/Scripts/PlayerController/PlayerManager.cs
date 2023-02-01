using System;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace PlayerController
{
    public class PlayerManager : MonoBehaviour, IApplyableDamage
    {
        [SerializeField] private float _health = MaxHealth;

        private const float MaxHealth = 100f;
        private bool _canApplyDamage = true;

        public event Action<float> Damaged;
        
        public bool IsAlive() => _canApplyDamage;
        public void Die()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }

        public bool TryApplyDamage(float damage)
        {
            if (!_canApplyDamage)
                return false;

            _health -= damage;

            Damaged?.Invoke(_health/MaxHealth);
            
            if (_health <= 0)
            {
                _canApplyDamage = false;
                Die();
                return false;
            }
            
            Debug.Log($"Health: {_health}");
            return true;
        }
    }
}

