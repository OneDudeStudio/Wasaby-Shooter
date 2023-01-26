using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portal : MonoBehaviour
{
    [SerializeField] private Portal _otherPortal;

    [SerializeField] private Camera _portalCamera;

    private void Start()
    {
        // create render texture
        _otherPortal._portalCamera.targetTexture = new RenderTexture(Screen.width, Screen.height, 24);
        GetComponent<MeshRenderer>().sharedMaterial.mainTexture = _otherPortal._portalCamera.targetTexture;
    }

    void Update()
    {
        // Transforms portal camera position like player camera position
        
        // get position player camera relatively position other portal 
        Vector3 lookerPosition = _otherPortal.transform.worldToLocalMatrix.MultiplyPoint3x4(Camera.main.transform.position);
        lookerPosition = new Vector3(-lookerPosition.x, lookerPosition.y, -lookerPosition.z);
        // set local position for portal camera
        _portalCamera.transform.localPosition = lookerPosition;
        
        // Transforms portal camera rotation like player camera rotation
        
        // get difference between two portals rotation
        //Quaternion difference = transform.rotation * Quaternion.Inverse(_otherPortal.transform.rotation * Quaternion.Euler(0, 180, 0));
        // set rotation for portal camera
        //_portalCamera.transform.rotation = difference * Camera.main.transform.rotation;
        
        // Clipping
        _portalCamera.nearClipPlane = lookerPosition.magnitude;
    }
}
