using System;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class DynamicGenerator : MonoBehaviour
{
    [SerializeField] bool Generatator = false;

    [SerializeField] private GameObject _gameObjectForGeneration;
    [SerializeField] private float _delay = 2f;
    [SerializeField] private float _speed = 3f;
    [SerializeField] private GameObject _directionPoint;

    private float _nextTimeToSpawn;
    private Vector3 _direction;

    private void Start()
    {
        _nextTimeToSpawn = Time.time;
        _direction = _directionPoint.transform.position - transform.position;
    }

    private void Update()
    {
        if (Time.time > _nextTimeToSpawn)
        {
            _nextTimeToSpawn = Time.time + _delay;
            if (Generatator)
            {
                Generate();
            }
            
        }
    }

    private void Generate()
    {
        GameObject newGameObject = 
            Instantiate(_gameObjectForGeneration, transform.position,
                Quaternion.LookRotation(_direction));
        // TO DO:  + Vector3.up * objectSizeY / 2 can change if another prefab
        // see also static generator
        newGameObject.AddComponent<Move>();
        newGameObject.GetComponent<Move>().speed = _speed;
        newGameObject.GetComponent<Move>().directionPoint = _directionPoint;
        newGameObject.transform.parent = transform;
    }
}


public class Move : MonoBehaviour
{
    public float speed;
    public GameObject directionPoint;

    private void Update()
    {
        transform.position =
            Vector3.MoveTowards(transform.position, directionPoint.transform.position, Time.deltaTime * speed);
    }
}