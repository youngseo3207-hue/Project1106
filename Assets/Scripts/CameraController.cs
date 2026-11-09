 using UnityEngine;
 using UnityEngine.InputSystem;

public class CameraController : MonoBehaviour
{
    [Header("카메라 변환")]
    [SerializeField] private Transform _cameraTransform;
    [Header("카메라 기본 값")]
    [SerializeField] private Vector3 _defaultPosition;
    [SerializeField] private Vector3 _defaultRotation;
    [Header("카메라 변경 값")]
    [SerializeField] private Vector3 _changePosition;
    [SerializeField] private Vector3 _changeRotation;
    [Header("카메라 변경 속도")]
    [SerializeField] private float _transitionSpeed = 3f;
    [Header("카메라 기준점")] 
    [SerializeField] private Transform _cameraPivot;
    
    public bool IsChanged => _isChanged;

    private bool _isChanged = false;

    private void Update()
    {
        if (Keyboard.current != null && Keyboard.current.tabKey.wasPressedThisFrame)
        {
            _isChanged = !_isChanged;
        }
        
        MoveCamera();
    }
    
    private Vector3 GetTargetPosition()
    {
        if (_isChanged)
        {
            return _changePosition;
        }
        
        else
        {
            return _defaultPosition;
        }
    }

    private Quaternion GetTargetRotation()
    {
        if (_isChanged)
        {
            return Quaternion.Euler(_changeRotation);
        }
        else
        {
            return Quaternion.Euler(_defaultRotation);
        }
    }

    private void MoveCamera()
    {
        Vector3 targetPosition = GetTargetPosition();
        Quaternion targetRotation = GetTargetRotation();
        
        _cameraTransform.localPosition = Vector3.Lerp(_cameraTransform.localPosition, targetPosition, Time.deltaTime * _transitionSpeed);
        _cameraTransform.localRotation =
            Quaternion.Slerp(_cameraTransform.localRotation, targetRotation, Time.deltaTime * _transitionSpeed);
        
        // Vector3.Lerp(_cameraTransform.position, targetPosition, Time.deltaTime * _transitionSpeed);
        // Quaternion.Slerp(_cameraTransform.rotation, targetRotation, Time.deltaTime * _transitionSpeed);
    }

    // private void ToggleOrbit()
    // {
    //     _isOrbiting = !_isOrbiting;
    // }
    //
    // private void RotateCamera()
    // {
    //     if (!_isChanged || !_isOrbiting)
    //         return;
    // }
}
