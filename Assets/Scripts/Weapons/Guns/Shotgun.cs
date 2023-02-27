using UnityEngine;

public class Shotgun : Gun
{
    private int _pelletCount;
    private float _variance;

    protected override void SetStatsFromConfig()
    {
        base.SetStatsFromConfig();
        _pelletCount = ((ShotgunConfig)_gunConfig).PelletCount;
        _variance = ((ShotgunConfig)_gunConfig).Variance;
    }

    protected override void Shoot()
    {
        for (int i = 0; i < _pelletCount; i++)
        {
            Vector3 gauss = GaussDirection();
            Vector3 direction = gauss * _range;

            if (Physics.Raycast(_playerCamera.transform.position, gauss, out RaycastHit hit, _range))
            {
                direction = (hit.point - _shootingPoint.position).normalized * hit.distance;

                _gunVFX.ShowHitParticles(hit);

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

    private Vector3 GaussDirection()
    {
        float s = 0;
        float v = 0;
        float u = 0;

        while (s == 0 || s > 1)
        {
            v = Random.Range(-1f, 1f);
            u = Random.Range(-1f, 1f);
            s = v * v + u * u;
        }

        float sqrt = Mathf.Sqrt(-2 * Mathf.Log(s) / s);
        float z1 = _variance * u * sqrt;
        float z2 = _variance * v * sqrt;
        return _playerCamera.transform.forward + _playerCamera.transform.right * z1 / 10 + _playerCamera.transform.up * z2 / 10;
    }

}