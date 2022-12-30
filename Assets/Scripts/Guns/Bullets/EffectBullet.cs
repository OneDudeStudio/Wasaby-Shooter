using UnityEngine;

public abstract class EffectBullet : Bullet
{
    public override void ShootBullet(IApplyableDamage target, RaycastHit hit)
    {
        if(hit.transform.TryGetComponent(out IApplyableEffect effectable))
            ApplyBulletEffect(effectable);
    }
    public abstract void ApplyBulletEffect(IApplyableEffect target);
}
