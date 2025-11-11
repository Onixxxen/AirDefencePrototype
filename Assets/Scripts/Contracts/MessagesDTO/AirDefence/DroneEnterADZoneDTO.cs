using UnityEngine;

public struct DroneEnterADZoneDTO
{
    public Vector3 DronePosition { get; }
    public Vector3 DroneVelocity { get; }
    public Vector3 ADPosition { get; } 

    public DroneEnterADZoneDTO(Vector3 dronePosition, Vector3 droneVelocity, Vector3 adPosition)
    {
        DronePosition = dronePosition;
        DroneVelocity = droneVelocity;
        ADPosition = adPosition;
    }
}