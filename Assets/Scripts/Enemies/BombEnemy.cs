using System;
using UnityEngine;

namespace Enemies
{
    public class BombEnemy : Enemy
    {
        [SerializeField] private GameObject _explosion;
        [SerializeField] private Transform _wheel;

        public override void TryAttack(IApplyableDamage player)
        {
            player.TryApplyDamage(damage);
            Instantiate(_explosion, transform.position, Quaternion.identity);
            Die();
        }

        private void Update()
        {
            _wheel.rotation *= Quaternion.AngleAxis(5, Vector3.right);
        }
    }
}