using System.Threading;
using System.Threading.Tasks;
using UnityEngine;

public class Burning : Effect
{
    private bool _isBurning = false;

    private int _ticks;
    private float _interval;
    private float _damage;

    private CancellationTokenSource _tokenSource;
    private CancellationToken _token;
    public Burning()
    {
        _tokenSource = new CancellationTokenSource();
        _token = _tokenSource.Token;
        EffectsConfig config = Resources.Load<EffectsConfig>("EffectsConfig");
        _ticks = config.Burning.Ticks;
        _interval = config.Burning.Interval;
        _damage = config.Burning.Damage;


    }
    public override void Apply(IApplyableEffect target)
    {
        TryStop();
        _tokenSource = new CancellationTokenSource();
        _token = _tokenSource.Token;
        _isBurning = true;
        if (target is IApplyableBurning burningTarget)
        {
            //await Execute(burningTarget, _token);
            DangerExecute(burningTarget).Wait();
        }
        TryStop();
        Debug.Log("end");
    }
    public void TryStop()
    {
        if (_isBurning)
        {
            _tokenSource.Cancel();
        }
        _isBurning = false;
    }

    public async Task Execute(IApplyableBurning target, CancellationToken token)
    {
        for (int i = 0; i < _ticks; i++)
        {
            if (token.IsCancellationRequested)
            {
                return;
            }
            await Task.Delay((int)(_interval * 1000));
            if (token.IsCancellationRequested)
            {
                return;
            }
            ((IApplyableDamage)target).TryApplyDamage(_damage);
        }
    }

    public async Task DangerExecute(IApplyableBurning target)
    {
        await Task.Run(async () =>
        {
            for (int i = 0; i < _ticks; i++)
            {
                await Task.Delay((int)(_interval * 1000));
                ((IApplyableDamage)target).TryApplyDamage(_damage);
            }
        }, _tokenSource.Token);
    }
}