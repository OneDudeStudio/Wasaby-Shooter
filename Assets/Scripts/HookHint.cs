using PlayerController;
using UnityEngine;

public class HookHint : MonoBehaviour
{
    private bool _isHookEnabled = false;

    [SerializeField] private GameObject hint;
    [SerializeField] private PlayerManager playerManager;
    [SerializeField] private ParticleSystem particles1;
    [SerializeField] private ParticleSystem particles2;

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out PlayerManager _))
        {
            if (!_isHookEnabled)
            {
                particles1.loop = false;
                particles2.loop = false;
                _isHookEnabled = true;
                playerManager.GetComponent<SwingingDone>().enabled = true;
            }
            hint.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        hint.SetActive(false);
    }
}
