using System;
using System.Collections.Generic;
using Domain.AirDefence;
using MessagePipe;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Application.Services.AirDefence
{
    public class AirDefenceService : IAirDefenceService, IInitializable, IDisposable, IMessageHandler<DroneEnterADZoneDTO>
    {
        [Inject] private readonly ISubscriber<DroneEnterADZoneDTO> _droneEnterADZoneSubscriber;
        [Inject] private readonly IPublisher<MissilePathDTO> _missilePathPublisher;
        
        [Inject] private readonly IAirDefenceConfig _airDefConfig;
        
        private DisposableBagBuilder _disposable;
        private IAirDefenceModel _airDefModel;
        private float _lastShotTime;
        
        public void Handle(DroneEnterADZoneDTO dto) => CalculateMissilePath(dto);

        public void Initialize()
        {
            _disposable = DisposableBag.CreateBuilder();
            
            _airDefModel = new AirDefenceModel(_airDefConfig.MissileSpeed,  _airDefConfig.ReloadSpeed);
            _lastShotTime = -_airDefModel.ReloadSpeed;
            
            _droneEnterADZoneSubscriber.Subscribe(this).AddTo(_disposable);
        }

        private void CalculateMissilePath(DroneEnterADZoneDTO dto)
        {
            if (!CanShoot()) return;

            Vector3 targetPos = PredictDronePosition(dto.DronePosition, dto.DroneVelocity, dto.ADPosition);

            List<Vector3> path = GenerateMissilePath(dto.ADPosition, targetPos);
            _missilePathPublisher.Publish(new MissilePathDTO(path, _airDefModel.MissileSpeed));

            _lastShotTime = Time.time;
        }
        
        private Vector3 PredictDronePosition(Vector3 dronePosition, Vector3 droneVelocity, Vector3 adPosition)
        {
            float distance = Vector3.Distance(adPosition, dronePosition);
            float timeToHit = distance / _airDefModel.MissileSpeed;
            Vector3 droneDirection = droneVelocity.normalized;
    
            float leadDistance = timeToHit * droneVelocity.magnitude;

            return dronePosition + droneDirection * leadDistance;
        }

        private List<Vector3> GenerateMissilePath(Vector3 start, Vector3 target)
        {
            List<Vector3> path = new List<Vector3>();
            int points = 20;

            float targetHeight = target.y;

            Vector3 p0 = start + Vector3.up * 5f;

            Vector3 p3 = new Vector3(target.x, targetHeight, target.z);

            Vector3 direction = (p3 - p0).normalized;
            float distance = Vector3.Distance(p0, p3);

            float archHeight = Mathf.Min(distance * 0.15f, 20f);

            Vector3 p1 = p0 + direction * (distance * 0.3f) + Vector3.up * archHeight;
            Vector3 p2 = p0 + direction * (distance * 0.7f) + Vector3.up * archHeight * 0.5f;

            for (int i = 0; i <= points; i++)
            {
                float t = i / (float)points;
                Vector3 point = CubicBezier(p0, p1, p2, p3, t);
                path.Add(point);
            }

            return path;
        }

        private Vector3 CubicBezier(Vector3 p0, Vector3 p1, Vector3 p2, Vector3 p3, float t)
        {
            float u = 1f - t;
            float tt = t * t;
            float uu = u * u;
            float uuu = uu * u;
            float ttt = tt * t;

            return (uuu * p0) + (3 * uu * t * p1) + (3 * u * tt * p2) + (ttt * p3);
        }

        private bool CanShoot() => Time.time >= _lastShotTime + _airDefModel.ReloadSpeed;

        public void Dispose() => _disposable?.Build().Dispose();
    }
}