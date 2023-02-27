using System.Collections;
using UnityEngine;

public class RedBarrel : MonoBehaviour, IApplyableDamage
{
    [SerializeField] private float _health = 2;
    private bool _isCanApplyDamage = true;

    private Explosion _explosion;

    private void Start()
    {
        _explosion = GetComponent<Explosion>();
        _explosion.SetConfig(FindObjectOfType<ConfigsLoader>().RootConfig.ExplosionItemsConfig.Barrel);
        _explosion.SetDamageDealer(new ExplosionDamageDealer());
    }
    
    public void Die()
    {
        StartCoroutine(Explode());
    }

    private IEnumerator Explode()
    {
        //sound
        yield return new WaitForSeconds(.1f);
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
