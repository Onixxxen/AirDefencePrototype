using UnityEngine;

namespace Infrastructure.Repositories
{
    [CreateAssetMenu(fileName = "DroneSpawnConfig", menuName = "ScriptableObjects/Configs/DroneSpawnConfig")]
    public class DroneSpawnConfig : ScriptableObject, IDroneSpawnConfig
    {
        [SerializeField] private int _dronesCount = 5;
        [SerializeField] private float _spawnTimeInterval = 3f;
        [SerializeField] private float _minSpawnRadius = 60f;
        [SerializeField] private float _maxSpawnRadius = 90f;
        [SerializeField] private bool _isReplenished;

        public int DronesCount => _dronesCount;
        public float SpawnTimeInterval => _spawnTimeInterval;
        public float MinSpawnRadius => _minSpawnRadius;
        public float MaxSpawnRadius => _maxSpawnRadius;

        public bool IsReplenished => _isReplenished;
    }
}