using UnityEngine;

public class CameraRotation : MonoBehaviour
{
    [Range(0.1f, 10.0f)] [SerializeField]
    private float _horizontalRotationSpeed;
    [Range(0.1f, 10.0f)] [SerializeField] 
    private float _verticalRotationSpeed;
    [SerializeField] private float _verticalMinRotation;
    [SerializeField] private float _verticalMaxRotation;


    private float x = 0;
    private float y = 0;

    private void Start()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }
   
    private void LateUpdate()
    {
        x += Input.GetAxis("Mouse X") * _horizontalRotationSpeed;
        TryAddY(Input.GetAxis("Mouse Y") * _verticalRotationSpeed);
        Quaternion rotation = Quaternion.Euler(y, x, 0);
        transform.rotation = rotation;
    }

    private void TryAddY(float value)
    {
        y -= value;
        if(y < _verticalMinRotation) y = _verticalMinRotation;
        if (y > _verticalMaxRotation) y = _verticalMaxRotation;    
    }
}
