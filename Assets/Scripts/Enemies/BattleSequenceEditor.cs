using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace Enemies
{
    [CustomEditor(typeof(BattleSequence))]
    public class BattleSequenceEditor : Editor
    {
        [SerializeField] private List<EnemyWaveInfo> _waves;
        [SerializeField] private EnemySpawner _enemySpawner;
        [SerializeField] private EnemyDetector _enemyDetector;
        [SerializeField] private List<GameObject> _obstacles;

        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
        }
    }
    
    
}