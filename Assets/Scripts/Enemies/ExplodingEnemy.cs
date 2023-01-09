using UnityEngine;

namespace Enemies
{
    public class ExplodingEnemy : Enemy
    {
        [SerializeField] private int _damage;
        [SerializeField] private GameObject _explosion;

        public override void Attack(IApplyableDamage player)
        {
            player.TryApplyDamage(_damage);
            Destroy(gameObject);

            Instantiate(_explosion, transform.position, Quaternion.identity);
        }
    }
}
