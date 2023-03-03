using System.Linq;
using Enemies.CustomTasks;
using NodeCanvas.Framework;
using UnityEngine;

namespace Enemies
{
    public class MeleeEnemy : Enemy
    {
        [SerializeField] private float _attackViewAngle;
        
        private float _excessDistance;
        
        public void OnMeleeAttackAnimated()
        {
            TryAttack(target);
        }
        
        public override void TryAttack(IApplyableDamage player)
        {
            if(CheckAttackConditions())
                player.TryApplyDamage(damage);
        }

        private bool CheckAttackConditions()
        {
            Variable attackRange = behaviourTreeOwner.graph.blackboard.GetVariable("attackRange");

            if (_excessDistance == 0)
                _excessDistance = behaviourTreeOwner
                    .graph
                    .GetAllTasksOfType<CheckRangeTask>()
                    .FirstOrDefault()
                    !.AdditionalDistance;

            float distance = Vector3.Distance(((MonoBehaviour)target).transform.position, transform.position) - _excessDistance;
            return distance <= (float)attackRange.value && CheckAttackFieldView();
        }
        
        private bool CheckAttackFieldView()
        {
            Vector3 enemyForwardVector = transform.forward;
            Vector3 enemyPosition = transform.position;
            Vector3 victimPosition = ((MonoBehaviour)target).transform.position;

            var vectorFromEnemyToVictim = new Vector3(victimPosition.x - enemyPosition.x, victimPosition.y, victimPosition.z - enemyPosition.z);
            enemyForwardVector = new Vector3(enemyForwardVector.x, victimPosition.y, enemyForwardVector.z);
            
            float angle = Vector3.Angle(enemyForwardVector, vectorFromEnemyToVictim);

            return angle < _attackViewAngle;
        }
    }
}

