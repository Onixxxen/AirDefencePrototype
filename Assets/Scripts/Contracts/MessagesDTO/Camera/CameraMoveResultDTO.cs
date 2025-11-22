using UnityEngine;

public struct CameraMoveResultDTO
{
    public Vector3 Position;

    public CameraMoveResultDTO(Vector3 position)
    {
        Position = position;
    }
}