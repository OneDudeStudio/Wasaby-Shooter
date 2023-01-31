public class ReturnSpeedValueModifier : RecoilModifier
{
    private readonly float _returnSpeedPrecentModifier;
    public ReturnSpeedValueModifier(float precent)
    {
        _returnSpeedPrecentModifier = precent;
    }
    public override void Modify(Recoil recoil) => recoil.ReturnSpeedValue *= SupportFunctions.PrecentToFloat(_returnSpeedPrecentModifier);
    public override void Reset(Recoil recoil) => recoil.ReturnSpeedValue = recoil.ReturnSpeedValue;
}