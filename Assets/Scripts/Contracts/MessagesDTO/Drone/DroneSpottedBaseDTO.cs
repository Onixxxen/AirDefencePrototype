using UnityEngine;

public class DroneSpottedBaseDTO
{
    public IDroneModel DroneModel;
    public Vector3 BasePosition;

    public DroneSpottedBaseDTO(IDroneModel droneModel, Vector3 basePosition)
    {
        DroneModel = droneModel;
        BasePosition = basePosition;
    }
}