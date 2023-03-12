using UnityEngine;

namespace Railways.GeneratorsAndDestroyers
{
    public class DynamicGeneratorByDelay : DynamicGenerator
    {
        [SerializeField] bool Generatator = false;
        [SerializeField] private float _delay = 2f;
        
        private float _nextTimeToSpawn;
        
        private void Start()
        {
            _nextTimeToSpawn = Time.time + _delay;
        }

        private void Update()
        {
            if (Time.time > _nextTimeToSpawn)
            {
                if (Generatator)
                {
                    Generate();
                }
                _nextTimeToSpawn = Time.time + _delay;
            }
        }

    }
}


