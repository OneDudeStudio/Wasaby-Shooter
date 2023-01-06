using UnityEngine;

public class RedBarrel : MonoBehaviour, IApplyableDamage
{
    private bool _isCanApplyDamage = true;
    [SerializeField] private float _health = 2;
    private Explosion _explosion;

    private void Start() => _explosion = GetComponent<Explosion>();
    public void Die()
    {
        _explosion.Explode();       
    }

    public bool TryApplyDamage(float damage)
    {
        if (!_isCanApplyDamage)
            return false;
        if (damage < 0)
            return true;

        _health -= damage;
        if (_health <= 0)
        {
            _isCanApplyDamage = false;
            Die();
            return false;
        }
        return true;
    }
}
