using System.Collections;
using UnityEngine;

public class Burning : Effect
{
    private bool _isBurning = false;

    private int _ticks;
    private float _interval;
    private float _damage;

    private float _lastTickTime;
    private int _currentTicks;

    private IApplyableDamage _victim;

    public Burning(IApplyableDamage damageable, EffectsConfig config)
    {
        _victim = damageable;
        SetCharacteristics(config);
    }

    public override void Apply(float currentTime)
    {
        if (!_isBurning)
            return;

        if(currentTime - _lastTickTime >= _interval)
        {
            _victim.TryApplyDamage(_damage);
            _currentTicks++;
            if (isEffectEnded())
            {
                _isBurning = false;
                return;
            }
            _lastTickTime = currentTime;
        }
    }

    public override void SetCharacteristics(EffectsConfig config)
    {
        _ticks = config.Burning.Ticks;
        _interval = config.Burning.Interval;
        _damage = config.Burning.Damage;
    }

    public override void StartEffect()
    {
        _currentTicks = 0;
        _lastTickTime = Time.time;
        _isBurning = true;
    }

    private bool isEffectEnded() => _currentTicks >= _ticks;

}