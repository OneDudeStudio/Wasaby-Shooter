using UnityEngine;

public class SilverBullet : DefaultBullet
{
    private float _additionalDamage = .5f;
    public override void ShootBullet(IApplyableDamage target, RaycastHit hit) => target.TryApplyDamage(_additionalDamage);
 
}
