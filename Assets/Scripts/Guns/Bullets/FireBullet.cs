//using System.Threading.Tasks;

public class FireBullet : EffectBullet
{
    public override void ApplyBulletEffect(IApplyableEffect target) => target.ApplyEffect(typeof(IApplyableBurning));


    //private int _ticks = 5;
    // private float _interval = 1f;
    // private float _fireDamage = 1f;
    //private async Task Burning(IApplyableDamage target)
    //{
    //    int millisecondInterval = (int)(_interval * 1000);
    //    for(int i=0; i < _ticks; i++)
    //    {
    //        if (target.Equals(null))
    //            break;
    //        target.TryApplyDamage(_fireDamage);
    //        await Task.Delay(millisecondInterval);
    //    }        
    //}
}
