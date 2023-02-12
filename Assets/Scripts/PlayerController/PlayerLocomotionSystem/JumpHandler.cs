using UnityEngine;

namespace PlayerController.PlayerLocomotionSystem
{
    public class JumpHandler : MonoBehaviour
    {
        [SerializeField] private float _jumpForce;
        [SerializeField] private float _jumpCooldown;

        private bool readyToJump;

        public void ReadyToJump()
        {
            readyToJump = true;
        }

        public void TryJump(PlayerMovementAdvanced playerMovementAdvanced, Rigidbody rb)
        {
            if (!readyToJump || !playerMovementAdvanced.IsGrounded)
            {
                return;
            }
            readyToJump = false;
            Jump(rb, playerMovementAdvanced);
            Invoke(nameof(ResetJump), _jumpCooldown);
        }

        private void Jump(Rigidbody rb, PlayerMovementAdvanced playerMovementAdvanced)
        {
            playerMovementAdvanced.IsExitingSlope = true;
            Vector3 velocity = rb.velocity;
            velocity = new Vector3(velocity.x, 0f, velocity.z);
            rb.velocity = velocity;
            rb.AddForce(transform.up * _jumpForce, ForceMode.Impulse);
        }
        private void ResetJump()
        {
            readyToJump = true;
        }
    }
}
