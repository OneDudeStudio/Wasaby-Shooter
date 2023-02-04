using UnityEngine;

namespace Enemies
{
    public class MeleeEnemy : Enemy
    {
        [SerializeField] private int _damage;

        public override void Attack(IApplyableDamage player)
        {
            player.TryApplyDamage(_damage);
        }
    }
}

