using UnityEngine;

public class RedBarrel : MonoBehaviour, IApplyableDamage
{
    [SerializeField] private float _health = 2;
    private bool _isCanApplyDamage = true;
    private Explosion _explosion;

    private void Start() => _explosion = GetComponent<Explosion>();
    public void Die()
    {
        GlobalEventManager.SendDie(transform);
        _explosion.Explode();
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
}
