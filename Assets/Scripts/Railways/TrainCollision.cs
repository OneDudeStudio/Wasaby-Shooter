using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrainCollision : MonoBehaviour
{
    [SerializeField] float _pushForce = 5;
    private Ragdoll _otherObjectRagdoll;
    private void OnCollisionEnter(UnityEngine.Collision collision)
    {
        _otherObjectRagdoll = collision.gameObject.GetComponent<Ragdoll>();
        
        Debug.Log("Collision!!! " + collision.gameObject.name);
        
        //TakeDamage();
        //_otherObjectRagdoll.ActivateRagdoll();
        PushObject(GetComponent<Move>().directionPoint.transform.position);
        //_otherObjectRagdoll.DeactivateRagdoll();
        
    }

    private void PushObject(Vector3 direction)
    {
        direction.y = 1;
        _otherObjectRagdoll.ApplyForce(direction * _pushForce);
    }

    private void TakeDamage(float amount, Vector3 direction)
    {
        // TO DO :: currentHealthOfPlayer -= amount
        // TO DO :: if (currentHealthOfPlayer <= 0.0f) Die();
        
    }
    
}
