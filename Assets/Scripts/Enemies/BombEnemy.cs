using UnityEngine;

namespace Enemies
{
    public class BombEnemy : Enemy
    {
        [SerializeField] private GameObject _explosion;

        public override void TryAttack(IApplyableDamage player)
        {
            player.TryApplyDamage(damage);
            Instantiate(_explosion, transform.position, Quaternion.identity);
            Die();
        }
    }
}