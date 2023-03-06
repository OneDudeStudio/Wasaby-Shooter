using System;
using System.Collections;
using System.Collections.Generic;
using Enemies.Spawn;
using PlayerController;
using Railways.GeneratorsAndDestroyers;
using Railways.Trains;
using UnityEngine;
using Random = System.Random;

namespace Enemies.BattleSequence
{
    public class TrainSequence : MonoBehaviour, IBattleSequence
    {
        [SerializeField] private EnemySpawner _enemySpawner;
        
        [SerializeField] private DynamicGeneratorByController _dynamicGeneratorByController;
        [SerializeField] private Destroyer _destroyer;
        
        [SerializeField] private TargetDetector _targetDetector;
        [SerializeField ]private List<Transform> _landingPoints;

        [SerializeField, Range(1, 10)] private int _meleeEnemyMaxCount;
        [SerializeField, Range(0, 10)] private int _bombEnemyMaxCount;
        
        private IEnemyTarget _target;
        private ControlledTrain _train;

        private List<Enemy> _enemies;
        private int _enemyCount;

        private int _jumpPointIndex;

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

        private void OnEnable()
        {
            _destroyer.Destroyed += InitializeSequence;
        }

        private void OnDisable()
        {
            _destroyer.Destroyed -= InitializeSequence;
        }

        public void InitializeSequence()
        {
            GenerateTrain();
            //_destroyer.Destroyed += InitializeSequence;
            
            _train.Arrived += StartProcess;
            _train.Arrived += SpawnWave;
        }

        public void UpdateSequenceScenario()
        {
            _enemyCount--;
            if(_enemyCount == 0)
                FinishSequence();
        }
        
        public void FinishSequence()
        {
            FinishProcess();
        }

        private void GenerateTrain()
        {
            _train = _dynamicGeneratorByController.Generate().GetComponent<ControlledTrain>();
        }

        private void StartProcess()
        {
            _train.StopMove();
            _train.OpenDoors();
        }

        private void FinishProcess()
        {
            _train.Departure();
        }
        
        private void SpawnWave()
        {
            var random = new Random();
            
            int meleeEnemyCount = random.Next(1, _meleeEnemyMaxCount);
            int bombEnemyCount = random.Next(0, _bombEnemyMaxCount);

            _enemies = new List<Enemy>();
            _enemyCount = meleeEnemyCount + bombEnemyCount;
            
            var infos = new List<EnemySpawnInfo>
            {
                new EnemySpawnInfo(EnemyType.Melee, meleeEnemyCount),
                new EnemySpawnInfo(EnemyType.Bomb, bombEnemyCount)
            };

            Vector3 spawnPosition = _train.SpawnPoint.position;

            foreach (var spawnInfo in infos)
            {
                List<Enemy> enemies = _enemySpawner.SpawnWave(spawnInfo, spawnPosition);

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

            List<Transform> jumpPoints = _train.JumpPoints;
            _jumpPointIndex = new Random().Next(jumpPoints.Count - 1);
            
            foreach (var enemy in _enemies)
            {
                Transform jumpPoint = GetJumpPoint(jumpPoints);
                Transform landingPoint = GetLandingPoint(jumpPoint);

                ProceduralEnemyMovement movement = enemy.ProceduralMovement;
                movement.Jumped += UpdateSequenceScenario;
                movement.StartRoute(jumpPoint, landingPoint);
                
                yield return new WaitForSeconds(movementStartDelayInSeconds);
            }
        }

        private Transform GetJumpPoint(List<Transform> jumpPoints)
        {
            _jumpPointIndex = (_jumpPointIndex + 1) % jumpPoints.Count;
            return jumpPoints[_jumpPointIndex];
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