using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Railways.Trains
{
    public class ControlledTrain : Train
    {
        public event Action OnArrive;

        private Animator _animator;

        private void Start()
        {
            _animator = GetComponent<Animator>();
            _animator.enabled = false;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Finish"))
            {
                OnArrive?.Invoke();
            }
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

            if (TryGetComponent(out Collider collider))
            {
                collider.enabled = false;
            }

            _animator.enabled = true;
        }

        public void StartMove()
        {
            _animator.enabled = false;

            if (TryGetComponent(out Move move))
            {
                move.enabled = true;
            }

            if (TryGetComponent(out TrainCollision trainCollision))
            {
                trainCollision.enabled = true;
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

        public void CloseDoors()
        {
            _animator.SetTrigger("closeDoors");
        }
    }
}