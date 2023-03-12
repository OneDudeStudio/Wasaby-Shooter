using System;
using System.Collections;
using UnityEngine;

public class UnifyCoroutine<T>
{
    public Action<T> callback;

    private IEnumerator _target;
    private static readonly GameObject _gameObject;
    private static readonly MonoBehaviour _monoBehaviour;

    static UnifyCoroutine()
    {
        _gameObject = new GameObject { isStatic = true, name = "[Unify]" };
        _monoBehaviour = _gameObject.AddComponent<UnifyMonoBehaviour>();
    }
    
    public UnifyCoroutine(IEnumerator target, Action<T> callback)
    {
        _target = target;
        this.callback = callback;
    }
    
    public void Start()
    {
        _monoBehaviour.StartCoroutine(StartUnifyCoroutine());
    }

    private IEnumerator StartUnifyCoroutine()
    {
        while (_target.MoveNext())
        {
            yield return _target.Current;
        }
        
        callback.Invoke((T)_target.Current);
    }
    
    

}