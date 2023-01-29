using System;
using UnityEngine;

public class BulletHolesPool : MonoBehaviour
{
    [SerializeField] private BulletHole _buletHolePrefab;
    [SerializeField] private int _poolSize;
    private BulletHole[] _pool;
    private int _counter = 0;

    private void Start()
    {
        _pool = new BulletHole[_poolSize];
        for (int i = 0; i < _poolSize; i++)
        {
            _pool[i] = Instantiate(_buletHolePrefab);
        }
        //GlobalEventManager.OnDie.AddListener(DestroyObject);
    }


    public void AddHole(RaycastHit hit)
    {
        _counter %= _poolSize;
        BulletHole bulletHole = _pool[_counter];
        bulletHole.gameObject.SetActive(true);
        bulletHole.transform.parent = null;
        bulletHole.transform.SetPositionAndRotation(hit.point + hit.normal * 0.001f, Quaternion.LookRotation(hit.normal));
        bulletHole.transform.parent = hit.transform;
        _counter++;
    }

    public void DestroyObject(Transform destroyable)
    {
        BulletHole[] holes = destroyable.GetComponentsInChildren<BulletHole>();
        foreach (BulletHole hole in holes)
        {
            hole.ReturnToPool();
        }
    }
}
