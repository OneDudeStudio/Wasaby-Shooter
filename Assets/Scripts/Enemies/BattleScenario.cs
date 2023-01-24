using System.Collections.Generic;
using PlayerController;
using UnityEngine;

namespace Enemies
{
    public class BattleScenario : MonoBehaviour
    {
        [SerializeField] private List<EnemySquadInfo> _infos;
        [SerializeField] private EnemySpawner _enemySpawner;
        [SerializeField] private EnemyDetector _enemyDetector;
        
        private bool _started;
        private int _currentSquadIndex;
        private int _currentSquadEnemiesCount;

        private void OnTriggerEnter(Collider other)
        {
            if(other.TryGetComponent(out PlayerManager _) && !_started)
                StartScenario();
        }
        
        private void StartScenario()
        {
            _enemyDetector.PlayerDetected = false;
            _started = true;
            SpawnSquad();
        }

        public void ChangeScenario()
        {
            _currentSquadEnemiesCount--;

            if (_currentSquadEnemiesCount != 0) 
                return;

            if (_currentSquadIndex != _infos.Count)
                SpawnSquad();
            else
                _enemyDetector.PlayerDetected = false;
        }

        private void SpawnSquad()
        {
            EnemySquadInfo info = _infos[_currentSquadIndex];
            
            EnemySquad squad = info.Squad;
            List<Transform> points = info.Points;
            _currentSquadEnemiesCount = info.EnemiesCount;

            var enemies = _enemySpawner.SpawnSquad(squad, points);
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
    }
}