using System.Collections;
using System.Collections.Generic;
using Enemies.Spawn;
using PlayerController;
using UnityEngine;
using Random = System.Random;

namespace Enemies.BattleSequence
{
    public class TrainSequence : MonoBehaviour, IBattleSequence
    {
        [SerializeField] private EnemySpawner _enemySpawner;
        
        [SerializeField] private TargetDetector _targetDetector;
        [SerializeField ]private List<Transform> _landingPoints; 
       
        [SerializeField] private GameObject _trainPrefab;
        [SerializeField] private Transform _stopPoint;

        [SerializeField, Range(1, 10)] private int _meleeEnemyMaxCount;
        [SerializeField, Range(1, 10)] private int _bombEnemyMaxCount;
        
        private IEnemyTarget _target;
        private TestTrain _train;

        private List<Enemy> _enemies;
        private int _enemyCount;

        private void Awake()
        {
            _target = FindObjectOfType<PlayerManager>();
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.G))
            {
                InitializeSequence();
            }
        }

        public void InitializeSequence()
        {
            _train = Instantiate(_trainPrefab, _stopPoint.position, Quaternion.identity).GetComponent<TestTrain>();
            SpawnWave();
        }

        public void UpdateSequenceScenario()
        {
            _enemyCount--;
            if(_enemyCount == 0)
                FinishSequence();
        }

        public void FinishSequence()
        {
            Destroy(_train.gameObject);
        }

        private void SpawnWave()
        {
            var random = new Random();
            
            int meleeEnemyCount = random.Next(1, _meleeEnemyMaxCount);
            int bombEnemyCount = random.Next(1, _bombEnemyMaxCount);

            _enemies = new List<Enemy>();
            _enemyCount = meleeEnemyCount + bombEnemyCount;
            
            var infos = new List<EnemySpawnInfo>
            {
                new EnemySpawnInfo(EnemyType.Melee, meleeEnemyCount),
                new EnemySpawnInfo(EnemyType.Bomb, bombEnemyCount)
            };

            foreach (var spawnInfo in infos)
            {
                List<Enemy> enemies = _enemySpawner.SpawnWave(spawnInfo, _train.SpawnPoint.position);
                Initialize(enemies);
                _enemies.AddRange(enemies);
            }

            TryMoveEnemies();
        }

        private void Initialize(List<Enemy> enemies)
        {
            foreach (var enemy in enemies)
            {
                enemy.SetTarget(_target);
                enemy.Damaged += () => _targetDetector.PlayerDetected = true;
                
                ProceduralEnemyMovement movement = enemy.ProceduralMovement;
                movement.enabled = true;
            }
        }

        private void TryMoveEnemies()
        {
            if (_enemies == null)
                return;

            StartCoroutine(nameof(StartProceduralMovement));
        }

        private IEnumerator StartProceduralMovement()
        {
            const float movementStartDelayInSeconds = 0.01f;
            
            foreach (var enemy in _enemies)
            {
                Transform jumpPoint = GetRandomJumpPoint(_train.JumpPoints);
                Transform landingPoint = GetLandingPoint(jumpPoint);

                ProceduralEnemyMovement movement = enemy.ProceduralMovement;
                movement.Jumped += UpdateSequenceScenario;
                movement.StartRoute(jumpPoint, landingPoint);
                
                yield return new WaitForSeconds(movementStartDelayInSeconds);
            }
        }

        private Transform GetRandomJumpPoint(List<Transform> jumpPoints)
        {
            var random = new Random();
            return jumpPoints[random.Next(jumpPoints.Count - 1)];
        }
        private Transform GetLandingPoint(Transform jumpPoint)
        {
            Transform landingPoint = _landingPoints[0];
            float minDistance = Vector3.Distance(jumpPoint.position, landingPoint.position);
                
            foreach (Transform currentLandingPoint in _landingPoints)
            {
                var distance = Vector3.Distance(jumpPoint.position, currentLandingPoint.position);
                if (distance <= minDistance)
                {
                    minDistance = distance;
                    landingPoint = currentLandingPoint;
                }
            }

            return landingPoint;
        }
    }
}