using System.Collections.Generic;
using UnityEngine;

public class MissilePathDTO
{
    public List<Vector3> Path { get; }
    public float Speed { get; }

    public MissilePathDTO(List<Vector3> path, float speed)
    {
        Path = path;
        Speed = speed;
    }
}