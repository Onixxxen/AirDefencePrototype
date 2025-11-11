public class CameraZoomDTO
{
    public float OrthographicSize;
    public float ZoomDelta;

    public CameraZoomDTO(float orthographicSize, float zoomDelta)
    {
        OrthographicSize = orthographicSize;
        ZoomDelta = zoomDelta;
    }
}