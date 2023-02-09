using System.Collections.Generic;
using System.Linq;
using PlayerController;
using UnityEditor;
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
            _enemyDetector.Detected += TrySetObstacles;
            
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
            
            TrySpawnExtraEnemies(info);
            
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

        private void TrySetObstacles(bool state)
        {
            foreach (var obstacle in _obstacles.Where(obstacle => obstacle))
                obstacle.SetActive(state);
        }

        private void TrySpawnExtraEnemies(EnemyWaveInfo info)
        {
            List<FullSpawnPointInfo> extraPoints = info.ExtraPoints;
            if(extraPoints.Count == 0)
                return;

            var enemies = new List<Enemy>();
            foreach (var extraPoint in extraPoints)
            {
                _currentSquadEnemiesCount += extraPoint.MeleeEnemiesCount + extraPoint.BombEnemiesCount;
                
                for (int i = 0; i < extraPoint.MeleeEnemiesCount; i++)
                    enemies.Add(_enemySpawner.Spawn(EnemyType.Melee, extraPoint.Point.position));
                for (int i = 0; i < extraPoint.BombEnemiesCount; i++)
                    enemies.Add(_enemySpawner.Spawn(EnemyType.Bomb, extraPoint.Point.position));
            }
            InitializeEnemies(enemies);
        }
    }
}