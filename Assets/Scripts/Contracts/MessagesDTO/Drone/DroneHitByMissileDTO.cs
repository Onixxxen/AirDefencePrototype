public struct DroneHitByMissileDTO
{
    public IDroneModel Drone { get; }
    public int Award { get; }

    public DroneHitByMissileDTO(IDroneModel drone)
    {
        Drone = drone;
        Award = drone.Award;
    }
}