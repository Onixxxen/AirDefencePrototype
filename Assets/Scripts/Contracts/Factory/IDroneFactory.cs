using Infrastructure.Repositories;
using UnityEngine;

public interface IDroneFactory
{
    void SpawnDrone(IDroneConfig config, float minSpawnRadius, float maxSpawnRadius, Transform baseTransform);
}