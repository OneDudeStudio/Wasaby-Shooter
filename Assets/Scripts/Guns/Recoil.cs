using UnityEngine;

public class Recoil : MonoBehaviour
{
    private Vector3 _currentRotation;
    private Vector3 _targetRotation;

    [SerializeField] private Vector3 _DefaultRecoil;
    [SerializeField] private float _DefaultReturnSpeed;
    [SerializeField] private float _DefaultSnappines;

    private Vector3 _recoil;
    private float _returnSpeed;
    private float _snappines;

    private void FixedUpdate()
    {
        _targetRotation = Vector3.Lerp(_targetRotation, Vector3.zero, _returnSpeed * Time.fixedDeltaTime);
        _currentRotation = Vector3.Slerp(_currentRotation, _targetRotation, _snappines * Time.fixedDeltaTime);
        transform.localRotation = Quaternion.Euler(_currentRotation);
    }

    public void RecoilFire() => _targetRotation += new Vector3(_recoil.x,
        Random.Range(-_recoil.y, _recoil.y), Random.Range(-_recoil.z, _recoil.z));
    public void SetRecoil(Vector3 recoil) => _recoil = recoil;
    public void SetReturnSpeed(float speed)
    {
        if(speed > 0)
            _returnSpeed = speed;
    }
    public void SetSnappines(float snappines)
    {
        if (snappines > 0)
            _snappines = snappines;
    }
    public Vector3 GetRecoil() => _DefaultRecoil;
    public float GetReturnSpeed() => _DefaultReturnSpeed;
    public float GetSnappines() => _DefaultSnappines;
}
