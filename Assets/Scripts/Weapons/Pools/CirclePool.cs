using System.Collections.Generic;
using UnityEngine;

public class CirclePool<T> where T : MonoBehaviour
{
    [SerializeField] private T _poolObjectPrefab;
    [SerializeField] private int _poolSize;
    private List<T> _pool;
    private int _counter = 0;
    private Transform _container = null;
    private bool _isActiveOnSpawn = false;

    public CirclePool(T prefab, int size, Transform container)
    {
       _container = container;
        _poolObjectPrefab = prefab;
        _poolSize = size;
        InitializePool();
    }

    public void InitializePool()
    {
        _pool = new List<T>(_poolSize);
        for (int i = 0; i < _poolSize; i++)
        {
            T poolObject = Object.Instantiate(_poolObjectPrefab, _container);
            poolObject.gameObject.SetActive(_isActiveOnSpawn);
            _pool.Add(poolObject);
        }
    }

    public T GetPoolObject()
    {
        _counter %= _poolSize;
        T poolObject = _pool[_counter];
        _counter++;
        poolObject.transform.parent = null;
        return poolObject;
    }
}
