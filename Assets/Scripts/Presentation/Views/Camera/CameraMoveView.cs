using UnityEngine;

public class CameraMoveView : MonoBehaviour, ICameraMoveView
{
    [SerializeField] private GameObject _camera;
    
    public void SetNewPosition(Vector3 position)
    {
        _camera.transform.position = position;
    }
}
