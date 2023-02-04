using UnityEngine;

namespace Enemies
{
    [CreateAssetMenu(menuName = "Enemies/Factory")]
    public class EnemyFactory : ScriptableObject
    {
        [SerializeField] private Enemy _meleeEnemyPrefab;
        [SerializeField] private Enemy _bombEnemyPrefab;

        public Enemy Get(EnemyType type)
        {
            return type switch
            {
                EnemyType.Melee => Instantiate(_meleeEnemyPrefab),
                EnemyType.Bomb => Instantiate(_bombEnemyPrefab),
                _ => null
            };
        }
    }
}
