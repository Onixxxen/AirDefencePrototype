using UnityEngine;
using VContainer;

namespace Controller.Drone
{
    public class DroneSpawnerController : MonoBehaviour
    {
        [Inject] private readonly IDroneFactory _droneFactory;
        [Inject] private readonly IDroneConfig _droneConfig;
        [Inject] private readonly IDroneSpawnConfig _droneSpawnConfig;
        
        [SerializeField] private Transform _baseTransform;
        
        private float _timer;

        public void Start()
        {
            SpawnDrone();
            _timer = _droneSpawnConfig.SpawnTimeInterval;
        }

        public void Update()
        {
            _timer -= Time.deltaTime;
            if (_timer <= 0f)
            {
                SpawnDrone();
                _timer = _droneSpawnConfig.SpawnTimeInterval;
            }
        }

        private void SpawnDrone()
        {
            _droneFactory.SpawnDrone(
                config: _droneConfig,
                minSpawnRadius: _droneSpawnConfig.MinSpawnRadius,
                maxSpawnRadius: _droneSpawnConfig.MaxSpawnRadius,
                baseTransform: _baseTransform
            );
        }
    }
}