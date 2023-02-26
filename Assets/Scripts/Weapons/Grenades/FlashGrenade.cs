public class FlashGrenade : Grenade
{
    protected override void SetExplosionDamageDealer()
    {
        _explosion.SetDamageDealer(new EffectExplosionDamageDealer<Stan>());
    }

    protected override ExplosionConfig GetConfig() => LoadConfig().Flash;

}
