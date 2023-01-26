public class IntervalModifier : GunModifier
{
    private float _intervalPrecentModifier;
    public IntervalModifier(float precent)
    {
        _intervalPrecentModifier = precent;
    }
    public override void Modify(Gun gun) => gun.Interval *= SupportFunctions.PrecentToFloat(_intervalPrecentModifier);
    public override void Reset(Gun gun) => gun.Interval = gun.Interval;
}