using UnityEngine;

public class Electricity : Effect
{
    private bool _canBeDamaged = false;
    private IApplyableDamage _victim;
    private ElectricityController _electricityController;
    private float _damage;
    private float _cooldown;
    private float _lastHitTime = 0;

    public Electricity(IApplyableDamage victim, ElectricityController controller)
    {
        EffectsConfig config = Resources.Load<EffectsConfig>("EffectsConfig");
        _damage = config.Electric.Damage;
        _cooldown = config.Electric.Interval * config.Electric.MaxConductors * .8f;
        _electricityController = controller;
        _victim = victim;
    }

    public override void Apply(float currentTime)
    {
        if(currentTime - _lastHitTime >= _cooldown)
        {
            _canBeDamaged = true;
        }
    }

    public override void StartEffect()
    {
        _electricityController.TryStartLightningChain(_victim);
        if (!_canBeDamaged)
            return;
        _canBeDamaged = false;
        _lastHitTime = Time.time;
        _victim.TryApplyDamage(_damage);
    }
}