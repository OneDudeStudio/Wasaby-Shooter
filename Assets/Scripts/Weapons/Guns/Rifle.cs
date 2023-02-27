using UnityEngine;

public class Rifle : Gun
{
    protected override void Shoot()
    {
        Vector3 direction = _playerCamera.transform.forward * _range / 3;
        if (Physics.Raycast(_playerCamera.transform.position, _playerCamera.transform.forward, out RaycastHit hit, _range))
        {
            _gunVFX.ShowHitParticles(hit);
            
            if ((hit.point - _shootingPoint.position).sqrMagnitude > 1f)
            {
                direction = (hit.point - _shootingPoint.position).normalized * hit.distance;
            }

            if (hit.transform.TryGetComponent(out IApplyableDamage damaged))
            {
                _bullet.DealDamage(damaged, CalculateDamage(hit.distance));
            }
            else
            {
                _gunVFX.ShowBulletHole(hit);
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