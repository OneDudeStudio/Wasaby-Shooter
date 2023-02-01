public class SnappinesValueModifier : RecoilModifier
{
    private readonly float _snappinesPrecentModifier;
    public SnappinesValueModifier(float precent)
    {
        _snappinesPrecentModifier = precent; 
    }
    public override void Modify(Recoil recoil) => recoil.SnappinesValue *= SupportFunctions.PrecentToFloat(_snappinesPrecentModifier);
    public override void Reset(Recoil recoil) => recoil.SnappinesValue = recoil.SnappinesValue;
}