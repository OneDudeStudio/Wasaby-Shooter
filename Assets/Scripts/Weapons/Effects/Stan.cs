using UnityEngine;

public class Stan : Effect
{
    private bool _isStanned;

    private float _duration;

    private float _startTime;
    private ISpeedChangeable _victim;

    public Stan(ISpeedChangeable victim)
    {
        EffectsConfig config = Resources.Load<EffectsConfig>("EffectsConfig");
        _duration = config.Stan.StanDuration;
        _victim = victim;
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

    public override void StartEffect()
    {
        Debug.Log("Stan");
        _startTime = Time.time;
        _isStanned = true;
        _victim.ModifySpeed(0);
    }
}