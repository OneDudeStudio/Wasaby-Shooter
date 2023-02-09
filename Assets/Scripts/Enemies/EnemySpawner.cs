using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace Enemies
{
    public class EnemySpawner : MonoBehaviour
    {
        [SerializeField] private EnemyFactory _enemyFactory;

        public List<Enemy> SpawnWave(EnemyWave wave, List<Transform> points)
        {
            int index = 0;
            var enemies = new List<Enemy>();

            foreach (var enemy in wave.GetEnemies())
            {
                for (var i = 0; i < enemy.Value; i++)
                {
                    enemies.Add(Spawn(enemy.Key, points[index].position));
                    index = (index + 1) % points.Count;
                }
            }

            return enemies;
        }

        private void Spawn(EnemyType type, Vector3 position, int count)
        {
            for (var i = 0; i < count; i++)
                Spawn(type, position);
        }

        public Enemy Spawn(EnemyType type, Vector3 position)
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