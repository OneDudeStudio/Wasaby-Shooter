using System;
using UnityEngine;

public class Rifle : Gun
{
    //refactor
    //private bool _shootAudioClipChecker = true;
    //[SerializeField] protected AudioClip clip1;
    //[SerializeField] protected AudioClip clip2;
    //[SerializeField] protected AudioSource _source;

    protected override void TryShoot()
    {
        if(!TryDecreaseAmmo())
        {
            return;
        }

       //if (_shootAudioClipChecker)
       //{
       //    _source.PlayOneShot(clip1);
       //}

       //else
       //{
       //    _source.PlayOneShot(clip2);
       //}

       //_shootAudioClipChecker = !_shootAudioClipChecker;
       
        if (Physics.Raycast(_playerCamera.transform.position, _playerCamera.transform.forward, out RaycastHit hit, _range))
        {
            Instantiate(_hitParticles, hit.point, Quaternion.LookRotation(hit.normal));
            
            if (hit.transform.TryGetComponent(out IApplyableDamage damaged))
            {
                damaged.TryApplyDamage(CalculateDamage(hit.distance));
                
               //if (!damaged.TryApplyDamage(CalculateDamage(hit.distance)))
               //{
               //    return;
               //}
               
               //Refactor
               if (damaged != null)
               {
                   _bullet.ShootBullet(damaged);
               }
               return;
            }            
            _holePool.AddHole(hit);
        }    
        
    }
}


public interface IApplyableDamage
{
    public bool TryApplyDamage(float damage);
    public void Die();
    
}
public interface IApplyableEffect
{
    public void ApplyEffect(Type type);
}

public interface IApplyableBurning : IApplyableEffect
{
    public void StartBurning();
}
public interface IApplyableFreeze : IApplyableEffect
{
    public void StartFreeze();
}
public interface IApplyablePoison : IApplyableEffect
{
    public void Poison(float damage);

}
public interface IApplyableElectric : IApplyableEffect
{
    public void Electric(bool isStartPoint);
}