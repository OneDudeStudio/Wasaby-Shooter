public class FreezeBullet : EffectBullet
{
    public override void ApplyBulletEffect(IApplyableEffect target) => target.ApplyEffect(typeof(IApplyableFreeze));
}
