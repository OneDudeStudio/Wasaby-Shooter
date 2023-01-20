using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portal : MonoBehaviour
{
    [SerializeField] Portal _otherPortal;

    [SerializeField] private Camera _portalCamera;
   
    void Start()
    {
        
    }

    
    void Update()
    {
        // Transforms portal camera position like player camera position
        
        // get position player camera relatively position other portal 
        Vector3 lookerPosition = _otherPortal.transform.worldToLocalMatrix.MultiplyPoint3x4(Camera.main.transform.position);
        // set local position for portal camera
        _portalCamera.transform.localPosition = -lookerPosition;
    }
}
