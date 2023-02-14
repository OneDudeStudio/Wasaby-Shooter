using System;
using UnityEngine;

namespace Enemies
{
    [Serializable]
    public class EnemySpawnInfo
    {
        [SerializeField] private EnemyType _type;
        [SerializeField] private int _count;

        public EnemyType Type => _type;
        public int Count => _count;
    }
}