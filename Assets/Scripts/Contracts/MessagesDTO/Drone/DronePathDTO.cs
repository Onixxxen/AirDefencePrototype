using System.Collections.Generic;
using UnityEngine;

public struct DronePathDTO
{
    public IDroneModel DroneModel;
    public List<Vector3> Path;
    public float SpeedMultiplier;
    public bool IsDive; 
    public IDroneView DroneView { get; }

    public DronePathDTO(IDroneModel model, List<Vector3> path, float speedMultiplier, bool isDive, IDroneView view)
    {
        DroneModel = model;
        Path = path;
        SpeedMultiplier = speedMultiplier;
        IsDive = isDive;
        DroneView = view;
    }
}