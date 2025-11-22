public struct CameraRotateResultDTO
{
    public float DeltaX;
    public float RotateSpeed;

    public CameraRotateResultDTO(float deltaX, float rotateSpeed)
    {
        DeltaX = deltaX;
        RotateSpeed = rotateSpeed;
    }
}