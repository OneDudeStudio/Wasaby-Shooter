using System;
using System.Collections.Generic;
using UnityEngine;

namespace Enemies
{
    public class TestTrain : MonoBehaviour
    {
        [SerializeField] private List<Transform> _jumpPoints;
        [SerializeField] private Transform _spawnPoint;

        public List<Transform> JumpPoints => _jumpPoints;
        public Transform SpawnPoint => _spawnPoint;
    }
}