public struct DroneDestroyDTO
{
    public IDroneView DroneView { get; }
    public bool IsShotDown;
    
    public DroneDestroyDTO(IDroneView droneView, bool isShotDown)
    {
        DroneView = droneView;
        IsShotDown = isShotDown;
    }
}