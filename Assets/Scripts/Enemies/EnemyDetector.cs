using UnityEngine;

namespace Enemies
{
    public class EnemyDetector : MonoBehaviour
    {
        [SerializeField] private bool _playerDetected;

        public bool PlayerDetected
        {
            get => _playerDetected;
            set
            {
                if (value && _playerDetected)
                    return;
                _playerDetected = value;
            }
        }
    }
}
