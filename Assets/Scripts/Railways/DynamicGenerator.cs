using UnityEngine;

public class DynamicGenerator : MonoBehaviour
{
    [SerializeField] private GameObject _gameObjectForGeneration;
    [SerializeField] private float _delay = 2f;
    [SerializeField] private float _speed = 3f;
    [SerializeField] private GameObject _directionPoint;

    private float _lastSpawnedTime;

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
    }
}

/*public class Move : MonoBehaviour
{
    public float speed;

    private void Update()
    {
        transform.Translate(Vector2.left * Time.deltaTime * speed);
    }

    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}*/
