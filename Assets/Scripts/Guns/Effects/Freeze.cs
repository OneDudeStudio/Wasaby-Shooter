using System;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;

public class Freeze : Effect
{
    private bool _isFreezing;

    private float _duration;
    private int _speedPrecentModifier;

    private CancellationTokenSource _cancellationToken;
    public Freeze()
    {
        _cancellationToken = new CancellationTokenSource();
        EffectsConfig config = Resources.Load<EffectsConfig>("EffectsConfig");
        _duration = config.Freeze.FreezeDuration;
        _speedPrecentModifier = config.Freeze.SpeedPrecentModifier;
    }
    public override void Apply(IApplyableEffect target)
    {
        TryStop();
        _cancellationToken = new CancellationTokenSource();
        _isFreezing = true;
        if (target is IApplyableFreeze freezeTarget)
        {
            Execute(freezeTarget).Wait();
        }
        TryStop();
       

    }
    public void TryStop()
    {
        if (_isFreezing)
        {
            _cancellationToken.Cancel();
            _isFreezing = false;
        }
    }

    public async Task Execute(IApplyableFreeze target)
    {
        await Task.Run(async () =>
        {
            target.ModifySpeed(SupportFunctions.PrecentToFloat(_speedPrecentModifier));
            await Task.Delay((int)(_duration * 1000));
            target.ResetSpeed();
        }, _cancellationToken.Token);
    }
}