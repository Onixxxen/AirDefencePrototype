using System;
using System.Collections.Generic;
using MessagePipe;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Presentation.Presenters
{
    public class AirDefencePresenter : IAirDefencePresenter,  IInitializable, IDisposable, IMessageHandler<MissilePathDTO>
    {
        [Inject] private readonly ISubscriber<MissilePathDTO> _missilePathSubscriber;
        
        [Inject] private readonly IAirDefenceView _airDefView;
        
        private DisposableBagBuilder _disposable;

        public void Handle(MissilePathDTO dto) => DisplayShot(dto.Path, dto.Speed);

        public void Initialize()
        {
            _disposable = DisposableBag.CreateBuilder();
            
            _missilePathSubscriber.Subscribe(this).AddTo(_disposable);
        }

        private void DisplayShot(List<Vector3> path, float speed)
        {
            _airDefView.Shot(path,  speed);
        }

        public void Dispose() => _disposable?.Build().Dispose();
    }
}