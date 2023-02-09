using UnityEngine;
using UnityEngine.Serialization;

public class StaticGenerator : MonoBehaviour
{
    [SerializeField] bool Generatator = false;
    [SerializeField] private GameObject _gameObjectForGeneration;
    [SerializeField] private GameObject _directionPoint;

    private Vector3 _direction;
    private float _distance;

    void Start()
    {
        var direction = _directionPoint.transform.position - transform.position;
        _distance = direction.magnitude;
        _direction = direction / _distance; // This is now the normalized direction.

        if (Generatator)
        {
            var objectSizeX = _gameObjectForGeneration.GetComponent<BoxCollider>().size.x;
            var countObjectsToGenerate = Mathf.RoundToInt(_distance / objectSizeX);

            Generate(countObjectsToGenerate);
        }
    }

    private void Generate(int countObjectsToGenerate)
    {
        float lerpValue = 0f;
        //As we'll be using vector3.lerp we want a value between 0 and 1
        float lerpForOneObjects = (float)1 / countObjectsToGenerate;

        for (int i = 0; i < countObjectsToGenerate; i++)
        {
            // increase lerpValue
            lerpValue += lerpForOneObjects;
            Vector3 newGameObjectPosition =
                Vector3.Lerp(transform.position, _directionPoint.transform.position, lerpValue);

            GameObject newGameObject =
                Instantiate(_gameObjectForGeneration,
                    newGameObjectPosition + Vector3.down * transform.localPosition.y + Vector3.down * 0.2f,
                    Quaternion.LookRotation(_direction) * Quaternion.Euler(0f, -90f, 0f));
            // TO DO:   can be delete if another prefab
            // TO DO: * Quaternion.Euler(0f, -90f, 0f) can delete if another prefab
            newGameObject.transform.parent = transform;
        }
    }
}