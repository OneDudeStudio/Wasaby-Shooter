using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameObjectEnumerator : IEnumerator
{
    public GameObject[] _gameObjects;

    // Enumerators are positioned before the first element
    // until the first MoveNext() call.
    int position = -1;

    public  GameObjectEnumerator(GameObject[] list)
    {
        _gameObjects = list;
    }

    public bool MoveNext()
    {
        position++;
        return (position < _gameObjects.Length);
    }

    public void Reset()
    {
        position = -1;
    }

    object IEnumerator.Current
    {
        get
        {
            return Current;
        }
    }

    public GameObject Current
    {
        get
        {
            try
            {
                return _gameObjects[position];
            }
            catch (IndexOutOfRangeException)
            {
                throw new InvalidOperationException();
            }
        }
    }
}
