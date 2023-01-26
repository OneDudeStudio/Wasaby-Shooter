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
            Die();
            Instantiate(_explosion, transform.position, Quaternion.identity);
        }
    }
}
