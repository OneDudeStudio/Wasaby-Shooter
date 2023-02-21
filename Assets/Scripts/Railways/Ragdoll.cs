using System;
using UnityEngine;
using Random = UnityEngine.Random;

// attach to first person
namespace Railways
{
    public class Ragdoll : MonoBehaviour
    {
        [SerializeField] private AudioSource _soundForCollision;

        private Rigidbody[] _rigidBodies;
        private Rigidbody _rigidbody;
        private Animator _animator;

        private void Start()
        {
            _rigidBodies = GetComponentsInChildren<Rigidbody>();
            _rigidbody = GetComponent<Rigidbody>();
            _animator = GetComponent<Animator>();
        }

        public void DeactivateRagdoll()
        {
            if (_rigidbody != null) _rigidbody.isKinematic = true;
            try
            {
                foreach (var rigidBody in _rigidBodies)
                {
                    rigidBody.isKinematic = true;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }

            if (_animator != null) _animator.enabled = true;
        }

        public void ActivateRagdoll()
        {
            if (_rigidbody != null) _rigidbody.isKinematic = false;
            try
            {
                foreach (var rigidBody in _rigidBodies)
                {
                    rigidBody.isKinematic = false;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }

            if (_animator != null) _animator.enabled = false;
        }

        public void ApplyForce(Vector3 force)
        {
            //Debug.Log(force);
            //Debug.DrawRay(transform.position, force, Color.red, 100.0f); 
            transform.Translate(Vector3.up);
            _rigidbody.AddForce(force, ForceMode.VelocityChange);
        }

        public void PlaySound()
        {
            _soundForCollision.pitch = Random.Range(0.9f, 1.1f);
            _soundForCollision.Play();
        }
    }
}