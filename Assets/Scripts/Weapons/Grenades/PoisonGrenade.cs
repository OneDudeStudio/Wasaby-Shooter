using UnityEngine;

public class PoisonGrenade : Grenade
{
    [SerializeField] private PoisonController _poisonSpawnerPrefab;
    public override void ExplosionPostEffects(bool isCeilingHit, Vector3 point)
    {
        Vector3 spawnerPosition = isCeilingHit ? point : point + Vector3.up * 1.5f;
        Instantiate(_poisonSpawnerPrefab, spawnerPosition, Quaternion.identity);
    }          
}
