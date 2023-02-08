using PlayerController;
using UnityEngine;

public class Void : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out PlayerManager player))
        {
            player.Die();
        }
    }
}
