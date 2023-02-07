using System;
using UnityEngine;

[CreateAssetMenu(menuName = "Grenades Data")]
public class GrenadesConfig : ScriptableObject
{
    [SerializeField] private DefaultGrenadeConfig _default;
    [SerializeField] private PoisonGrenadeConfig _poison;
    [SerializeField] private FlashGrenadeConfig _flash;

    public DefaultGrenadeConfig Default => _default;
    public PoisonGrenadeConfig Poison => _poison;
    public FlashGrenadeConfig Flash => _flash;
}

[Serializable]
public class GrenadeConfig
{
    public float ExplosionDamage;
    public float ExplosionRadius;
    public int MaxTargets;
    public LayerMask CheckLayers;
}
[Serializable]
public class DefaultGrenadeConfig : GrenadeConfig
{
}
[Serializable]
public class PoisonGrenadeConfig : GrenadeConfig
{
}
[Serializable]
public class FlashGrenadeConfig : GrenadeConfig
{
}

