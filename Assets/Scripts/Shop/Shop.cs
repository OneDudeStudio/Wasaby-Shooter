using PlayerController;
using UnityEngine;
using System;

public class Shop : MonoBehaviour
{
    private UIController _uiController;
    private InputManager _inputManager;
    private GunController _gunController;

    private bool _isInShop = false;
    private bool _isShopUIOpened = false;

    public bool IsInShop => _isInShop;

    private void Start()
    {
        _inputManager = FindObjectOfType<InputManager>();
        _uiController = FindObjectOfType<UIController>();
        _gunController = FindObjectOfType<GunController>();
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
        _uiController.SetCanvasDeactive(CanvasType.ShopHint);

        _uiController.SetCanvasActive(CanvasType.Shop);
        _inputManager.ShowCursor();
        _inputManager.LockCameraRotation();
        _inputManager.LockMovement();
        _inputManager.LockWeapons();
    }

    private void CloseShopWindow()
    {
        _uiController.SetCanvasDeactive(CanvasType.Shop);
        _inputManager.HideCursor();
        _inputManager.UnlockCameraRotation();
        _inputManager.UnlockMovement();
        _inputManager.UnlockWeapons();
    }



    public void SetGunModule(int number) => _gunController.SetModule(number);

    public void SetBullet(int number) => _gunController.SetBullet(number);

    public void SetGrenade(int number) => _gunController.SetGrenade(number);

}
