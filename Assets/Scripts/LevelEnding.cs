using PlayerController;
using UnityEngine;

public class LevelEnding : MonoBehaviour
{
    [SerializeField] private GameObject _canvas;
    [SerializeField] private GameObject _mainCanvas;
    private InputManager _inputManager;

    private void Start()
    {
        _inputManager = FindObjectOfType<InputManager>();  
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out PlayerManager _))
        {
            _inputManager.ShowCursor();
            _inputManager.LockCameraRotation();
            _inputManager.LockMovement();
            _inputManager.LockWeapons();
            _mainCanvas.SetActive(false);
            _canvas.SetActive(true);
        }
    }
}
