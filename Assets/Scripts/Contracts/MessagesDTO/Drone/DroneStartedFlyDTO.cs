using UnityEngine;

public struct DroneStartedFlyDTO
{
    public IDroneModel DroneModel;
    public Vector3 CurrentPosition;
    public Vector3 BasePosition;
    
    public DroneStartedFlyDTO(IDroneModel droneModel, Vector3 currentPosition, Vector3 basePosition)
    {
        DroneModel = droneModel;
        CurrentPosition = currentPosition;
        BasePosition = basePosition;
    }
}