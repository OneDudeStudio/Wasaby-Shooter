using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrainCollision : MonoBehaviour
{
    private void OnCollisionEnter(UnityEngine.Collision collision)
    {
        Debug.Log("Collision!!! " + collision.gameObject.name);
        collision.gameObject.transform.Translate(new Vector3(0.2f, 0.3f, 1) * 5 * Time.deltaTime);
    }
}
