using System;
using System.Collections.Generic;
using UnityEngine;

namespace Enemies.Spawn
{
    [Serializable]
    public class EnemyWaveInfo
    {
        [SerializeField] private RandomSpawnInfo _randomSpawnInfo;
        [SerializeField] private List<FullEnemiesSpawnInfo> _fullSpawnInfos;
        
        public RandomSpawnInfo RandomSpawnInfo => _randomSpawnInfo;
        public List<FullEnemiesSpawnInfo> FullSpawnInfos => _fullSpawnInfos;
    }
}