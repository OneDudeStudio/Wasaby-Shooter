using UnityEngine;

[CreateAssetMenu(menuName = "ShotgunsData")]
public class ShotgunConfig : GunConfig
{
    [Space]
    public int PelletCount;
    public float Variance;
}
