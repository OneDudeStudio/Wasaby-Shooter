using System;
using DG.Tweening;
using UnityEngine;

namespace PlayerController.PlayerLocomotionSystem
{
    public class PlayerCam : MonoBehaviour
    {
        [Header("Base Settings")]
        [SerializeField] private Camera _camera;
        [SerializeField] private float _sensX;
        [SerializeField] private float _sensY;
        [SerializeField] private float _multiplier;
        [SerializeField] private Transform _orientation;
        [SerializeField] private Transform _cameraHolder;

        [Header("Advanced Fov Settings")]
        [SerializeField] private bool _useFluentFov;
        [SerializeField] private Rigidbody _rb;
        [SerializeField] private float _minMovementSpeed;
        [SerializeField] private float _maxMovementSpeed;
        [SerializeField] private float _minFov;
        [SerializeField] private float _maxFov;

        private float xRotation;
        private float yRotation;

        //add HeadBob

        public Transform Orientation => _orientation;
        
        public void RotateCamera(float xInput, float yInput)
        {
            float mouseX = xInput * _sensX;
            float mouseY = yInput * _sensY;
            yRotation += mouseX * _multiplier;
            xRotation -= mouseY * _multiplier;
            xRotation = Mathf.Clamp(xRotation, -90f, 90f);
            
            _cameraHolder.localRotation = Quaternion.Euler(xRotation, yRotation, 0);
            _orientation.localRotation = Quaternion.Euler(0, yRotation, 0);

            if (_useFluentFov)
            {
                HandleFov();
            }
        }

        private void HandleFov()
        {
            float moveSpeedDif = _maxMovementSpeed - _minMovementSpeed;
            float fovDif = _maxFov - _minFov;

            float rbFlatVel = new Vector3(_rb.velocity.x, 0f, _rb.velocity.z).magnitude;
            float currMoveSpeedOvershoot = rbFlatVel - _minMovementSpeed;
            float currMoveSpeedProgress = currMoveSpeedOvershoot / moveSpeedDif;

            float fov = (currMoveSpeedProgress * fovDif) + _minFov;

            float currFov = _camera.fieldOfView;

            float lerpedFov = Mathf.Lerp(fov, currFov, Time.deltaTime * 200);

            _camera.fieldOfView = lerpedFov;
        }

        public void DoFov(float endValue)
        {
            _camera.DOFieldOfView(endValue, 0.25f);
        }
    }
}