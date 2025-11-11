using System.Collections.Generic;
using Presentation.Views.Drone;
using UnityEngine;

public interface IDroneView
{
    void FlyAlongPath(IDroneModel drone, List<Vector3> path, float speedMultiplier, bool isDive);
    void DestroyDrone(bool isShotDown);
}