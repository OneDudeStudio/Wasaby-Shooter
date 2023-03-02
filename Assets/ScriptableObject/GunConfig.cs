using System;
using UnityEngine;

[CreateAssetMenu(menuName = "Guns Data")]
public class GunConfig : ScriptableObject
{
    [Header("Gun Stats")]
    public float DefaultDamage;
    public int DefaultMaxAmmo;
    public float DefaultRange;
    public float DefaultIntervalTime;
    public float ReloadDuration;
    public AnimationCurve DamageByDistance;
    public LayerMask AffectingLayers;

    [Space]
    [Header("Recoil Stats")]
    public Vector3 DefaultRecoil;
    public float DefaultReturnSpeed;
    public float DefaultSnappines;
    public Vector3 DefaultPositionRecoil;
    public float DefaultPositionReturnSpeed;
    public float DefaultPositionSnappines;

    [Space]
    [Header("Potential Gun Modules")] 
    [SerializeField] private DamageModule _damageModule;
    [SerializeField] private ExtendedMag _extendedMagModule;
    [SerializeField] private RangeModule _rangeModule;

    public DamageModule Damage => _damageModule;
    public ExtendedMag Mag => _extendedMagModule;
    public RangeModule Range => _rangeModule;

    [Serializable]
    public struct DamageModule
    {
        public float DamagePrecentMultiplier;
        public float AdditionalRange;
    }

    [Serializable]
    public struct ExtendedMag
    {
        public int AdditioanalAmmo;
    }
    
    [Serializable]
    public struct RangeModule
    {
        public float DamagePrecentMultiplier;
        public float AdditionalRange;
    }

}