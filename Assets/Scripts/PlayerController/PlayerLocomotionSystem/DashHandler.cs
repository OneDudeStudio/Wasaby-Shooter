using System.Collections;
using UnityEngine;

namespace PlayerController.PlayerLocomotionSystem
{
    public class DashHandler : MonoBehaviour
    {
        public float dashForce;
        public float dashUpwardForce;
        public float maxDashYSpeed;
        public float dashDuration;
        public float dashCooldown;
        public float dashFov;
        
        [Header("Settings")]
        public bool useCameraForward = true;
        public bool allowAllDirections = true;
        public bool disableGravity;
        public bool resetVel = true;
        
        private float _dashCooldownTimer;
        private Vector3 _delayedForceToApply;

        private void Update()
        {
            if (_dashCooldownTimer > 0)
            {
                _dashCooldownTimer -= Time.deltaTime;
            }
        }

        public void TryDash(PlayerMovementAdvanced pm, Rigidbody rb, PlayerCam playerCam)
        {
            if (_dashCooldownTimer > 0)
            {
                return;
            }
            
            _dashCooldownTimer = dashCooldown;
            pm.IsDashing = true;
            pm.maxYSpeed = maxDashYSpeed;
            
            playerCam.DoFov(dashFov);

            var forwardT = useCameraForward ? playerCam.transform : playerCam.Orientation;

            var direction = GetDirection(forwardT, pm.HorizontalInput, pm.VerticalInput);

            var forceToApply = direction * dashForce + playerCam.Orientation.up * dashUpwardForce;

            if (disableGravity)
            {
                rb.useGravity = false;
            }
            _delayedForceToApply = forceToApply;

            StartCoroutine(DelayedDashForceCoroutine(rb, 0.025f));
            StartCoroutine(DelayedResetDashCoroutine(pm, rb, playerCam, dashDuration));
        }

        private IEnumerator DelayedDashForceCoroutine(Rigidbody rb, float delay)
        {
            yield return new WaitForSeconds(delay);
            DelayedDashForce(rb);
        }

        private void DelayedDashForce(Rigidbody rb)
        {
            if (resetVel)
            {
                rb.velocity = Vector3.zero;
            }

            rb.AddForce(_delayedForceToApply, ForceMode.Impulse);
        }

        private IEnumerator DelayedResetDashCoroutine(PlayerMovementAdvanced pm, Rigidbody rb, PlayerCam playerCam, float delay)
        {
            yield return new WaitForSeconds(delay);
            ResetDash(pm, rb, playerCam);
        }

        private void ResetDash(PlayerMovementAdvanced pm, Rigidbody rb, PlayerCam playerCam)
        {
            pm.IsDashing = false;
            pm.maxYSpeed = 0;

            playerCam.DoFov(85f);

            if (disableGravity)
            {
                rb.useGravity = true;
            }
        }

        private Vector3 GetDirection(Transform forwardT, float horizontalInput, float verticalInput)
        {
            Vector3 direction;

            if (allowAllDirections)
            {
                direction = forwardT.forward * verticalInput + forwardT.right * horizontalInput;
            }
            
            else
            {
                direction = forwardT.forward;
            }
            
            if (verticalInput == 0 && horizontalInput == 0)
            {
                direction = forwardT.forward;
            }

            return direction.normalized;
        }
    }
}