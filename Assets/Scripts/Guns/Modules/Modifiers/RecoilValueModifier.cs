public class RecoilValueModifier : RecoilModifier
{
    private float _recoilPrecentModifier;
    public RecoilValueModifier(float precent)
    {
        _recoilPrecentModifier = precent;
    }
    public override void Modify(Recoil recoil) => recoil.RecoilValue *= SupportFunctions.PrecentToFloat(_recoilPrecentModifier);
    public override void Reset(Recoil recoil) => recoil.RecoilValue = recoil.RecoilValue;
}
