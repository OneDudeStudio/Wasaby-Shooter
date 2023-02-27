public class DefaultExplosionGrenade : Grenade
{
    protected override ExplosionConfig GetConfig() => LoadConfig().Default;
}
