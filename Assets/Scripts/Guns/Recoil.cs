using UnityEngine;

public class Recoil : MonoBehaviour
{
    private Vector3 _currentRotation;
    private Vector3 _targetRotation;

    [SerializeField] private Vector3 _recoil;

    [SerializeField] private float _returnSpeed;
    [SerializeField] private float _snappines;

    private void FixedUpdate()
    {
        _targetRotation = Vector3.Lerp(_targetRotation, Vector3.zero, _returnSpeed * Time.fixedDeltaTime);
        _currentRotation = Vector3.Slerp(_currentRotation, _targetRotation, _snappines * Time.fixedDeltaTime);
        transform.localRotation = Quaternion.Euler(_currentRotation);
    }

    public void RecoilFire() => _targetRotation += new Vector3(_recoil.x, Random.Range(-_recoil.y, _recoil.y), Random.Range(-_recoil.z, _recoil.z));
}
