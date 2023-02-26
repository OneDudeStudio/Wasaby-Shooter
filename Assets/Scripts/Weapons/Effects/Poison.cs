using UnityEngine;

public class Poison : Effect
{
    private bool _isPoisoned = false;
    private float _damage;
    private float _interval;

    private float _lastTickTime = 0;
    private IApplyableDamage _victim;

    public Poison(IApplyableDamage victim, EffectsConfig config)
    {
        _victim = victim;
        SetCharacteristics(config);
    }

    public override void Apply(float currentTime)
    {
        if(currentTime - _lastTickTime >= 0.9 * _interval)
        {
            _isPoisoned = false;
        }
    }

    public override void SetCharacteristics(EffectsConfig config)
    {
        _damage = config.Poison.Damage;
        _interval = config.Poison.Interval;
    }

    public override void StartEffect()
    {
        if (_isPoisoned)
            return;
        _victim.TryApplyDamage(_damage);
        _isPoisoned = true;
        _lastTickTime = Time.time;
    }
}
