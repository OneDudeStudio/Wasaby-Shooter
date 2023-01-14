using UnityEngine;

public class Shotgun : Gun
{
    [SerializeField] private int _pelletCount;
    [SerializeField] private float _variance;

   //[SerializeField] protected AudioClip clip1;
   //[SerializeField] protected AudioSource _source;

    protected override void TryShoot()
    {
        if (!TryDecreaseAmmo())
        {
            return;
        }
        //if (IsOutOfAmmo())
        //    return;
        // _source.PlayOneShot(clip1);
        for (int i = 0; i < _pelletCount; i++)
        {
            if (Physics.Raycast(_playerCamera.transform.position, GaussDirection(), out RaycastHit hit, _range))
            {
                if (hit.transform.TryGetComponent(out IApplyableDamage damaged))
                {
                    if (!damaged.TryApplyDamage(CalculateDamage(hit.distance)))
                        return;
                    return;
                }
                _holePool.AddHole(hit);
            }
        }
    }

    private Vector3 GaussDirection()
    {
        float s = 0;
        float v = 0;
        float u = 0;
        
        while(s == 0 || s > 1)
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