using System;
using System.Linq;
using Enemies.CustomTasks;
using NodeCanvas.Framework;
using UnityEngine;

namespace Enemies
{
    public class MeleeEnemy : Enemy
    {
        [SerializeField] private int _damage;
        [SerializeField] private float _attackViewAngle;

        private float _excessDistance;
        
        public void Attack()
        {
            Variable attackRange = behaviourTreeOwner.graph.blackboard.GetVariable("attackRange");

            if (_excessDistance == 0)
                _excessDistance = behaviourTreeOwner
                    .graph
                    .GetAllTasksOfType<CheckRangeTask>()
                    .FirstOrDefault()
                    !.radius;

            float distance = Vector3.Distance(playerManager.transform.position, transform.position) - _excessDistance;
            
            if ( distance <= (float)attackRange.value && CheckAttackFieldView())
            {
                Attack(playerManager);
            }
            
        }

        public bool CheckAttackFieldView()
        {
            Vector3 enemyForwardVector = transform.forward;
            Vector3 enemyPosition = transform.position;
            Vector3 victimPosition = playerManager.transform.position;

            var vectorFromEnemyToVictim = new Vector3(victimPosition.x - enemyPosition.x, victimPosition.y, victimPosition.z - enemyPosition.z);
            enemyForwardVector = new Vector3(enemyForwardVector.x, victimPosition.y, enemyForwardVector.z);
            
            float angle = Vector3.Angle(enemyForwardVector, vectorFromEnemyToVictim);

            return angle < _attackViewAngle;
        }
        
        public override void Attack(IApplyableDamage player)
        {
            player.TryApplyDamage(_damage);
        }
    }
}

