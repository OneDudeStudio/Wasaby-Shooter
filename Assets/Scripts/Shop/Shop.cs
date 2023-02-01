using PlayerController;
using UnityEngine;
using System;

public class Shop : MonoBehaviour
{
    private UIController _uiController;
    private InputManager _inputManager;
    private CursorController _cursorController;

    private bool _isInShop = false;
    private bool _isShopUIOpened = false;

    [SerializeField] private Gun _gun;
    [SerializeField] private GrenadeLauncher _grenadeLauncher;
    [SerializeField] private BulletsConfig _bulletsConfig;

    private IBullet[] _bullets = new IBullet[4];
    private Type[] _moduleTypes = new Type[] { typeof(NullModule), typeof(ExtendedMag), typeof(BarrelModule), typeof(ShutterModule) };
    private Type[] _grenadeTypes = new Type[] { null, typeof(DefaultExplosionGrenade), typeof(PoisonGrenade), typeof(FlashGrenade) };
   

    private void Start()
    {
        _cursorController = FindObjectOfType<CursorController>();
        _inputManager = FindObjectOfType<InputManager>();
        _uiController = FindObjectOfType<UIController>();

        _bullets[0] = new Bullet(_bulletsConfig.Default.AdditionalDamagePrecent);
        _bullets[1] = new EffectBullet<Burning>(_bulletsConfig.Fire.AdditionalDamagePrecent);
        _bullets[2] = new EffectBullet<Freeze>(_bulletsConfig.Freeze.AdditionalDamagePrecent);
        _bullets[3] = new EffectBullet<Electricity>(_bulletsConfig.Electric.AdditionalDamagePrecent);

        _gun.SetBullet(_bullets[0]);

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out PlayerManager _))
        {
            _isInShop = true;
            _uiController.SetCanvasActive(CanvasType.ShopHint);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        _isInShop = false;
        CloseShopWindow();
        _uiController.SetCanvasDeactive(CanvasType.ShopHint);
    }

    public void TryUseShop(bool isExiting)
    {
        if (!_isInShop)
            return;
        if (!_isShopUIOpened && !isExiting)
        {
            OpenShopWindow();
        }
        else
        {
            CloseShopWindow();
        }
        _isShopUIOpened = !_isShopUIOpened;
    }

    private void OpenShopWindow()
    {
        _uiController.SetCanvasActive(CanvasType.Shop);
        _cursorController.ShowCursor();
        _inputManager.LockCameraRotation();
        _inputManager.LockMovement();
        _inputManager.LockWeapons();
    }

    private void CloseShopWindow()
    {
        _uiController.SetCanvasDeactive(CanvasType.Shop);
        _cursorController.HideCursor();
        _inputManager.UnlockCameraRotation();
        _inputManager.UnlockMovement();
        _inputManager.UnlockWeapons();
    }



    public void SetGunModule(int number) => _gun.SetModule(_moduleTypes[number]);

    public void SetBullet(int number) => _gun.SetBullet(_bullets[number]);

    public void SetGrenade(int number) => _grenadeLauncher.SetGrenade(_grenadeTypes[number]);

}
