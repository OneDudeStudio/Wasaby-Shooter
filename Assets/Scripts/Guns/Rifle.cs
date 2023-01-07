using System;
using UnityEngine;

public class Rifle : Gun
{
    private bool _checker = true;
    [SerializeField] protected AudioClip clip1;
    [SerializeField] protected AudioClip clip2;
    [SerializeField] protected AudioSource _source;
    public override void TryShoot()
    {
        if (IsOutOfAmmo())
            return;
        if (_checker)
            _source.PlayOneShot(clip1);
        else
            _source.PlayOneShot(clip2);
        _checker = !_checker;
        _shootParticles.Play();
        _recoil.RecoilFire();
        if (Physics.Raycast(_playerCamera.transform.position, _playerCamera.transform.forward, out RaycastHit hit, _range))
        {
            Instantiate(_hitParticles, hit.point, Quaternion.LookRotation(hit.normal));
            if (hit.transform.TryGetComponent(out IApplyableDamage damaged))
            {
                if (!damaged.TryApplyDamage(CalculateDamage(hit.distance)))
                    return;
                //_bullet.ShootBullet(damaged);
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