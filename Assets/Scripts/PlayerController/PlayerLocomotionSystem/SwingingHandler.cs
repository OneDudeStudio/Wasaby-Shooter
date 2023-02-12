using UnityEngine;

namespace PlayerController.PlayerLocomotionSystem
{
    public class SwingingHandler : MonoBehaviour
    {
        [Header("General Settings")]
        [SerializeField] private LineRenderer _lineRenderer;
        [SerializeField] private Transform _gunTip;
        [SerializeField] private LayerMask _whatIsGrappleable;

        [Header("Swinging Settings")]
        [SerializeField] private float _horizontalThrustForce;
        [SerializeField] private float _forwardThrustForce;
        [SerializeField] private float _extendCableSpeed;
        [SerializeField] private float _predictionSphereCastRadius;
        [SerializeField] private Transform _predictionPoint;

        private float _maxSwingDistance = 25f;
        private Vector3 _swingPoint;
        private SpringJoint _joint;
        private Vector3 _currentGrapplePosition;
        private RaycastHit _predictionHit;
        private Vector3 jointOriginalPos;

        public void CheckForSwingPoints(PlayerCam playerCam)
        {
            if (_joint != null) return;

            var playerCamTransform = playerCam.transform;
            Physics.SphereCast(playerCamTransform.position, _predictionSphereCastRadius, playerCamTransform.forward,
                out var sphereCastHit, _maxSwingDistance, _whatIsGrappleable);

            var camTransform = playerCam.transform;
            Physics.Raycast(camTransform.position, camTransform.forward,
                out var raycastHit, _maxSwingDistance, _whatIsGrappleable);

            Vector3 realHitPoint;

            if (raycastHit.point != Vector3.zero)
            {
                realHitPoint = raycastHit.point;
            }

            else if (sphereCastHit.point != Vector3.zero)
            {
                realHitPoint = sphereCastHit.point;
            }

            else
            {
                realHitPoint = Vector3.zero;
            }

            if (realHitPoint != Vector3.zero)
            {
                _predictionPoint.gameObject.SetActive(true);
                _predictionPoint.position = realHitPoint;
            }

            else
            {
                _predictionPoint.gameObject.SetActive(false);
            }

            _predictionHit = raycastHit.point == Vector3.zero ? sphereCastHit : raycastHit;
        }

        public void StartSwing(PlayerMovementAdvanced playerMovementAdvanced, Transform player)
        {
            if (_predictionHit.point == Vector3.zero) return;

            if (GetComponent<Grappling>() != null)
            {
                GetComponent<Grappling>().StopGrapple();
            }

            playerMovementAdvanced.ResetRestrictions();

            playerMovementAdvanced.IsSwinging = true;

            _swingPoint = _predictionHit.point;
            _joint = player.gameObject.AddComponent<SpringJoint>();
            _joint.autoConfigureConnectedAnchor = false;
            _joint.connectedAnchor = _swingPoint;

            var distanceFromPoint = Vector3.Distance(player.position, _swingPoint);

            _joint.maxDistance = distanceFromPoint * 0.8f;
            _joint.minDistance = distanceFromPoint * 0.25f;
            _joint.spring = 4.5f;
            _joint.damper = 7f;
            _joint.massScale = 4.5f;
            _lineRenderer.positionCount = 2;
            _currentGrapplePosition = _gunTip.position;
        }

        public void StopSwing(PlayerMovementAdvanced playerMovementAdvanced)
        {
            playerMovementAdvanced.IsSwinging = false;

            _lineRenderer.positionCount = 0;

            Destroy(_joint);
        }

        public void InAirGearMovementController(Rigidbody rigidbody, PlayerCam playerCam)
        {
            if (_joint == null)
            {
                return;
            }

            if (Input.GetKey(KeyCode.D))
            {
                rigidbody.AddForce(playerCam.Orientation.right * _horizontalThrustForce * Time.deltaTime);
            }

            if (Input.GetKey(KeyCode.A))
            {
                rigidbody.AddForce(-playerCam.Orientation.right * _horizontalThrustForce * Time.deltaTime);
            }

            if (Input.GetKey(KeyCode.W))
            {
                rigidbody.AddForce(playerCam.Orientation.forward * _horizontalThrustForce * Time.deltaTime);
            }

            if (Input.GetKey(KeyCode.Space))
            {
                var position = transform.position;
                var directionToPoint = _swingPoint - position;
                rigidbody.AddForce(directionToPoint.normalized * _forwardThrustForce * Time.deltaTime);
                var distanceFromPoint = Vector3.Distance(position, _swingPoint);
                _joint.maxDistance = distanceFromPoint * 0.8f;
                _joint.minDistance = distanceFromPoint * 0.25f;
            }

            if (Input.GetKey(KeyCode.S))
            {
                var extendedDistanceFromPoint = Vector3.Distance(transform.position, _swingPoint) + _extendCableSpeed;
                _joint.maxDistance = extendedDistanceFromPoint * 0.8f;
                _joint.minDistance = extendedDistanceFromPoint * 0.25f;
            }

        }

        public void DrawRope()
        {
            if (!_joint)
            {
                return;
            }

            _currentGrapplePosition = Vector3.Lerp(_currentGrapplePosition, _swingPoint, Time.deltaTime * 8f);
            _lineRenderer.SetPosition(0, _gunTip.position);
            _lineRenderer.SetPosition(1, _currentGrapplePosition);
        }
    }
}