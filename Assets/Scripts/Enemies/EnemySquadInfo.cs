using System;
using System.Collections.Generic;
using UnityEngine;

namespace Enemies
{
    [Serializable]
    public class EnemySquadInfo
    {
        [SerializeField] private int _strikingEnemyCount;
        [SerializeField] private int _explodingEnemyCount;

        [SerializeField] private List<Transform> _points;

        private EnemySquad _squad;
        public EnemySquad Squad
        {
            get
            {
                if (_squad != null) 
                    return _squad;
                
                _squad = new EnemySquad();
                Squad.Add(EnemyType.Striking, _strikingEnemyCount);
                Squad.Add(EnemyType.Exploding, _explodingEnemyCount);
                return _squad;
            }
        }
        public List<Transform> Points => _points;
        public int EnemiesCount => _strikingEnemyCount + _explodingEnemyCount;
    }
}