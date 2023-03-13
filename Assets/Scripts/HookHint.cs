using PlayerController;
using PlayerController.PlayerLocomotionSystem;
using UnityEngine;

public class HookHint : MonoBehaviour
{
    private bool _isHookEnabled = false;

    [SerializeField] private GameObject hint;
    [SerializeField] private GameObject img;
    [SerializeField] private PlayerManager playerManager;
    [SerializeField] private ParticleSystem particles1;
    [SerializeField] private ParticleSystem particles2;

    private InputManager inputManager;

    private void Start()
    {
       inputManager = FindObjectOfType<InputManager>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out PlayerManager _))
        {
            if (!_isHookEnabled)
            {
                particles1.loop = false;
                particles2.loop = false;
                _isHookEnabled = true;
                inputManager.UnlockSwing();
            }
            hint.SetActive(true);
            img.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        hint.SetActive(false);
        img.SetActive(false);
    }
}
