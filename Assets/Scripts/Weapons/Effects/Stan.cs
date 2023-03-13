using UnityEngine;

public class Stan : Effect
{
    private bool _isStanned;

    private float _duration;

    private float _startTime;
    private ISpeedChangeable _victim;

    public Stan(ISpeedChangeable victim, EffectsConfig config)
    {
        _victim = victim;
        SetCharacteristics(config);
    }

    public override void Apply(float currentTime)
    {
        if (!_isStanned)
            return;
        
        if (currentTime - _startTime >= _duration)
        {
            _isStanned = false;
            _victim.ResetSpeed();
        }
    }

    public override void SetCharacteristics(EffectsConfig config)
    {
        _duration = config.Stan.StanDuration;
    }

    public override void StartEffect()
    {
        _startTime = Time.time;
        _isStanned = true;
        _victim.ModifySpeed(0);
    }
}