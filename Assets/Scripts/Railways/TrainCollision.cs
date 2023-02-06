using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class TrainCollision : MonoBehaviour
{
    [SerializeField] private float _pushForce = 5;
    [SerializeField] private float _pushHeight = 5;
    [SerializeField] private float _damage = 5;
    [SerializeField] private AudioSource _soundForCollision;
    
    private Ragdoll _otherObjectRagdoll;
    private void OnCollisionEnter(UnityEngine.Collision collision)
    {
        _otherObjectRagdoll = collision.gameObject.GetComponent<Ragdoll>();
        
        Debug.Log("Collision!!! " + collision.gameObject.name);
        
        PushObject(GetComponent<Move>().directionPoint.transform.position);
        //PushObject(transform.position);
        PlaySound();
        TakeDamage(collision.gameObject, _damage);
        
    }
    
    private void PlaySound()
    {
        _soundForCollision.pitch = Random.Range(0.9f, 1.1f);
        _soundForCollision.Play();
    }

    private void PushObject(Vector3 direction)
    {
        _otherObjectRagdoll.ActivateRagdoll();
        
        direction.y = _pushHeight;
        _otherObjectRagdoll.ApplyForce(direction * _pushForce);
        
    }

    private void TakeDamage(GameObject damagedObject,float amount)
    {
        IApplyableDamage iApplyableDamage = damagedObject.GetComponent<IApplyableDamage>();
        iApplyableDamage.TryApplyDamage(amount);
    }
    
}
