using UnityEngine;

public class PoisonGrenade : Grenade
{
    [SerializeField] private PoisonSpawner _poisonSpawnerPrefab;
    public override void ExplosionPostEffects(bool isCeilingHit, Vector3 point)
    {
        Vector3 spawnerPosition = isCeilingHit ? point : point + Vector3.up;
        PoisonSpawner spawner = Instantiate(_poisonSpawnerPrefab, spawnerPosition, Quaternion.identity);
        spawner.ProjectSpawnpoints();
    }          
}
