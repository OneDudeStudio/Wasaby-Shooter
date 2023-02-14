using System;
using System.Collections.Generic;
using UnityEngine;

namespace Enemies
{
    [Serializable]
    public class FullEnemiesSpawnInfo
    {
        [SerializeField] private Transform _point;
        [SerializeField] private List<EnemySpawnInfo> _enemySpawnInfos;

        public Transform Point => _point;
        public List<EnemySpawnInfo> EnemySpawnInfos => _enemySpawnInfos;
    }
}