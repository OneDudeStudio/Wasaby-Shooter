using System;
using System.Collections.Generic;
using UnityEngine;

namespace Railways.Trains
{
    public class ControlledTrain : Train
    {
        [SerializeField] private List<Transform> _jumpPoints;
        [SerializeField] private Transform _spawnPoint;

        public List<Transform> JumpPoints => _jumpPoints;
        public Transform SpawnPoint => _spawnPoint;

        private bool _arrived;

        public event Action Arrived;

        private Animator _animator;

        private void Start()
        {
            _animator = GetComponent<Animator>();
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Finish") && !_arrived)
            {
                _arrived = true;
                Arrived?.Invoke();
            }
        }

        public void Departure()
        {
            CloseDoors();
        }


        public void StopMove()
        {
            if (TryGetComponent(out Move move))
            {
                move.enabled = false;
            }

            if (TryGetComponent(out TrainCollision trainCollision))
            {
                trainCollision.enabled = false;
            }

            foreach (Collider childCollider in GetComponentsInChildren<Collider>(true))
            {
                childCollider.enabled = true;
            }
            
            if (TryGetComponent(out Collider collider))
            {
                collider.enabled = false;
            }
        }

        public void OpenDoors()
        {
            _animator.SetTrigger("openDoors");
        }

        private void CloseDoors()
        {
            _animator.SetTrigger("closeDoors");
        }

        private void OnCloseDoorsAnimationOver()
        {
            StartMove();
        }

        private void StartMove()
        {
            if (TryGetComponent(out Move move))
            {
                move.enabled = true;
            }

            if (TryGetComponent(out TrainCollision trainCollision))
            {
                trainCollision.enabled = true;
            }

            foreach (Collider childCollider in GetComponentsInChildren<Collider>(true))
            {
                childCollider.enabled = false;
            }
            
            if (TryGetComponent(out Collider collider))
            {
                collider.enabled = true;
            }
        }
    }
}