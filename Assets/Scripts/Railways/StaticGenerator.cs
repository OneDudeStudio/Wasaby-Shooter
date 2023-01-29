using UnityEngine;

public class StaticGenerator : MonoBehaviour
{
    [SerializeField] bool Generatator = false;

    [SerializeField] private GameObject _gameObjectForGeneration;
    [SerializeField] private int _count = 10;
    [SerializeField] private float _distance = 0;
    [SerializeField] private GameObject _directionPoint;

    private Quaternion _gameObjectRotation;

    void Start()
    {
        _gameObjectRotation = transform.rotation;
        Vector3 direction = new Vector3(
            _directionPoint.transform.position.x - transform.position.x,
            _directionPoint.transform.position.y - transform.position.y,
            _directionPoint.transform.position.z - transform.position.z
        );

        if (Generatator)
        {
            Generate();
        }
    }

    private void Generate()
    {
        // NEED TO DO
        for (int i = 0; i < _count;)
        {
            Vector3 newGameObjectPosition = new Vector3(
                transform.position.x + _distance * ++i,
                transform.position.y - transform.localScale.y / 2, // attach to the ground
                transform.position.z
            );
            GameObject newGameObject =
                Instantiate(_gameObjectForGeneration, newGameObjectPosition, _gameObjectRotation);
            newGameObject.transform.parent = transform;
        }
    }
}