using UnityEngine;

public class GrenadeLauncher : MonoBehaviour
{
    [SerializeField] private Grenade _currentGrenade;
    [SerializeField] private Transform _grenadeSpawnPoint;
    [SerializeField] private Transform _cameraTransform;

    [SerializeField] private float _throwForce;
    [SerializeField] private float _upwardForce;
    public void ShootGranade()
    {
        Grenade grenade = Instantiate(_currentGrenade, _grenadeSpawnPoint.position, _cameraTransform.rotation);
        Rigidbody rb = grenade.GetComponent<Rigidbody>();
        Vector3 direction = _cameraTransform.forward;
        if (Physics.Raycast(_cameraTransform.position, _cameraTransform.forward, out RaycastHit hit, 300f))
            direction = (hit.point - _grenadeSpawnPoint.position).normalized;

        Vector3 force = direction * _throwForce + transform.up * _upwardForce;
        rb.AddForce(force, ForceMode.Impulse);
    }
}
