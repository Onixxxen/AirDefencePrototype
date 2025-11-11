using UnityEngine;

public interface ICameraMoveService
{
    void SetPosition(Vector3 position, Vector3 cameraForward, Vector3 cameraRight, Vector2 inputDirection);
}