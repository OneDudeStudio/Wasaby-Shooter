using UnityEngine;

public class Poison : Effect
{
    private bool _isPoisoned = false;
    private float _damage;
    private float _interval;

    private float _lastTickTime = 0;

    private IApplyableDamage _victim;
    public Poison(IApplyableDamage victim)
    {
        EffectsConfig config = Resources.Load<EffectsConfig>("EffectsConfig");
        _damage = config.Poison.Damage;
        _interval = config.Poison.Interval;
        _victim = victim;
        _lastTickTime = Time.time;
    }

    public override void Apply(float currentTime)
    {
        if(currentTime - _lastTickTime >= 0.9 * _interval)
        {
            _isPoisoned = false;
        }
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
