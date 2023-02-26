using System;
using UnityEngine;

[CreateAssetMenu(menuName = "Grenades Data")]
public class ExplosionItemsConfig : ScriptableObject
{
    [SerializeField] private DefaultGrenadeConfig _default;
    [SerializeField] private PoisonGrenadeConfig _poison;
    [SerializeField] private FlashGrenadeConfig _flash;
    [SerializeField] private RedBarrelConfig _redBarrel;

    public DefaultGrenadeConfig Default => _default;
    public PoisonGrenadeConfig Poison => _poison;
    public FlashGrenadeConfig Flash => _flash;
    public RedBarrelConfig Barrel => _redBarrel;
}

[Serializable]
public class ExplosionConfig
{
    public float ExplosionDamage;
    public float ExplosionRadius;
    public LayerMask CheckLayers;
}

[Serializable]
public class DefaultGrenadeConfig : ExplosionConfig
{
}

[Serializable]
public class PoisonGrenadeConfig : ExplosionConfig
{
}

[Serializable]
public class FlashGrenadeConfig : ExplosionConfig
{
}

[Serializable]
public class RedBarrelConfig : ExplosionConfig
{
}