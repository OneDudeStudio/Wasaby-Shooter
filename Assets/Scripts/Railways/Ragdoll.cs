using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

// attach to first person
public class Ragdoll : MonoBehaviour
{
    private Rigidbody[] _rigidBodies;
    private Rigidbody _rigidbody;
    private Animator _animator;

    void Start()
    {
        _rigidBodies = GetComponentsInChildren<Rigidbody>();
        _rigidbody = GetComponent<Rigidbody>();
        _animator = GetComponent<Animator>();
    }

    public void DeactivateRagdoll()
    {
        if(_rigidbody != null) _rigidbody.isKinematic = true;
        Debug.Log("Ragdoll deactivate");
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
        if(_rigidbody != null) _rigidbody.isKinematic = false;
        Debug.Log("Ragdoll activate");
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
        _rigidbody.AddForce(force, ForceMode.VelocityChange);
    }
}