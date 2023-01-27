using System.Collections;
using UnityEngine;

public class Burning : Effect
{
    private bool _isBurning = false;

    private int _ticks;
    private float _interval;
    private float _damage;

    private IApplyableDamage _damadable;

    public Burning(IApplyableDamage damageable)
    {
        EffectsConfig config = Resources.Load<EffectsConfig>("EffectsConfig");
        _ticks = config.Burning.Ticks;
        _interval = config.Burning.Interval;
        _damage = config.Burning.Damage;

        _damadable = damageable;
    }

    public override void Apply()
    {
        throw new System.NotImplementedException();
    }

    //public override void Apply()
    //{
    //    if (_isBurning)
    //        StopCoroutine("BurningCoroutine");
    //    StartCoroutine("BurningCoroutine");
    //    _isBurning = true;
    //}

    public IEnumerator BurningCoroutine()
    {
        for (int i = 0; i < _ticks; i++)
        {
            yield return new WaitForSeconds(_interval);
            _damadable.TryApplyDamage(_damage);
        }
        _isBurning = false;
    }
}