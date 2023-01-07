using System;
using UnityEngine;

public class PlayerManager : MonoBehaviour, IApplyableDamage, IApplyablePoison
{
    private bool _isCanApplyDamage = true;
    [SerializeField] private float _health = 10f;

    public bool isAlive() => _isCanApplyDamage;
    public void Die()
    {
        Destroy(gameObject);
    }

    public bool TryApplyDamage(float damage)
    {
        if (!_isCanApplyDamage)
            return false;

        _health -= damage;

        if (_health <= 0)
        {
            _isCanApplyDamage = false;
            Die();
            return false;
        }

        return true;
    }
    public void ApplyEffect(Type type)
    {
        
    }

    public void Poison(float damage)
    {
        throw new NotImplementedException();
    }
}

