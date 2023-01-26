public class RangeModifier : GunModifier
{
    private float _additionalRange;
    public RangeModifier(float additional)
    {
        _additionalRange = additional;
    }
    public override void Modify(Gun gun) => gun.Range += _additionalRange;
    public override void Reset(Gun gun) => gun.Range = gun.Range;
}