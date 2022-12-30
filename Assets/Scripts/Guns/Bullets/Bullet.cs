using UnityEngine;

public abstract class Bullet
{
    public abstract void ShootBullet(IApplyableDamage target, RaycastHit hit);
}
