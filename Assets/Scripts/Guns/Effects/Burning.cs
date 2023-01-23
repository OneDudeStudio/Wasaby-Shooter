using System;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;

public class Burning : Effect
{
    private bool _isBurning;

    private int _ticks;
    private float _interval;
    private float _damage;

    private CancellationTokenSource _cancellationToken;
    public Burning()
    {
        _cancellationToken = new CancellationTokenSource();
        EffectsConfig config = Resources.Load<EffectsConfig>("EffectsConfig");
        _ticks = config.Burning.Ticks;
        _interval = config.Burning.Interval;
        _damage = config.Burning.Damage;
    }
    public override void Apply(IApplyableEffect target)
    {
        TryStop();
        _cancellationToken = new CancellationTokenSource();
        _isBurning = true;
        if (target is IApplyableBurning burningTarget)
        {
            Execute(burningTarget).Wait();
        }
        TryStop();

    }
    public void TryStop()
    {
        if(_isBurning)
        {
            _cancellationToken.Cancel();
            _isBurning = false;
        }
    }

    public async Task Execute(IApplyableBurning target)
    {
        await Task.Run(async () =>
        {
        for (int i = 0; i < _ticks; i++)
            {
                await Task.Delay((int)(_interval * 1000));
                ((IApplyableDamage)target).TryApplyDamage(_damage);
            }

        }, _cancellationToken.Token);
    }
}