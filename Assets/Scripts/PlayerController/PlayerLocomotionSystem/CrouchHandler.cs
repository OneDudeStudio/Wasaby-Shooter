using UnityEngine;

namespace PlayerController.PlayerLocomotionSystem
{
    public class CrouchHandler : MonoBehaviour
    {
        [SerializeField] private float _crouchSpeed;
        [SerializeField] private float _crouchYScale;

        public float CrouchSpeed => _crouchSpeed;

        private float startYScale;

        public void SetDefaultScale()
        {
            startYScale = transform.localScale.y;
        }

        public void TryStartCrouch(PlayerMovementAdvanced playerMovementAdvanced, Rigidbody rb)
        {
            ChangeScale(_crouchYScale);
            rb.AddForce(Vector3.down * 5f, ForceMode.Impulse);
            playerMovementAdvanced.IsCrouching = true;
        }

        public void StopCrouch(PlayerMovementAdvanced playerMovementAdvanced)
        {
            ChangeScale(startYScale);
            playerMovementAdvanced.IsCrouching = false;
        }

        private void ChangeScale(float newScale)
        {
            var playerTransform = transform;
            var localScale = playerTransform.localScale;
            localScale = new Vector3(localScale.x, newScale, localScale.z);
            playerTransform.localScale = localScale;
        }
    }
}
