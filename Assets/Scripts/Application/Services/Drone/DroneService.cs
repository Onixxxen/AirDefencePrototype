using System;
using System.Collections.Generic;
using Application.Services.Currency;
using MessagePipe;
using UnityEngine;
using VContainer;
using VContainer.Unity;
using Random = UnityEngine.Random;

namespace Application.Services.Drone
{
    public class DroneService : IDroneService, IInitializable, IDisposable, IMessageHandler<DroneStartedFlyDTO>,
        IMessageHandler<DroneSpottedBaseDTO>, IMessageHandler<DroneHitBaseDTO>, IMessageHandler<SpawnDroneDTO>, IMessageHandler<DroneHitDTO>
    {
        [Inject] private readonly ISubscriber<SpawnDroneDTO> _spawnSubscriber;
        [Inject] private readonly ISubscriber<DroneStartedFlyDTO> _droneStartedFlySubscriber;
        [Inject] private readonly ISubscriber<DroneSpottedBaseDTO> _droneSpottedBaseSubscriber;
        [Inject] private readonly ISubscriber<DroneHitBaseDTO> _droneHitBaseSubscriber;
        [Inject] private readonly ISubscriber<DroneHitDTO> _droneHitSubscriber;

        [Inject] private readonly IPublisher<DronePathDTO> _droneGoalPublisher;
        [Inject] private readonly IPublisher<DroneDestroyDTO> _droneDestroyPublisher;
        
        [Inject] private readonly IGoldService _goldService;

        private readonly Dictionary<IDroneModel, IDroneView> _droneViews = new();
        private DisposableBagBuilder _disposable;

        public void Handle(SpawnDroneDTO dto) => AddDrone(dto);

        public void Handle(DroneStartedFlyDTO dto) => PlanFlightRoute(dto.DroneModel, dto.CurrentPosition, dto.BasePosition);
        public void Handle(DroneSpottedBaseDTO dto) => PlanDiveRoute(dto.DroneModel, dto.BasePosition);
        public void Handle(DroneHitBaseDTO dto) => DestroyDrone(dto.DroneModel, false);
        public void Handle(DroneHitDTO dto) => DestroyDrone(dto.DroneModel, true);

        public void Initialize()
        {
            _disposable = DisposableBag.CreateBuilder();

            _spawnSubscriber.Subscribe(this).AddTo(_disposable);
            _droneStartedFlySubscriber.Subscribe(this).AddTo(_disposable);
            _droneSpottedBaseSubscriber.Subscribe(this).AddTo(_disposable);
            _droneHitBaseSubscriber.Subscribe(this).AddTo(_disposable);
            _droneHitSubscriber.Subscribe(this).AddTo(_disposable);
        }

        private void AddDrone(SpawnDroneDTO dto)
        {
            _droneViews.Add(dto.Model, dto.View);
        }

        private void PlanFlightRoute(IDroneModel model, Vector3 currentPos, Vector3 basePos)
        {
            List<Vector3> path = new List<Vector3>();
            int waypointCount = Random.Range(8, 12);
            Vector3 lastPos = currentPos;
            Vector3 prevDirection = (basePos - currentPos).normalized;
            float midHeight = (model.MinHeight + model.MaxHeight) * 0.5f;

            const float minStep = 30f;
            const float maxStep = 50f;
            const float randomRadius = 20f;
            const int maxAttempts = 15;

            for (int i = 0; i < waypointCount; i++)
            {
                Vector3 nextPos = lastPos + prevDirection * minStep;
                bool valid = false;
                int attempts = 0;

                while (!valid && attempts < maxAttempts)
                {
                    float step = Random.Range(minStep, maxStep);
                    Vector3 forward = prevDirection * step;
                    Vector3 toBase = (basePos - lastPos).normalized * 15f;

                    Vector3 rand = new Vector3(
                        Random.Range(-1f, 1f),
                        Random.Range(-0.3f, 0.3f),
                        Random.Range(-1f, 1f)
                    ).normalized * randomRadius;

                    nextPos = lastPos + forward + toBase + rand;

                    nextPos.y = midHeight + Random.Range(-0.5f, 0.5f);
                    nextPos.y = Mathf.Clamp(nextPos.y, model.MinHeight, model.MaxHeight);

                    Vector3 newDir = (nextPos - lastPos).normalized;
                    float angle = Vector3.Angle(prevDirection, newDir);

                    if (angle <= model.MaxTurnAngle)
                        valid = true;
                    else
                        attempts++;
                }

                path.Add(nextPos);
                lastPos = nextPos;
                prevDirection = (nextPos - lastPos).normalized;
            }

            PublishPath(model, path, 1f, false);
        }

        private void PlanDiveRoute(IDroneModel model, Vector3 basePos)
        {
            List<Vector3> path = new List<Vector3> { basePos };
            PublishPath(model, path, 4f, true);
        }

        private void DestroyDrone(IDroneModel model, bool isShotDown)
        {
            if (_droneViews.TryGetValue(model, out var view))
            {
                if (isShotDown)
                    _goldService.AddValue(model.Award);
                
                _droneDestroyPublisher.Publish(new DroneDestroyDTO(view, isShotDown));
                _droneViews.Remove(model);
            }
        }

        private void PublishPath(IDroneModel model, List<Vector3> path, float speed, bool isDive)
        {
            if (_droneViews.TryGetValue(model, out var view))
                _droneGoalPublisher.Publish(new DronePathDTO(model, path, speed, isDive, view));
        }

        public void Dispose() => _disposable?.Build().Dispose();
    }
}