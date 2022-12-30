using UnityEngine;

public class BulletHole : MonoBehaviour
{
    public void ReturnToPool()
    {
        transform.parent = null;
        gameObject.SetActive(false);
    }
}
