using System;
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

        private GameObject _generatedGameObject;
        private Func<bool> _generateGameObject;
        private event Action Genereted;

        private void Awake()
        {
            _generateGameObject = GenerateDynamicGameObject;
        }

        private void Start()
        {
            TurnOffRenderer();
        }

        public GameObject Generate()
        {
            _generatedGameObject = null;
            
            StartCoroutine(StartGeneratorCoroutine());
            GenerateDynamicGameObject();
            
            Genereted?.Invoke();

            return _generatedGameObject;
        }

        private IEnumerator StartGeneratorCoroutine()
        {
            TurnOnRenderer();
            PlaySound();
            yield return new WaitForSeconds(2);
            //yield return new WaitUntil(_generateGameObject);
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

        private bool GenerateDynamicGameObject()
        {
            var direction = _directionPoint.transform.position - transform.position;

            _generatedGameObject = Instantiate(_gameObjectForGeneration,
                transform.position + _gameObjectForGeneration.transform.position,
                Quaternion.LookRotation(direction));
            // TO DO:  + Vector3.up * objectSizeY / 2 can change if another prefab
            // see also static generator
            SetMove(_generatedGameObject);
            return true;
        }

        private void SetMove(GameObject newGameObject)
        {
            newGameObject.AddComponent<Move>();
            newGameObject.GetComponent<Move>().Speed = _speed;
            newGameObject.GetComponent<Move>().DirectionPoint = _directionPoint;
            newGameObject.transform.parent = transform;
        }
    }
    
}