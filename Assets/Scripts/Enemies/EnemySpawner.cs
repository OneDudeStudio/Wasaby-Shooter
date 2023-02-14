using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace Enemies
{
    public class EnemySpawner : MonoBehaviour
    {
        [SerializeField] private EnemyFactory _enemyFactory;

        public List<Enemy> SpawnWave(RandomSpawnInfo spawnInfo)
        {
            int index = 0;
            var enemies = new List<Enemy>();

            foreach (var info in spawnInfo.EnemySpawnInfos)
            {
                for (var i = 0; i < info.Count; i++)
                {
                    enemies.Add(Spawn(info.Type, spawnInfo.Points[index].position));
                    index = (index + 1) % spawnInfo.Points.Count;
                }
            }

            return enemies;
        }
        
        public List<Enemy> SpawnWave(List<FullEnemiesSpawnInfo> spawnInfos)
        {
            var enemies = new List<Enemy>();
            foreach (var pointInfo in spawnInfos)
            {
                foreach (var enemySpawnInfo in pointInfo.EnemySpawnInfos)
                {
                    for(int i = 0; i<enemySpawnInfo.Count; i++)
                        enemies.Add(Spawn(enemySpawnInfo.Type, pointInfo.Point.position));
                }
            }

            return enemies;
        }

        public Enemy Spawn(EnemyType type, Vector3 position)
        {
            var enemy = _enemyFactory.Get(type);

            if (!NavMesh.SamplePosition(position, out NavMeshHit hit, 1f, NavMesh.AllAreas))
                return null;
            
            enemy.transform.position = position;
            return enemy;
        }
    }
}