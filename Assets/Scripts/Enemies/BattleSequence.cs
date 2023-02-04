using System.Collections.Generic;
using PlayerController;
using UnityEngine;

namespace Enemies
{
    public class BattleSequence : MonoBehaviour
    {
        [SerializeField] private List<EnemyWaveInfo> _infos;
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
            
            SpawnSquad();
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

            if (_currentSquadIndex != _infos.Count)
                SpawnSquad();
            else
                FinishBattle();
        }

        private void SpawnSquad()
        {
            EnemyWaveInfo info = _infos[_currentSquadIndex];
            
            EnemyWave wave = info.Wave;
            List<Transform> points = info.Points;
            _currentSquadEnemiesCount = info.EnemiesCount;

            var enemies = _enemySpawner.SpawnSquad(wave, points);
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