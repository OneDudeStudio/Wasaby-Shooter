using System;
using System.Collections.Generic;
using UnityEngine;

namespace Enemies
{
    [Serializable]
    public class EnemyWaveInfo
    {
        [SerializeField] private int _meleeEnemyCount;
        [SerializeField] private int _bombEnemyCount;
        [SerializeField] private List<Transform> _points;
        [SerializeField] private List<FullSpawnPointInfo> _extraPoints;

        private EnemyWave wave;
        public EnemyWave Wave
        {
            get
            {
                if (wave != null) 
                    return wave;
                
                wave = new EnemyWave();
                Wave.Add(EnemyType.Melee, _meleeEnemyCount);
                Wave.Add(EnemyType.Bomb, _bombEnemyCount);
                return wave;
            }
        }
        public List<Transform> Points => _points;
        public List<FullSpawnPointInfo> ExtraPoints => _extraPoints;

        public int EnemiesCount => _meleeEnemyCount + _bombEnemyCount;
    }
}