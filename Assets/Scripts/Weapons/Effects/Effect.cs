public abstract class Effect
{
    public abstract void Apply(float currentTime);
    public abstract void StartEffect();
    public abstract void SetCharacteristics(EffectsConfig config);
}
