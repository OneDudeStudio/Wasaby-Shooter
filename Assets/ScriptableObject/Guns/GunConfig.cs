using System;
using UnityEngine;


[CreateAssetMenu(menuName = "Guns Data")]

public class GunConfig : ScriptableObject
{
    [Header("Gun Stats")]
    public float _defaultDamage;
    public int _defaultMaxAmmo;
    public float _defaultRange;
    public float _defaultIntervalTime;
    public AnimationCurve _damageByDistance;

    [Space]
    [Header("Recoil Stats")]
    public Vector3 _defaultRecoil;
    public float _defaultReturnSpeed;
    public float _defaultSnappines;
    public Vector3 _defaultPositionRecoil;
    public float _defaultPositionReturnSpeed;
    public float _defaultPositionSnappines;



    [Space]
    [Header("Potential Gun Modules")] 
    [SerializeField] private DamageModule _damageModule;
    [SerializeField] private ExtendedMag _extendedMagModule;
    [SerializeField] private RangeModule _rangeModule;

    public DamageModule Damage => _damageModule;
    public ExtendedMag Mag => _extendedMagModule;
    public RangeModule Range => _rangeModule;

    [Serializable]
    public class ModuleConfig
    {
    }

    [Serializable]
    public class DamageModule : ModuleConfig
    {
        public float DamagePrecentMultiplier;
        public float AdditionalRange;
    }

    [Serializable]
    public class ExtendedMag : ModuleConfig
    {
        public int AdditioanalAmmo;
    }
    
    [Serializable]
    public class RangeModule : ModuleConfig
    {
        public float DamagePrecentMultiplier;
        public float AdditionalRange;
    }

}