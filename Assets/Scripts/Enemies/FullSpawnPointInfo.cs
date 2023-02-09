using System;
using UnityEngine;
using UnityEngine.Serialization;

namespace Enemies
{
    [Serializable]
    public class FullSpawnPointInfo
    {
        [SerializeField] private Transform _point;
        [SerializeField] private int _meleeEnemiesEnemiesCount;
        [SerializeField] private int _bombEnemiesCountCount;

        public Transform Point => _point;
        public int MeleeEnemiesCount => _meleeEnemiesEnemiesCount;
        public int BombEnemiesCount => _bombEnemiesCountCount;
    }
}