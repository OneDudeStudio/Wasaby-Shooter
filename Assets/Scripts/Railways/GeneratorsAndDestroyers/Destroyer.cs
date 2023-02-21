using System;
using UnityEngine;

namespace Railways.GeneratorsAndDestroyers
{
    public class Destroyer : MonoBehaviour
    {
        public event Action OnDestroy;

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent<Move>(out var train))
            {
                Destroy(other.gameObject);
                OnDestroy?.Invoke();
            }
        }
    }
}