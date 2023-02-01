using System.Collections.Generic;

public class GunModule
{
    private readonly Gun _gun;
    protected List<GunModifier> _gunModifiers = new List<GunModifier>();
    protected List<RecoilModifier> _recoilModifiers = new List<RecoilModifier>();

    public GunModule(Gun gun)
    {
        _gun = gun;
    }
    public void ApplyModifiers()
    {
        foreach (GunModifier modifier in _gunModifiers)
        {
            modifier.Modify(_gun);
        }
        foreach (RecoilModifier modifier in _recoilModifiers)
        {
            modifier.Modify(_gun.Rec);
        }
    }
    public void ResetModifiers()
    {
        foreach (GunModifier modifier in _gunModifiers)
        {
            modifier.Reset(_gun);
        }
        foreach (RecoilModifier modifier in _recoilModifiers)
        {
            modifier.Reset(_gun.Rec);
        }
    }
}