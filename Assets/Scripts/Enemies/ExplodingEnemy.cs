using UnityEngine;

namespace Enemies
{
    public class ExplodingEnemy : Enemy
    {
        [SerializeField] private int _damage;
        
        public override void Attack(IApplyableDamage player)
        {
            player.TryApplyDamage(_damage);
            Destroy(gameObject);
        }
    }
}
