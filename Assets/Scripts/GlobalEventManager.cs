using UnityEngine;
using UnityEngine.Events;

public class GlobalEventManager : MonoBehaviour
{
    public static UnityEvent<Transform> OnDie = new UnityEvent<Transform>();

    public static void SendDie(Transform tr)
    {
        OnDie.Invoke(tr);
    }
}
