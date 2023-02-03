public class FlashGrenade : Grenade
{
    public override void ExplosionPostEffects()
    {
        foreach(IApplyableDamage victim in _victims)
        {
            if (victim == null)
                continue;

            if(victim is IApplyableEffect changeable)
            {
                changeable.StartEffect<Stan>();
            }
        }
    }

    protected override GrenadeConfig GetConfig() => _config.Flash;
}
