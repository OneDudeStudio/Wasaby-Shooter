using System.Collections;
using UnityEngine;

namespace PlayerController.PlayerLocomotionSystem
{
    public class PlayerMovementAdvanced : MonoBehaviour
    {
        [Header("Locomotion Handlers")]
        [SerializeField] private MovementState _movementState;
        [SerializeField] private JumpHandler _jumpHandler;
        [SerializeField] private CrouchHandler _crouchHandler;
        [SerializeField] private MovementHandler _movementHandler;
        [SerializeField] private DashHandler _dashHandler;
        [SerializeField] private SwingingHandler _swingingHandler;

        [Header("Locomotion Settings")]
        [SerializeField] private PlayerCam _playerCam;
        [SerializeField] private float _playerHeight;
        [SerializeField] private float _maxSlopeAngle;
        [SerializeField] private LayerMask _whatIsGround;

        private float _lastDesiredMoveSpeed;
        private float _verticalInput;
        private float _desiredMoveSpeed;
        private float _horizontalInput;
        private float _moveSpeed;
        public float maxYSpeed;
        private bool keepMomentum;
        private Rigidbody rb;
        private RaycastHit slopeHit;

        public float PlayerHeight => _playerHeight;
        public float MaxSlopeAngle => _maxSlopeAngle;

        public float DesiredMoveSpeed
        {
            get => _desiredMoveSpeed;
            private set => _desiredMoveSpeed = value;
        }
        public float VerticalInput
        {
            get => _verticalInput;
            private set => _verticalInput = value;
        }
        public float HorizontalInput
        {
            get => _horizontalInput;
            private set => _horizontalInput = value;
        }
        public float MoveSpeed
        {
            get => _moveSpeed;
            set => _moveSpeed = value;
        }

        /// <summary>
        /// Описаны все возможные стейты при передвижении игрока
        /// </summary>
        #region PlayerStates

        private bool _isSliding;
        private bool _isVaulting;
        private bool _isSprinting;
        private bool _isFreeze;
        private bool _isUnlimited;
        private bool _isRestricted;
        private bool _isGrounded;
        private bool _isActiveGrapple;
        private bool _isCrouching;
        private bool _isSwinging;
        private bool _isExitingSlope;
        private bool _isDashing;
        public bool IsGrounded => _isGrounded;
        public bool IsSliding => _isSliding;
        public bool IsVaulting => _isVaulting;
        public bool IsSprinting => _isSprinting;
        public bool IsActiveGrapple => _isActiveGrapple;
        
        public bool IsDashing
        {
            get => _isDashing;
            set => _isDashing = value;
        }
        public bool IsCrouching
        {
            get => _isCrouching;
            set => _isCrouching = value;
        }
        public bool IsSwinging
        {
            get => _isSwinging;
            set => _isSwinging = value;
        }
        public bool IsExitingSlope
        {
            get => _isExitingSlope;
            set => _isExitingSlope = value;
        }
        public bool IsFreeze
        {
            get => _isFreeze;
            set => _isFreeze = value;
        }
        public bool IsUnlimited
        {
            get => _isUnlimited;
            set => _isUnlimited = value;
        }
        public bool IsRestricted
        {
            get => _isRestricted;
            set => _isRestricted = value;
        }
        #endregion

        private void Awake()
        {
            rb = GetComponent<Rigidbody>();
            rb.freezeRotation = true;
        }

        public void ResetRestrictions()
        {
            _isActiveGrapple = false;
            _playerCam.DoFov(85f);
        }

        private void Start()
        {
            _jumpHandler.ReadyToJump();
            _crouchHandler.SetDefaultScale();
        }

        private void Update()
        {
            CheckGround();
            _movementHandler.SpeedControl(this, rb);
            StateHandler();
            _isSprinting = false;
            HandleDrag();
            _swingingHandler.CheckForSwingPoints(_playerCam);
            _swingingHandler.InAirGearMovementController(rb, _playerCam);
        }

        private void FixedUpdate()
        {
            _movementHandler.MovePlayer(this, _playerCam, rb);
        }

        private void LateUpdate()
        {
            _swingingHandler.DrawRope();
        }

        public void StartSwingByHandler()
        {
            _swingingHandler.StartSwing(this, transform);
        }

