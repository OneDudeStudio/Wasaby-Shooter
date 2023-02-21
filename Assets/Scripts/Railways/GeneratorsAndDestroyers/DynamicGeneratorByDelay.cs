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
            _nextTimeToSpawn = Time.time;
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

    }
}


