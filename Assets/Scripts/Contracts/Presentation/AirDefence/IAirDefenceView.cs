using System.Collections.Generic;
using UnityEngine;

public interface IAirDefenceView
{
    void Shot(List<Vector3> path, float speed);
}