using System.Collections.Generic;

namespace Enemies
{
    public class EnemyWave
    {
        private Dictionary<EnemyType, int> _enemies;

        public EnemyWave()
        {
            _enemies = new Dictionary<EnemyType, int>();
        }
        
        public void Add(EnemyType type, int count)
        {
            if (_enemies.ContainsKey(type))
                _enemies[type] = count;
            else
                _enemies.Add(type, count);
        }

        public Dictionary<EnemyType, int> GetEnemies() => _enemies;
    }
}