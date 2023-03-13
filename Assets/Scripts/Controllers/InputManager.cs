using PlayerController.PlayerLocomotionSystem;
using UI.UI;
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
    [SerializeField] private KeyCode _dashKey = KeyCode.LeftAlt;
    [SerializeField] private KeyCode _sprintKey = KeyCode.LeftShift;
    [SerializeField] private KeyCode _crouchKey = KeyCode.LeftControl;

    [Space]
    [Header("SwingingKeys")]
    [SerializeField] private KeyCode _swingKey = KeyCode.E;

    [Space]
    [Header("Axis")]
    [SerializeField] private string _horizontalAxis = "Horizontal";
    [SerializeField] private string _verticalAxis = "Vertical";
    [SerializeField] private string _horizontalMouseAxis = "Mouse X";
    [SerializeField] private string _verticalMouseAxis = "Mouse Y";

    [Space]
    [Header("Environment")]
    [SerializeField] private KeyCode _openShopKey = KeyCode.F;
    //[SerializeField] private KeyCode _exitKey = KeyCode.Escape;
    
    //[Space]
    //[Header("UI interact")]
    // [SerializeField] private KeyCode _pauseGameOrExit = KeyCode.Escape;
    // [SerializeField] private KeyCode _exitKey = KeyCode.Escape;

    [Space]
    [Header("Scripts")]
    [SerializeField] private PlayerMovementAdvanced _movementAdvanced;
    [SerializeField] private GunController _gunController;
    [SerializeField] private PlayerCam _playerCam;

    [SerializeField] private Shop _shop;

    [SerializeField] private GeneralCanvasCore _generalCanvas;

    private CursorController _cursorController;


    private bool _isBlockAnyInput = false;
    private bool _isCanMove = true;
    private bool _isCanRotateCamera = true;
    private bool _isCanUseWeapon = true;


    public void LockCameraRotation() => _isCanRotateCamera = false;
    public void UnlockCameraRotation() => _isCanRotateCamera = true;

    public void LockMovement() => _isCanMove = false;
    public void UnlockMovement() => _isCanMove = true;


    public void LockWeapons() => _isCanUseWeapon = false;
    public void UnlockWeapons() => _isCanUseWeapon = true;

    public void ShowCursor() => _cursorController.ShowCursor();
    public void HideCursor() => _cursorController.HideCursor();


    private void Start()
    {
        _cursorController = new CursorController();
    }

    private void Update()
    {
       //  if (Input.GetKeyDown(_pauseGameOrExit))
       //  {
       //      _isBlockAnyInput = _generalCanvas.TryGoToPause();
       //
       //      switch (_isBlockAnyInput)
       //      {
       //          case true:
       //              _cursorController.ShowCursor();
       //              break;
       //          case false:
       //              _cursorController.HideCursor();
       //              break;
       //      }
       //
       //      _shop.TryUseShop(false);
       //  }
       //  
       // if (_isBlockAnyInput)
       // {
       //     return;
       // }
        
        if (_isCanRotateCamera)
        {
            _playerCam.RotateCamera(Input.GetAxisRaw(_horizontalMouseAxis), Input.GetAxisRaw(_verticalMouseAxis));
        }

        if (_isCanMove)
        {
            _movementAdvanced.SetInputs(Input.GetAxisRaw(_horizontalAxis), Input.GetAxisRaw(_verticalAxis));

            if (Input.GetKey(_jumpKey))
            {
                _movementAdvanced.TryJumpByHandler();
            }
            
            if (Input.GetKeyDown(_dashKey))
            {
                _movementAdvanced.TryDashByHandler();
            }

            if (Input.GetKeyDown(_crouchKey))
            {
                _movementAdvanced.TryStartCrouchByHandler();
            }

            if (Input.GetKeyUp(_crouchKey))
            {
                _movementAdvanced.StopCrouchByHandler();
            }

            if (Input.GetKey(_sprintKey))
            {
                _movementAdvanced.Sprint();
            }
        }

        if (Input.GetKeyDown(_swingKey))
        {
            _movementAdvanced.StartSwingByHandler();
        }

        if (Input.GetKeyUp(_swingKey))
        {
            _movementAdvanced.StopSwingByHandler();
        }

        if (_isCanUseWeapon)
        {
            if (Input.GetKey(_shootKey))
            {
                _gunController.TryShoot();
            }

            if (Input.GetKeyDown(_reloadKey))
            {
                _gunController.TryReloadGun();
            }

            if (Input.GetKeyDown(_grenadeLauncherShootKey))
            {
                _gunController.TryShootGrenade();
            }
        }


       if (Input.GetKeyDown(_openShopKey))
       {
           _shop.TryUseShop(false);
       }
        

       // if (Input.GetKeyDown(_exitKey))
       // {
       //      _shop.TryUseShop(true);
       //
       //      ////
       //
       // }

    }
}
