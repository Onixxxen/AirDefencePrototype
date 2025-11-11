using UnityEngine;

[CreateAssetMenu(fileName = "CameraConfig", menuName = "ScriptableObjects/Configs/CameraConfig")]
public class CameraConfig : ScriptableObject, ICameraConfig
{
    [SerializeField] private float _cameraMoveSpeed = 20;
    [SerializeField] private float _cameraZoomSpeed = 0.6f;
    [SerializeField] private float _cameraRotateSpeed = 8;

    [SerializeField] private Vector2 _zoomBorder;
    
    public float CameraMoveSpeed => _cameraMoveSpeed;
    public float CameraZoomSpeed => _cameraZoomSpeed;
    public float CameraRotateSpeed => _cameraRotateSpeed;

    public Vector2 ZoomBorder => _zoomBorder;
}