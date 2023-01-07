public class FreezeBullet : EffectBullet
{
    //precent of dammage to add to current for freeze bullet
    public FreezeBullet() => _additionalDamagePrecent = 30;
    public override void ApplyBulletEffect(IApplyableEffect effectTarget) => effectTarget.ApplyEffect(typeof(IApplyableFreeze));
}
