using System;
using UnityEngine;


[CreateAssetMenu(menuName = "Guns Data")]

public class GunConfig : ScriptableObject
{
    [Header("Stats")]
    public float _defaultDamage;
    public int _defaultMaxAmmo;
    public float _defaultRange;
    public float _defaultIntervalTime;
    public Vector3 _defaultRecoil;
    public float _defaultReturnSpeed;
    public float _defaultSnappines;

    public AnimationCurve _damageByDistance;
    
    
    [Header("Potential Gun Modules")] 
    [SerializeField] private DamageModule _damageModule;
    [SerializeField] private ExtendedMag _extendedMagModule;
    [SerializeField] private RangeModule _rangeModule;

    public DamageModule Damage => _damageModule;
    public ExtendedMag Mag => _extendedMagModule;
    public RangeModule Range => _rangeModule;

    [Serializable]
    public class Module
    {
        public bool isActive;
    }

    [Serializable]
    public class DamageModule : Module
    {
        public float DamagePrecentMultiplier;
        public float AdditionalRange;
    }

    [Serializable]
    public class ExtendedMag : Module
    {
        public int AdditioanalAmmo;
    }
    
    [Serializable]
    public class RangeModule : Module
    {
        public float DamagePrecentMultiplier;
        public float AdditionalRange;
    }

}