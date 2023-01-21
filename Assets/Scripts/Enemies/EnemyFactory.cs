using UnityEngine;

namespace Enemies
{
    [CreateAssetMenu(menuName = "Enemies/Factory")]
    public class EnemyFactory : ScriptableObject
    {
        [SerializeField] private Enemy _strikingEnemyPrefab;
        [SerializeField] private Enemy _explodingEnemyPrefab;

        public Enemy Get(EnemyType type)
        {
            return type switch
            {
                EnemyType.Striking => Instantiate(_strikingEnemyPrefab),
                EnemyType.Exploding => Instantiate(_explodingEnemyPrefab),
                _ => null
            };
        }
    }
}
