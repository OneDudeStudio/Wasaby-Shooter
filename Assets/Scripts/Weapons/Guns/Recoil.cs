using UnityEngine;

public class Recoil : MonoBehaviour
{
    [SerializeField] private Gun _gun;
    [SerializeField] private Transform _targetToRecoilEffect;


    private Vector3 _currentRotation;
    private Vector3 _targetRotation;

    private Vector3 _recoil;
    private float _returnSpeed;
    private float _snappines;


    private Vector3 _currentPosition;
    private Vector3 _targetPosition;
    private Vector3 _defaultPosition;

    private Vector3 _positionRecoil;
    private float _positionReturnSpeed;
    private float _positionSnappines;


    public Vector3 RecoilValue
    {
        get => _gun.Config.DefaultRecoil;
        set => _recoil = value;
    }
    public float ReturnSpeedValue
    {
        get => _gun.Config.DefaultReturnSpeed;
        set
        {
            if (value > 0)
            {
                _returnSpeed = value;
            }

        }
    }
    public float SnappinesValue
    {
        get => _gun.Config.DefaultSnappines;
        set => _snappines = value;
    }

    public Vector3 PositionRecoilValue
    {
        get => _gun.Config.DefaultPositionRecoil;
        set => _positionRecoil = value;
    }
    public float PositionReturnSpeedValue
    {
        get => _gun.Config.DefaultPositionReturnSpeed;
        set
        {
            if (value > 0)
            {
                _positionReturnSpeed = value;
            }

        }
    }
    public float PositionSnappinesValue
    {
        get => _gun.Config.DefaultPositionSnappines;
        set => _positionSnappines = value;
    }


    private void Awake()
    {
        _defaultPosition = transform.localPosition;
        _targetPosition = _defaultPosition;
        _currentPosition = _defaultPosition;
    }

    private void FixedUpdate()
    {
        _targetRotation = Vector3.Lerp(_targetRotation, Vector3.zero, _returnSpeed * Time.fixedDeltaTime);
        _currentRotation = Vector3.Slerp(_currentRotation, _targetRotation, _snappines * Time.fixedDeltaTime);
        _targetToRecoilEffect.localRotation = Quaternion.Euler(_currentRotation);

        _targetPosition = Vector3.Lerp(_targetPosition, _defaultPosition, _positionReturnSpeed * Time.fixedDeltaTime);
        _currentPosition = Vector3.Slerp(_currentPosition, _targetPosition, _positionSnappines * Time.fixedDeltaTime);
        transform.localPosition = _currentPosition;
    }

    public void RecoilFire()
    {
        _targetRotation += new Vector3(_recoil.x, Random.Range(-_recoil.y, _recoil.y), Random.Range(-_recoil.z, _recoil.z));
        _targetPosition += new Vector3(Random.Range(-_positionRecoil.x, _positionRecoil.x), Random.Range(-_positionRecoil.y, _positionRecoil.y), Random.Range(_positionRecoil.z / 2, _positionRecoil.z));
    }
}