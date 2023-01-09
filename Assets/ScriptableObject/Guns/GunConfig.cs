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
    public AnimationCurve _damageByDistance;
    
    [Header("Potential Gun Modules")] 
    public RangeModule rangeModule;
    public TestDamageModule damageModule;
    public SupportModule supportModule;
    
    [Serializable]
    public class Module
    {
        public bool isActive;
    }

    [Serializable]
    public class TestDamageModule : Module
    {
        public float DamageMultiplier;
    }

    [Serializable]
    public class RangeModule : Module
    {
        public float multiplier;
        public float rangePersentMultiplier;
        
        //public float SetNewRange(float range)
        //{
        //    return range *= multiplier;
        //}
    }
    
    [Serializable]
    public class SupportModule : Module
    {
        public float multiplier;
        public float rangePersentMultiplier;
    }

}