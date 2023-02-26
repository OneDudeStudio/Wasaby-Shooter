using System;
using UnityEngine;

[CreateAssetMenu(menuName = "RootConfig")]
public class RootConfig : ScriptableObject
{
    [Header("Configs")]
    [SerializeField] private GunConfig _gunConfig;
    [SerializeField] private ShotgunConfig _shotgunConfig;
    [SerializeField] private BulletsConfig _bulletsConfig;
    [SerializeField] private EffectsConfig _effectsConfig;
    [SerializeField] private ExplosionItemsConfig _explosionItemsConfig;

    public GunConfig GunConfig => _gunConfig;
    public ShotgunConfig ShotgunConfig => _shotgunConfig;
    public BulletsConfig BulletsConfig => _bulletsConfig;
    public EffectsConfig EffectsConfig => _effectsConfig;
    public ExplosionItemsConfig ExplosionItemsConfig => _explosionItemsConfig;

}