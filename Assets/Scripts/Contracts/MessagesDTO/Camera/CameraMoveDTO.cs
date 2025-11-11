using UnityEngine;

public class CameraMoveDTO
{
    public Vector3 Position;
    public Vector3 CameraForward;
    public Vector3 CameraRight;
    public Vector2 InputDirection;

    public CameraMoveDTO(Vector3 position, Vector3 cameraForward, Vector3 cameraRight, Vector2 inputDirection)
    {
        Position = position;
        CameraForward = cameraForward;
        CameraRight = cameraRight;
        InputDirection = inputDirection;
    }
}