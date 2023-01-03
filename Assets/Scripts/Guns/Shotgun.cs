using UnityEngine;

public class Shotgun : Gun
{
    [SerializeField] private int _pelletCount;
    [SerializeField] private float _koef1;
    [SerializeField] private float _koef2;
    public override void TryShoot()
    {
        if (IsOutOfAmmo())
            return;

        _shootParticles.Play();
        _recoil.RecoilFire();
        for (int i = 0; i < _pelletCount; i++)
        {
            float v = _koef1 * Random.Range(-1f, 1f) - _koef2;
            float u = _koef1 * Random.Range(-1f, 1f) - _koef2;
            float s = v * v + u * u;
            //float x = Mathf.Sqrt(-2 * Mathf.Log(v)) * Mathf.Cos(2 * Mathf.PI * u);
            //float y = Mathf.Sqrt(-2 * Mathf.Log(v)) * Mathf.Sin(2 * Mathf.PI * u);
            float a = Mathf.Sqrt(-2 * Mathf.Log(s) / s);
            float z1 = u * a;
            float z2 = v * a;
            //Debug.Log(x);
            //Debug.Log(y);
            if (Physics.Raycast(_playerCamera.transform.position, 
                _playerCamera.transform.forward + _playerCamera.transform.right * z1 * .1f + _playerCamera.transform.up * z2 * .1f,
                out RaycastHit hit, _range))
            {
                if (hit.transform.TryGetComponent(out IApplyableDamage damaged))
                {
                    if (!damaged.TryApplyDamage(CalculateDamage(hit.distance)))
                        return;
                }
                _holePool.AddHole(hit);
            }
        }
    }
}
