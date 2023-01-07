using UnityEngine;
using UnityEngine.Events;

public class GlobalEventManager : MonoBehaviour
{
    public static UnityEvent<Transform> OnDie = new UnityEvent<Transform>();
    public static UnityEvent<Transform> OnStartLightningChain = new UnityEvent<Transform>();

    public static void SendDie(Transform tr)
    {
        OnDie.Invoke(tr);
    }
    public static void SendLightningChain(Transform tr)
    {
        OnStartLightningChain.Invoke(tr);
    }
}
