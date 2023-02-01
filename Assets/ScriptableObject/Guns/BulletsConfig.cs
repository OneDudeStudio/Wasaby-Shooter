using System;
using UnityEngine;

[CreateAssetMenu(menuName = "Bullets Data")]
public class BulletsConfig : ScriptableObject
{
    [SerializeField] private DefaultBullet _default;
    [SerializeField] private FireBullet _fire;
    [SerializeField] private FreezeBullet _freeze;
    [SerializeField] private ElectricBullet _electric;

    public DefaultBullet Default => _default;
    public FireBullet Fire => _fire;
    public FreezeBullet Freeze => _freeze;
    public ElectricBullet Electric => _electric;
}

[Serializable]
public class BulletConfig
{
    public int AdditionalDamagePrecent; 
}
[Serializable]
public class DefaultBullet : BulletConfig
{ 
}
[Serializable]
public class FireBullet : BulletConfig
{
}
[Serializable]
public class FreezeBullet : BulletConfig
{
}
[Serializable]
public class ElectricBullet : BulletConfig
{
}

