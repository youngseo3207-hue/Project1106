using UnityEngine;
using UnityEngine.InputSystem;

public class MouseController : MonoBehaviour
{
    [SerializeField] private CameraController _cameraController;
    [SerializeField] private Transform _cameraPivot;
    [SerializeField] private float _rotateSpeed = 1f;
    private bool _isDragging = false;
    private Vector2 _mouseMove;

    private void Update()
    {
        mouseRotation();
        rotateCamera();
    }

    private void mouseRotation()
    {
        _isDragging = false;
        _mouseMove = Vector2.zero;

        if (!_cameraController.IsChanged || Mouse.current == null)
            return;

        if (Mouse.current.rightButton.isPressed)
        {
            _isDragging = true;
            _mouseMove = Mouse.current.delta.ReadValue();
        }
    }

    private void rotateCamera()
    {
        if (!_isDragging)
            return;
        
        float rotateAmount = _mouseMove.x * _rotateSpeed;
        _cameraPivot.Rotate(0f, rotateAmount, 0f);
    }
}
