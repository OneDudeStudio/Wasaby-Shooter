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
        
        public GameObject Generate()
        {
            //PlaySound();
            StartCoroutine(TurnOnRenderer());
            
            GameObject newGameObject = GetGeneratedGameObject();
            SetMove(newGameObject);
            
            return newGameObject;
        }
        
        private IEnumerator TurnOnRenderer()
        {
        
            _rendererForGeneration.material.EnableKeyword("_EMISSION");
            yield return new WaitForSeconds(1);
            _rendererForGeneration.material.DisableKeyword("_EMISSION");
        }
    
        private void PlaySound()
        {
            //_soundForGeneration.Play();
            _soundForGeneration.pitch = Random.Range(0.9f, 1.1f);
            _soundForGeneration.PlayOneShot(_soundForGeneration.clip, 1);
        }
        
        private void SetMove(GameObject newGameObject)
        {
            newGameObject.AddComponent<Move>();
            newGameObject.GetComponent<Move>().Speed = _speed;
            newGameObject.GetComponent<Move>().DirectionPoint = _directionPoint;
            newGameObject.transform.parent = transform;
        }

        private GameObject GetGeneratedGameObject()
        {
            var direction = _directionPoint.transform.position - transform.position;
            
            return Instantiate(_gameObjectForGeneration, transform.position,
                Quaternion.LookRotation(direction));
            // TO DO:  + Vector3.up * objectSizeY / 2 can change if another prefab
            // see also static generator
        }
    }
}
