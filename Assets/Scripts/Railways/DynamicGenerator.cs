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

    private void Start()
    {
        _nextTimeToSpawn = Time.time;
    }

    private void Update()
    {
        if (Time.time > _nextTimeToSpawn)
        {
            _nextTimeToSpawn = Time.time + _delay;
            GameObject newGameObject = Instantiate(_gameObjectForGeneration, transform.position, Quaternion.identity);
            newGameObject.AddComponent<Move>();
            newGameObject.GetComponent<Move>().speed = _speed;
        }
    }
    

    /*private float _lastSpawnedTime;

    void Update()
    {
        if (Time.time > _lastSpawnedTime + _delay)
        {
            Generate();
            _lastSpawnedTime = Time.time;

        }
    }

    public void Generate()
    {
        GameObject newGameObject = Instantiate(_gameObjectForGeneration, transform.position, Quaternion.identity);
        newGameObject.GetComponent<Rigidbody>().velocity = transform.forward * _speed;
    }*/
}


public class Move : MonoBehaviour
{
    public float speed;

    private void Update()
    {
        transform.Translate(Vector2.right * Time.deltaTime * speed);
    }
    
}
