using MessagePipe;
using UnityEngine;
using UnityEngine.InputSystem;
using VContainer;

public class CameraController : MonoBehaviour
{
    [Inject] private readonly IPublisher<CameraMoveDTO> _movePublisher;
    [Inject] private readonly IPublisher<CameraZoomDTO> _zoomPublisher;
    [Inject] private readonly IPublisher<CameraRotateResultDTO> _cameraRotateResultPublisher;
    [Inject] private readonly ICameraConfig _cameraConfig;
    [Inject] private readonly PlayerInputActions _inputActions;

    [SerializeField] private Camera _camera;

    private Vector2 _currentMoveInput;
    private bool _middleMouseHeld;
    private Vector2 _middleDragDelta;
    
    private float _currentZoomInput;
    
    private bool _rightMouseHeld;
    private Vector2 _rotateDelta;
    
    public void Awake()
    {
        _inputActions.Enable();

        _inputActions.Player.Move.performed += OnMovePerformed;
        _inputActions.Player.Move.canceled += OnMoveCanceled;
        _inputActions.Player.MiddleDrag.performed += OnMiddleDrag;
        
        _inputActions.Player.Zoom.performed += OnZoomPerformed;
        _inputActions.Player.Zoom.canceled += OnZoomCanceled;
        
        _inputActions.Player.RightRotate.performed += OnRightRotatePerformed;
        _inputActions.Player.RightRotate.canceled += OnRightRotateCanceled;
    }

    private void Update()
    {
        TryKeyboardMove();
        TryMiddleMouseMove();
        TryZoom();
        TryRotate();
    }

    private void TryKeyboardMove()
    {
        if (_currentMoveInput != Vector2.zero)
        {
            _movePublisher.Publish(new CameraMoveDTO(_camera.transform.position, _camera.transform.forward,
                _camera.transform.right, _currentMoveInput));
        }
    }

    private void TryMiddleMouseMove()
    {
        if (_middleDragDelta != Vector2.zero)
        {
            Vector2 dragInput = new Vector2(-_middleDragDelta.x, -_middleDragDelta.y);
            _movePublisher.Publish(new CameraMoveDTO(_camera.transform.position, _camera.transform.forward,
                _camera.transform.right, dragInput));
            _middleDragDelta = Vector2.zero;
        }
    }

    private void TryZoom()
    {
        if (_currentZoomInput != 0)
        {
            _zoomPublisher.Publish(new CameraZoomDTO(_camera.orthographicSize, _currentZoomInput));
            _currentZoomInput = 0; 
        }
    }

    private void TryRotate()
    {
        if (_rightMouseHeld)
        {
            var delta = _inputActions.Player.RotateDelta.ReadValue<Vector2>();
            if (delta != Vector2.zero)
                _cameraRotateResultPublisher.Publish(new CameraRotateResultDTO(delta.x, _cameraConfig.CameraRotateSpeed));
        }
    }

    private void OnMiddleDrag(InputAction.CallbackContext ctx) =>
        _middleDragDelta = Mouse.current.middleButton.isPressed 
        ? _middleDragDelta = ctx.ReadValue<Vector2>()
        : _middleDragDelta = Vector2.zero;

    private void OnMovePerformed(InputAction.CallbackContext context) => _currentMoveInput = context.ReadValue<Vector2>();
    private void OnMoveCanceled(InputAction.CallbackContext context) => _currentMoveInput = Vector2.zero;

    private void OnZoomPerformed(InputAction.CallbackContext context) => _currentZoomInput = context.ReadValue<Vector2>().y;
    private void OnZoomCanceled(InputAction.CallbackContext context) => _currentZoomInput = 0;

    private void OnRightRotatePerformed(InputAction.CallbackContext ctx) => _rightMouseHeld = true;
    private void OnRightRotateCanceled(InputAction.CallbackContext ctx) => _rightMouseHeld = false;

    private void OnDestroy()
    {
        _inputActions.Player.Move.performed -= OnMovePerformed;
        _inputActions.Player.Move.canceled -= OnMoveCanceled;
        _inputActions.Player.MiddleDrag.performed -= OnMiddleDrag;
        
        _inputActions.Player.Zoom.performed -= OnZoomPerformed;
        _inputActions.Player.Zoom.canceled -= OnZoomCanceled;
        
        _inputActions.Player.RightRotate.performed -= OnRightRotatePerformed;
        _inputActions.Player.RightRotate.canceled -= OnRightRotateCanceled;
    }
}