using UnityEngine;

public struct SpawnDroneDTO
{
    public IDroneModel Model { get; }
    public IDroneView View { get; }
    public Vector3 Position { get; }
    public Vector3 BasePosition { get; }

    public SpawnDroneDTO(IDroneModel model, IDroneView view, Vector3 position, Vector3 basePosition)
    {
        Model = model;
        View = view;
        Position = position;
        BasePosition = basePosition;
    }
}