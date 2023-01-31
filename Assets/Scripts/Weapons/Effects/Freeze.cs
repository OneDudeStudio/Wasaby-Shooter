using UnityEngine;

public class Freeze : Effect
{
    private bool _isFreezing;

    private float _duration;
    private float _speedModifier;

    private float _startTime;
    private IApplyableFreeze _victim;

    public Freeze(IApplyableFreeze victim)
    {
        EffectsConfig config = Resources.Load<EffectsConfig>("EffectsConfig");
        _duration = config.Freeze.FreezeDuration;
        _speedModifier = SupportFunctions.PrecentToFloat(config.Freeze.SpeedPrecentModifier);
        _victim = victim;
    }

    public override void Apply(float currentTime)
    {
        if (!_isFreezing)
            return;

        if (currentTime - _startTime >= _duration)
        {
            _isFreezing = false;
            _victim.ResetSpeed();
            Debug.Log("end freeze");
        }
    }

    public override void StartEffect()
    {
        _startTime = Time.time;
        _isFreezing = true;
        _victim.ModifySpeed(_speedModifier);
        Debug.Log("start freeze");
    }
}