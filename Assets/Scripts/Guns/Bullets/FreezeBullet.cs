public class FreezeBullet : EffectBullet
{
    public override void ApplyBulletEffect(IApplyableEffect effectTarget) => effectTarget.ApplyEffect(typeof(IApplyableFreeze));
}
