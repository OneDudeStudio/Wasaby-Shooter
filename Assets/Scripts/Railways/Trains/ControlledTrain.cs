using System;
using UnityEngine;

namespace Railways.Trains
{
    public class ControlledTrain : Train
    {
        public event Action Arrive;

        [SerializeField] private Animator _doorsComponentAnimator;
        

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Finish"))
            {
                print("invoke");
                Arrive?.Invoke();
            }
        }

        public void StopMove()
        {
            Debug.Log(TryGetComponent<Move>(out var train));
            if (TryGetComponent(out Move move))
            {
                move.enabled = false;
            }

            if (TryGetComponent(out TrainCollision collider))
            {
                collider.enabled = false;
            }
        }

        public void StartMove()
        {
            Debug.Log(TryGetComponent<Move>(out var train));
            if (TryGetComponent(out Move move))
            {
                move.enabled = true;
            }
            
            if (TryGetComponent(out TrainCollision collider))
            {
                collider.enabled = true;
            }
        }
    
        public void OpenDoors()
        {
            _doorsComponentAnimator.SetTrigger("openingDoors");
        }

        public void CloseDoors()
        {
            _doorsComponentAnimator.SetTrigger("closingDoors");
        }

    
    }
}

