public class ElectricBullet : EffectBullet
{
    public override void ApplyBulletEffect(IApplyableEffect effectTarget) => effectTarget.ApplyEffect(typeof(IapplyableElectric));
}
