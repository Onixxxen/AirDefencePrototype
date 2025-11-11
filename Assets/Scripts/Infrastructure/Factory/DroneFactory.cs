using System;
using Controller.Drone;
using Domain.Drone;
using Infrastructure.Pool;
using MessagePipe;
using Presentation.Views.Drone;
using UnityEngine;
using VContainer;
using VContainer.Unity;
using Random = UnityEngine.Random;

namespace Infrastructure.Factory
{
    public class DroneFactory : IDroneFactory, IInitializable
    {
        [Inject] private readonly IPublisher<SpawnDroneDTO> _spawnPublisher;
        [Inject] private readonly IObjectResolver _resolver;
        [Inject] private readonly IDroneSpawnConfig _droneSpawnConfig;
        [Inject] private readonly DroneView _dronePrefab;

        private DronePool _pool;

        public void Initialize()
        {
            _pool = new DronePool(_dronePrefab, _droneSpawnConfig.DronesCount, _droneSpawnConfig.IsReplenished);
        }

        public void SpawnDrone(IDroneConfig config, float minSpawnRadius, float maxSpawnRadius, Transform baseTransform)
        {
            var droneView = _pool.Get();
            
            if (!droneView)
                return;
            
            Vector3 spawnPosition = GetRandomSpawnPoint(baseTransform.position, minSpawnRadius, maxSpawnRadius);

            var model = new DroneModel(
                config.Speed, config.Damage, config.Award,
                config.MinHeight, config.MaxHeight,
                config.MaxTurnAngle, config.MaxBankAngle,
                config.BankSpeed, config.TurnSpeed, config.HeightLerpSpeed
            );
            
            _spawnPublisher.Publish(new SpawnDroneDTO(model, droneView, spawnPosition, baseTransform.position));
            
            droneView.transform.position = spawnPosition;
            
            var controller = droneView.GetComponent<DroneController>();
            _resolver.Inject(controller);
            
            droneView.gameObject.SetActive(true);
            
            controller.Setup(model, baseTransform);
            controller.StartFly();

        }
        
        private Vector3 GetRandomSpawnPoint(Vector3 basePos, float minRadius, float maxRadius)
        {
            float angle = Random.Range(0f, Mathf.PI * 2f);
            float radius = Random.Range(minRadius, maxRadius);
    
            Vector3 offset = new Vector3(
                Mathf.Cos(angle) * radius,
                15f,
                Mathf.Sin(angle) * radius
            );
    
            return basePos + offset;
        }
    }
}