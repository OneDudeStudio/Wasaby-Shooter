using UnityEngine;

public class PoisonGrenade : Grenade
{
    [SerializeField] private PoisonController _poisonSpawnerPrefab;
    public override void ExplosionPostEffects()
    {
        Vector3 spawnerPosition = IsCeilingHit(_collision.contacts[0].normal, transform.position) ? transform.position : transform.position + Vector3.up * 1.5f;
        Instantiate(_poisonSpawnerPrefab, spawnerPosition, Quaternion.identity);
    }

    protected override GrenadeConfig GetConfig() => _config.Poison;

    private bool IsCeilingHit(Vector3 noraml, Vector3 position)
    {
        if (noraml.y < -.7f)
            return true;
        if (Physics.Raycast(position, Vector3.up, 1.5f))
            return true;
        return false;
    }
}
