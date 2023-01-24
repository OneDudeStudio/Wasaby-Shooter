using UnityEngine;

namespace PlayerController
{
    public class PlayerManager : MonoBehaviour, IApplyableDamage
    {
        [SerializeField] private float _health = 100f;
    
        private bool _canApplyDamage = true;

        public bool IsAlive() => _canApplyDamage;
        public void Die()
        {
            Destroy(gameObject);
        }

        public bool TryApplyDamage(float damage)
        {
            if (!_canApplyDamage)
                return false;

            _health -= damage;

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

