using System.Collections.Generic;
using System.Linq;
using Enemies.Spawn;
using PlayerController;
using UnityEngine;

namespace Enemies.BattleSequence
{
    public class BattleSequence : MonoBehaviour, IBattleSequence
    {
        [SerializeField] private List<EnemyWaveInfo> _waves;
        [SerializeField] private EnemySpawner _enemySpawner;
        [SerializeField] private TargetDetector _targetDetector;
        [SerializeField] private List<GameObject> _obstacles;

        private bool _initialized;
        private int _currentWaveIndex;
        private int _currentWaveEnemiesCount;

        private IEnemyTarget _target;

        private void Awake()
        {
            _target = FindObjectOfType<PlayerManager>();
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out PlayerManager _) && !_initialized)
            {
                InitializeSequence();                
            }
        }
        
        
        public void InitializeSequence()
        {
            _initialized = true;
            _targetDetector.PlayerDetected = false;
            _targetDetector.Detected += TrySetObstacles;
            
            SpawnWave();
        }

        public void FinishSequence()
        {
            _targetDetector.PlayerDetected = false;
        }
        
        public void UpdateSequenceScenario()
        {
            _currentWaveEnemiesCount--;

            if (_currentWaveEnemiesCount != 0)
            {
                return;
            }

            if (_currentWaveIndex != _waves.Count)
            {
                SpawnWave();                
            }
            else
            {
                 FinishSequence();               
            }
        }

        private void SpawnWave()
        {
            EnemyWaveInfo info = _waves[_currentWaveIndex];

            TrySpawnEnemiesInRandomPoints(info);
            TrySpawnExtraEnemies(info);
            
            _currentWaveIndex++;
        }

        private void TrySpawnEnemiesInRandomPoints(EnemyWaveInfo info)
        {
            RandomSpawnInfo spawnInfo = info.RandomSpawnInfo;

            foreach (EnemySpawnInfo enemySpawnInfo in spawnInfo.EnemySpawnInfos)
            {
                _currentWaveEnemiesCount += enemySpawnInfo.Count;
            }
            
            spawnInfo.RandomizePoints();
            
            List<Enemy> enemies = _enemySpawner.SpawnWave(spawnInfo);
            InitializeEnemies(enemies);
        }
        
        private void TrySpawnExtraEnemies(EnemyWaveInfo info)
        {
            List<FullEnemiesSpawnInfo> pointInfos = info.FullSpawnInfos;
            
            if(pointInfos.Count == 0)
                return;

            var enemies = _enemySpawner.SpawnWave(pointInfos);

            foreach (FullEnemiesSpawnInfo pointInfo in pointInfos)
            { 
                foreach (EnemySpawnInfo enemySpawnInfo in pointInfo.EnemySpawnInfos)
                {
                    _currentWaveEnemiesCount += enemySpawnInfo.Count;                
                }               
            }
            
            InitializeEnemies(enemies);
        }
        
        private void InitializeEnemies(List<Enemy> enemies)
        {
            foreach (Enemy enemy in enemies)
            {
                enemy.SetTarget(_target);
                enemy.Died += UpdateSequenceScenario;
                enemy.Damaged += () => _targetDetector.PlayerDetected = true;
            }
        }

        private void TrySetObstacles(bool state)
        {
            foreach (GameObject obstacle in _obstacles.Where(obstacle => obstacle))
            {
                obstacle.SetActive(state);                
            }
        }
    }
}