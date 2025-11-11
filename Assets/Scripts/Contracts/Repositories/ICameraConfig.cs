using UnityEngine;

public interface ICameraConfig
{
    float CameraMoveSpeed { get; }
    float CameraZoomSpeed { get; }
    float CameraRotateSpeed { get; }
    Vector2 ZoomBorder { get; }
}