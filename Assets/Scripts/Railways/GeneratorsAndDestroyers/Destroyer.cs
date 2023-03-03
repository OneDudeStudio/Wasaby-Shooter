using System;
using Railways.Trains;
using UnityEngine;

namespace Railways.GeneratorsAndDestroyers
{
    public class Destroyer : MonoBehaviour
    {
        public event Action Destroyed;

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent<Move>(out var train))
            {
                Destroy(other.gameObject);
                Destroyed?.Invoke();
            }
        }
    }
}