using System;
using UnityEngine;


[CreateAssetMenu(menuName = "Effects Data")]

public class EffectsConfig : ScriptableObject
{
    [SerializeField] private BurningConfig _burning;
    [SerializeField] private FreezeConfig _freeze;
    [SerializeField] private PoisonConfig _poison;

    public BurningConfig Burning => _burning;
    public FreezeConfig Freeze => _freeze;
    public PoisonConfig Poison => _poison;

    [Serializable]
    public class EffectConfig
    {
    }

    [Serializable]
    public class BurningConfig : EffectConfig
    {
        public int Ticks;
        public float Interval;
        public float Damage;
    }

    [Serializable]
    public class FreezeConfig : EffectConfig
    {
        public float FreezeDuration;
        public int SpeedPrecentModifier;
    }

    [Serializable]
    public class PoisonConfig : BurningConfig
    {
    }
}