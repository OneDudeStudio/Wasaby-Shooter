using UnityEngine;

public class Freeze : Effect
{
    private bool _isFreezing;

    private float _duration;
    private float _speedModifier;

    private float _startTime;
    private ISpeedChangeable _victim;

    public Freeze(ISpeedChangeable victim, EffectsConfig config)
    {
        _victim = victim;
        SetCharacteristics(config);
    }

    public override void Apply(float currentTime)
    {
        if (!_isFreezing)
            return;

        if (currentTime - _startTime >= _duration)
        {
            _isFreezing = false;
            _victim.ResetSpeed();
        }
    }

    public override void SetCharacteristics(EffectsConfig config)
    {
        _duration = config.Freeze.FreezeDuration;
        _speedModifier = SupportFunctions.PrecentToFloat(config.Freeze.SpeedPrecentModifier);
    }

    public override void StartEffect()
    {
        _startTime = Time.time;
        _isFreezing = true;
        _victim.ModifySpeed(_speedModifier);
    }
}