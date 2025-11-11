using UnityEngine;

public class CameraZoomView : MonoBehaviour, ICameraZoomView
{
    [SerializeField] private Camera _camera;
    
    public void SetNewZoom(float zoom)
    {
        _camera.orthographicSize = zoom;
    }
}