using System;
using UnityEngine;

namespace Enemies
{
    public class EnemyDetector : MonoBehaviour
    {
        [SerializeField] private bool _playerDetected;

        public event Action<bool> Detected;
        
        public bool PlayerDetected
        {
            get => _playerDetected;
            set
            {
                if (value && _playerDetected)
                    return;
                
                _playerDetected = value;
                Detected?.Invoke(_playerDetected);
            }
        }
    }
}
