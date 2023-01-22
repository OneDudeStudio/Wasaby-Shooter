using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace Enemies
{
    public class EnemySpawner : MonoBehaviour
    {
        [SerializeField] private EnemyFactory _enemyFactory;

        public List<Enemy> SpawnSquad(EnemySquad squad, List<Transform> points)
        {
            int index = 0;
            var enemies = new List<Enemy>();

            foreach (var enemy in squad.GetEnemies())
            {
                for (var i = 0; i < enemy.Value; i++)
                {
                    if (index == points.Count)
                        return null;
                    
                    enemies.Add(Spawn(enemy.Key, points[index++].position));
                }
            }

            return enemies;
        }

        private void Spawn(EnemyType type, Vector3 position, int count)
        {
            for (var i = 0; i < count; i++)
                Spawn(type, position);
        }

        private Enemy Spawn(EnemyType type, Vector3 position)
        {
            var enemy = _enemyFactory.Get(type);

            if (NavMesh.SamplePosition(position, out NavMeshHit hit, 1f, NavMesh.AllAreas))
            {
                enemy.transform.position = position;

                return enemy;
            }
            
            return null;
        }
    }
}