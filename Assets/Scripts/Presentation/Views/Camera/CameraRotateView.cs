using UnityEngine;
using UnityEngine.InputSystem;
using VContainer;

public class CameraRotateView : MonoBehaviour, ICameraRotateView
{
    [SerializeField] private GameObject _rotateObject;

    public void RotateCamera(float deltaX, float rotateSpeed)
    {
        _rotateObject.transform.Rotate(Vector3.up, deltaX * rotateSpeed * Time.deltaTime, Space.World);
    }
}