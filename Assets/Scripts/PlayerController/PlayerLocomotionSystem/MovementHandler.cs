using System.Collections;
using UnityEngine;

namespace PlayerController.PlayerLocomotionSystem
{
    public class MovementHandler : MonoBehaviour
    {
        public float walkSpeed;
        public float sprintSpeed;
        public float slideSpeed;
        public float swingSpeed;
        public float vaultSpeed;
        public float airMinSpeed;
        public float speedIncreaseMultiplier;
        public float slopeIncreaseMultiplier;
        public float groundDrag;
        public float airMultiplier;
        public float crouchMultiplier;


        private Vector3 moveDirection;
        private RaycastHit slopeHit;

        public void MovePlayer(PlayerMovementAdvanced pm, PlayerCam playerCam, Rigidbody rigidbody)
        {
            if (pm.IsRestricted)
            {
                return;
            }

            moveDirection = playerCam.Orientation.forward * pm.VerticalInput + playerCam.Orientation.right * pm.HorizontalInput;

            if (OnSlope(pm) && !pm.IsExitingSlope)
            {
                rigidbody.AddForce(GetSlopeMoveDirection(moveDirection) * (pm.MoveSpeed * 20f), ForceMode.Force);

                if (rigidbody.velocity.y > 0) rigidbody.AddForce(Vector3.down * 80f, ForceMode.Force);
            }

            else if (pm.IsGrounded)
            {
                rigidbody.AddForce(moveDirection.normalized * (pm.MoveSpeed  * 10f), ForceMode.Force);
            }

            else if (!pm.IsGrounded)
            {
                rigidbody.AddForce(moveDirection.normalized * (pm.MoveSpeed  * 10f * airMultiplier),
                    ForceMode.Force);
            }

        }

        public IEnumerator SmoothlyLerpMoveSpeed(PlayerMovementAdvanced pm)
        {
            float time = 0;
            var difference = Mathf.Abs(pm.DesiredMoveSpeed - pm.MoveSpeed);
            var startValue = pm.MoveSpeed;

            while (time < difference)
            {
                pm.MoveSpeed = Mathf.Lerp(startValue, pm.DesiredMoveSpeed, time / difference);

                if (OnSlope(pm))
                {
                    var slopeAngle = Vector3.Angle(Vector3.up, slopeHit.normal);
                    var slopeAngleIncrease = 1 + slopeAngle / 90f;
                    time += Time.deltaTime * speedIncreaseMultiplier * slopeIncreaseMultiplier * slopeAngleIncrease;
                }

                else
                {
                    time += Time.deltaTime * speedIncreaseMultiplier;
                }

                yield return null;
            }

            pm.MoveSpeed = pm.DesiredMoveSpeed;
        }

        public void SpeedControl(PlayerMovementAdvanced pm, Rigidbody rigidbody)
        {
            if (OnSlope(pm) && !pm.IsExitingSlope)
            {
                if (rigidbody.velocity.magnitude > pm.MoveSpeed ) rigidbody.velocity = rigidbody.velocity.normalized * pm.MoveSpeed ;
            }

            else
            {
                var flatVel = new Vector3(rigidbody.velocity.x, 0f, rigidbody.velocity.z);

                if (!(flatVel.magnitude > pm.MoveSpeed )) return;

                var limitedVel = flatVel.normalized * pm.MoveSpeed ;
                rigidbody.velocity = new Vector3(limitedVel.x, rigidbody.velocity.y, limitedVel.z);
            }
        }

        private bool OnSlope(PlayerMovementAdvanced playerMovementAdvanced)
        {
            if (!Physics.Raycast(transform.position, Vector3.down, out slopeHit,
                    playerMovementAdvanced.PlayerHeight * 0.5f + 0.3f))
            {
                return false;
            }

            var angle = Vector3.Angle(Vector3.up, slopeHit.normal);
            return angle < playerMovementAdvanced.MaxSlopeAngle && angle != 0;
        }

        private Vector3 GetSlopeMoveDirection(Vector3 direction)
        {
            return Vector3.ProjectOnPlane(direction, slopeHit.normal).normalized;
        }

    }
}
