using UnityEngine;

public class Rifle : Gun
{
    protected override void Shoot()
    {
        if (Physics.Raycast(_playerCamera.transform.position, _playerCamera.transform.forward, out RaycastHit hit, _range))
        {
            Instantiate(_hitParticles, hit.point, Quaternion.LookRotation(hit.normal));
            
            if (hit.transform.TryGetComponent(out IApplyableDamage damaged))
            {
                _bullet.DealDamage(damaged, CalculateDamage(hit.distance));
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
public enum EffectType
{
    Burning,
    Freeze,
    Poison,
    Electric
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