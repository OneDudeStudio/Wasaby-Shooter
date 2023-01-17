using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace Enemies
{
    public class EnemySpawner : MonoBehaviour
    {
        [SerializeField] private EnemyFactory _enemyFactory;

        private BattleScenario _battleScenario;

        public void SetBattleScenario(BattleScenario scenario)
        {
            _battleScenario = scenario;
        }

        public void SpawnSquad(EnemySquad squad, List<Transform> points)
        {
            int index = 0;
            
            foreach (var enemy in squad.GetEnemies())
            {
                for (var i = 0; i < enemy.Value; i++)
                {
                    if (index == points.Count)
                        return;
                    
                    Spawn(enemy.Key, points[index++].position);
                }
            }
        }

        private void Spawn(EnemyType type, Vector3 position, int count)
        {
            for (var i = 0; i < count; i++)
                Spawn(type, position);
        }

        private void Spawn(EnemyType type, Vector3 position)
        {
            var enemy = _enemyFactory.Get(type);

            if (_battleScenario)
                enemy.Died += _battleScenario.ChangeScenario;

            if (NavMesh.SamplePosition(position, out NavMeshHit hit, 1f, NavMesh.AllAreas))
                enemy.transform.position = position;
        }
    }
}