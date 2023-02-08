using System.Collections.Generic;
using System.Linq;
using PlayerController;
using UnityEngine;
using UnityEngine.Serialization;

namespace Enemies
{
    public class BattleSequence : MonoBehaviour
    {
        [SerializeField] private List<EnemyWaveInfo> _waves;
        [SerializeField] private EnemySpawner _enemySpawner;
        [SerializeField] private EnemyDetector _enemyDetector;
        [SerializeField] private List<GameObject> _obstacles;

        private bool _initialized;
        private int _currentSquadIndex;
        private int _currentSquadEnemiesCount;

        private void OnTriggerEnter(Collider other)
        {
            if(other.TryGetComponent(out PlayerManager _) && !_initialized)
                StartBattle();
        }
        
        private void StartBattle()
        {
            _initialized = true;
            _enemyDetector.PlayerDetected = false;
            _enemyDetector.Detected += SetObstacles;
            
            SpawnWave();
        }

        private void FinishBattle()
        {
            _enemyDetector.PlayerDetected = false;
        }
        
        private void ChangeScenario()
        {
            _currentSquadEnemiesCount--;

            if (_currentSquadEnemiesCount != 0) 
                return;

            if (_currentSquadIndex != _waves.Count)
                SpawnWave();
            else
                FinishBattle();
        }

        private void SpawnWave()
        {
            EnemyWaveInfo info = _waves[_currentSquadIndex];
            EnemyWave wave = info.Wave;

            List<Transform> points = info.Points;
            
            var random = new System.Random();
            points = points.OrderBy(point => random.Next()).ToList();
            
            _currentSquadEnemiesCount = info.EnemiesCount;

            var enemies = _enemySpawner.SpawnWave(wave, points);
            InitializeEnemies(enemies);
            
            _currentSquadIndex++;
        }

        private void InitializeEnemies(List<Enemy> enemies)
        {
            foreach (var enemy in enemies)
            {
                enemy.Died += ChangeScenario;
                enemy.Damaged += () => _enemyDetector.PlayerDetected = true;
            }
        }

        private void SetObstacles(bool state)
        {
            _obstacles.ForEach(obstacle => obstacle.SetActive(state));
        }
    }
}