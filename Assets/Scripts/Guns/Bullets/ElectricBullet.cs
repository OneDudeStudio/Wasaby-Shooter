public class ElectricBullet : EffectBullet
{
    //precent of dammage to add to current for electric bullet
    public ElectricBullet() => _additionalDamagePrecent = -30;
   // public override void ApplyBulletEffect(IApplyableEffect effectTarget) => effectTarget.ApplyEffect(typeof(IApplyableElectric));
}
