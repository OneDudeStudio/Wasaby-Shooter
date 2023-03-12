using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Railways.GeneratorsAndDestroyers
{
    public class DynamicGenerator : MonoBehaviour
    {
        [SerializeField] private GameObject _gameObjectForGeneration;
        [SerializeField] private float _speed = 3f;
        [SerializeField] private GameObject _directionPoint;
        [SerializeField] private AudioSource _soundForGeneration;
        [SerializeField] private Renderer _rendererForGeneration;

        private void Awake()
        {
            TurnOffRenderer();
        }

        public GameObject Generate()
        {
            UnifyCoroutine<object> coroutine = new UnifyCoroutine<object>(GeneratedProcess(), ReturnValueCallback);
            coroutine.Start();

            return GetGeneratedGameObject();
        }

        private void ReturnValueCallback(object returnValue)
        {
            Debug.Log("Return Value is * " + returnValue + " *");
        }

        private IEnumerator GeneratedProcess()
        {
            TurnOnRenderer();
            PlaySound();
            yield return new WaitForSeconds(2);
            //yield return GetGeneratedGameObject();
            //yield return new WaitForSeconds(1);
            TurnOffRenderer();
        }

        private void TurnOnRenderer()
        {
            _rendererForGeneration.material.EnableKeyword("_EMISSION");
        }

        private void TurnOffRenderer()
        {
            _rendererForGeneration.material.DisableKeyword("_EMISSION");
        }

        private void PlaySound()
        {
            _soundForGeneration.pitch = Random.Range(0.9f, 1.1f);
            _soundForGeneration.PlayOneShot(_soundForGeneration.clip, 1);
        }

        private GameObject GetGeneratedGameObject()
        {
            var direction = _directionPoint.transform.position - transform.position;

            var generatedGameObject = Instantiate(_gameObjectForGeneration,
                transform.position + _gameObjectForGeneration.transform.position,
                Quaternion.LookRotation(direction));
            // TO DO:  + Vector3.up * objectSizeY / 2 can change if another prefab
            // see also static generator
            SetMove(generatedGameObject);
            return generatedGameObject;
        }

        private void SetMove(GameObject objectToMove)
        {
            objectToMove.AddComponent<Move>();
            objectToMove.GetComponent<Move>().Speed = _speed;
            objectToMove.GetComponent<Move>().DirectionPoint = _directionPoint;
            objectToMove.transform.parent = transform;
        }
    }
}