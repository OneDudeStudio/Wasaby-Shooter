using UnityEngine;

public class InputManager : MonoBehaviour
{
    [Header("GunKeys")]
    [SerializeField] private KeyCode _shootKey = KeyCode.Mouse0;
    [SerializeField] private KeyCode _grenadeLauncherShootKey = KeyCode.Mouse1;
    [SerializeField] private KeyCode _reloadKey = KeyCode.R;

    [Space]
    [Header("MovementKeys")]
    [SerializeField] private KeyCode _jumpKey = KeyCode.Space;
    [SerializeField] private KeyCode _sprintKey = KeyCode.LeftShift;
    [SerializeField] private KeyCode _crouchKey = KeyCode.LeftControl;

    [Space]
    [Header("Axis")]
    [SerializeField] private string _horizontalAxis = "Horizontal";
    [SerializeField] private string _verticalAxis = "Vertical";
    [SerializeField] private string _horizontalMouseAxis = "Mouse X";
    [SerializeField] private string _verticalMouseAxis = "Mouse Y";

    [Space]
    [Header("Environment")]
    [SerializeField] private KeyCode _openShopKey = KeyCode.F;
    [SerializeField] private KeyCode _exitKey = KeyCode.Escape;


    [Space]
    [Header("Scripts")]
    [SerializeField] private PlayerMovementAdvanced _movementAdvanced;
    [SerializeField] private Gun _gun;
    [SerializeField] private GrenadeLauncher _grenadeLauncher;
    [SerializeField] private PlayerCam _playerCam;

    [SerializeField] private Shop _shop;



    private bool _isCanMove = true;
    private bool _isCanRotateCamera = true;
    private bool _isCanUseWeapon = true;


    public void LockCameraRotation() => _isCanRotateCamera = false;
    public void UnlockCameraRotation() => _isCanRotateCamera = true;

    public void LockMovement() => _isCanMove = false;
    public void UnlockMovement() => _isCanMove = true;


    public void LockWeapons() => _isCanUseWeapon = false;
    public void UnlockWeapons() => _isCanUseWeapon = true;

    private void Update()
    {
        if (_isCanRotateCamera)
        {
            _playerCam.RotateCamera(Input.GetAxisRaw(_horizontalMouseAxis), Input.GetAxisRaw(_verticalMouseAxis));
        }


        if(_isCanMove)
        {
            _movementAdvanced.SetInputs(Input.GetAxisRaw(_horizontalAxis), Input.GetAxisRaw(_verticalAxis));

            if (Input.GetKey(_jumpKey))
            {
                _movementAdvanced.TryJump();
            }

            if (Input.GetKeyDown(_crouchKey))
            {
                _movementAdvanced.TryStartCrouch();
            }

            if (Input.GetKeyUp(_crouchKey))
            {
                _movementAdvanced.StopCrouch();
            }

            if (Input.GetKey(_sprintKey))
            {
                _movementAdvanced.Sprint();
            }
        }


        if (_isCanUseWeapon)
        {
            if (Input.GetKey(_shootKey))
            {
                _gun.TryShoot();
            }

            if (Input.GetKeyDown(_reloadKey))
            {
                _gun.TryReload();
            }

            if (Input.GetKeyDown(_grenadeLauncherShootKey))
            {
                _grenadeLauncher.TryShootGrenade();
            }
        }


        if (Input.GetKeyDown(_openShopKey))
        {
            _shop.TryUseShop(false);
        }
        if (Input.GetKeyDown(_exitKey))
        {
            _shop.TryUseShop(true);

            ////

        }

    }
}
