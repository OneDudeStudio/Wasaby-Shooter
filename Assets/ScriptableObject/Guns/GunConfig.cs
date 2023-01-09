using System;
using UnityEngine;


[CreateAssetMenu(menuName = "Guns Data", fileName = "GunConfig")]
public class GunConfig : ScriptableObject
{
    [SerializeField] private RiffleGun _riffle;
    //[SerializeField] private ShotgunGun _shotgun;

    public RiffleGun riffle => _riffle;
   // public ShotgunGun shotgun => _shotgun;


}
[Serializable]
public class DefaultGun
{
    public float _defaultDamage;
    public int _defaultMaxAmmo;
    public float _defaultRange;
    public float _defaultIntervalTime;
    public AnimationCurve _damageByDistance;
}

[Serializable]
public class RiffleGun : DefaultGun
{
   
}

[Serializable]
public class ShotgunGun : DefaultGun
{
    public int _pelletCount;
    public float _variance;
} 