        public void StopSwingByHandler()
        {
            _swingingHandler.StopSwing(this);
        }
        public void Sprint()
        {
            _isSprinting = true;
        }

        public void SetInputs(float xInput, float yInput)
        {
            (HorizontalInput, VerticalInput) = (xInput, yInput);
        }

        private void CheckGround()
        {
            var playerTransform = transform;
            var playerPosition = playerTransform.position;
            var origin = new Vector3(playerPosition.x, playerPosition.y - playerTransform.localScale.y * .5f,
                playerPosition.z);
            var direction = transform.TransformDirection(Vector3.down);
            _isGrounded = Physics.Raycast(origin, direction, _playerHeight * 0.5f + 0.2f, _whatIsGround);
        }

        private void HandleDrag()
        {
            if (_movementState == MovementState.Walking || _movementState == MovementState.Sprinting ||
                _movementState == MovementState.Crouching)
            {
                rb.drag = _movementHandler.groundDrag;
            }

            else
            {
                rb.drag = 0;
            }
        }

        public void TryJumpByHandler()
        {
            _jumpHandler.TryJump(this, rb);
        }
        
        public void TryDashByHandler()
        {
            _dashHandler.TryDash(this, rb, _playerCam);
        }

        public void TryStartCrouchByHandler()
        {
            if (HorizontalInput != 0 || VerticalInput != 0)
            {
                return;
            }

            _crouchHandler.TryStartCrouch(this, rb);
        }

        public void StopCrouchByHandler()
        {
            _crouchHandler.StopCrouch(this);
        }

        /// <summary>
        /// Главный метод этого класса, он следит за поведением игрока в зависимости от его стейта
        /// </summary>

        private void StateHandler()
        {
            // Mode - Dashing
            if (_isDashing)
            {
                _movementState = MovementState.Dashing;
                DesiredMoveSpeed = _movementHandler.dashSpeed;
                _movementHandler.speedIncreaseMultiplier = _movementHandler.dashSpeedChangeFactor;
            }
            
            else if (_isFreeze)
            {
                _movementState = MovementState.Freeze;
                rb.velocity = Vector3.zero;
                DesiredMoveSpeed = 0f;
            }

            else if (_isUnlimited)
            {
                _movementState = MovementState.Unlimited;
                DesiredMoveSpeed = 999f;
            }

            else if (_isVaulting)
            {
                _movementState = MovementState.Vaulting;
                DesiredMoveSpeed = _movementHandler.vaultSpeed;
            }

            else if (_isActiveGrapple)
            {
                _movementState = MovementState.Grappling;
                MoveSpeed = _movementHandler.sprintSpeed;
            }

            else if (_isSwinging)
            {
                _movementState = MovementState.Swinging;
                MoveSpeed = _movementHandler.swingSpeed;
            }
            else if (_isCrouching)
            {
                _movementState = MovementState.Crouching;
                DesiredMoveSpeed = _crouchHandler.CrouchSpeed;
            }

            else if (_isGrounded && _isSprinting)
            {
                _movementState = MovementState.Sprinting;
                DesiredMoveSpeed = _movementHandler.sprintSpeed;
            }

            else if (_isGrounded)
            {
                _movementState = MovementState.Walking;
                DesiredMoveSpeed = _movementHandler.walkSpeed;
            }

            else
            {
                _movementState = MovementState.Air;

                if (MoveSpeed < _movementHandler.airMinSpeed) DesiredMoveSpeed = _movementHandler.airMinSpeed;
            }

            var desiredMoveSpeedHasChanged = DesiredMoveSpeed != _lastDesiredMoveSpeed;

            if (desiredMoveSpeedHasChanged)
            {
                if (keepMomentum)
                {
                    StopAllCoroutines();
                    StartCoroutine(_movementHandler.SmoothlyLerpMoveSpeed(this));
                }
                else
                {
                    MoveSpeed = DesiredMoveSpeed;
                }
            }

            _lastDesiredMoveSpeed = DesiredMoveSpeed;

            if (Mathf.Abs(DesiredMoveSpeed - MoveSpeed) < 0.1f)
            {
                keepMomentum = false;
            }
        }

    }
}
