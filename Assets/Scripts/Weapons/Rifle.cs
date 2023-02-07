using UnityEngine;

public class Rifle : Gun
{
    protected override void Shoot()
    {
        Vector3 direction = _playerCamera.transform.forward * _range/3;
        if (Physics.Raycast(_playerCamera.transform.position, _playerCamera.transform.forward, out RaycastHit hit, _range))
        {
            direction = (hit.point - _shootingPoint.position).normalized * hit.distance;
            Instantiate(_hitParticles, hit.point, Quaternion.LookRotation(hit.normal));
       
            if (hit.transform.TryGetComponent(out IApplyableDamage damaged))
            {
                _bullet.DealDamage(damaged, CalculateDamage(hit.distance));
            }
            else
            {
                _holePool.AddHole(hit);
            }           
        }
        _gunVFX.ShowGunTracer(direction);
    }
}


public interface IApplyableDamage
{
    public bool TryApplyDamage(float damage);
    public void Die();

}

public interface IApplyableEffect
{
    public void StartEffect<T>() where T : Effect;
    public void ApplyEffects(float currentTime);
}

public interface ISpeedChangeable
{
    public void ModifySpeed(float modifier);
    public void ResetSpeed();
}