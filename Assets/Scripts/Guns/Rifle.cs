using System;
using UnityEngine;

public class Rifle : Gun
{
    public override void TryShoot()
    {
        if (IsOutOfAmmo())
            return;
        
        _shootParticles.Play();
        _recoil.RecoilFire();
        if (Physics.Raycast(_playerCamera.transform.position, _playerCamera.transform.forward, out RaycastHit hit, _range))
        {
            Instantiate(_hitParticles, hit.point, Quaternion.LookRotation(hit.normal));
            if (hit.transform.TryGetComponent(out IApplyableDamage damaged))
            {
                if (!damaged.TryApplyDamage(CalculateDamage(hit.distance)))
                    return;
                _bullet.ShootBullet(damaged);
                //if (hit.transform.gameObject.TryGetComponent(out IApplyableEffect effectable))
                    //_bullet.ApplyBullectEffect(effectable);
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