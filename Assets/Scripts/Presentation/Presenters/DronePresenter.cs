using System;
using MessagePipe;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Presentation.Presenters
{
    public class DronePresenter : IDronePresenter, IInitializable, IDisposable, IMessageHandler<DronePathDTO>, IMessageHandler<DroneDestroyDTO>
    {
        [Inject] private readonly ISubscriber<DronePathDTO> _dronePathSubscriber;
        [Inject] private readonly ISubscriber<DroneDestroyDTO> _droneDestroySubscriber;
        
        private DisposableBagBuilder _disposable;

        public void Handle(DronePathDTO dto) => DisplayFlyDrone(dto);
        public void Handle(DroneDestroyDTO dto) => DisplayDestroyDrone(dto);

        public void Initialize()
        {
            _disposable = DisposableBag.CreateBuilder();
            
            _dronePathSubscriber.Subscribe(this).AddTo(_disposable);
            _droneDestroySubscriber.Subscribe(this).AddTo(_disposable);
        }

        private void DisplayFlyDrone(DronePathDTO dto)
        {
            dto.DroneView.FlyAlongPath(dto.DroneModel, dto.Path, dto.SpeedMultiplier, dto.IsDive);
        }

        private void DisplayDestroyDrone(DroneDestroyDTO dto)
        {
            dto.DroneView.DestroyDrone(dto.IsShotDown);
        }

        public void Dispose() => _disposable?.Build().Dispose();
    }
}