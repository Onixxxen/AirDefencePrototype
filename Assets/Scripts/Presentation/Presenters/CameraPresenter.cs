using System;
using MessagePipe;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Presentation.Presenters
{
    public class CameraPresenter : ICameraPresenter, IInitializable, IDisposable, IMessageHandler<CameraMoveResultDTO>, IMessageHandler<CameraZoomResultDTO>, IMessageHandler<CameraRotateResultDTO>
    {
        [Inject] private readonly ISubscriber<CameraMoveResultDTO> _cameraMoveResultSubscriber;
        [Inject] private readonly ISubscriber<CameraZoomResultDTO> _cameraZoomResultSubscriber;
        [Inject] private readonly ISubscriber<CameraRotateResultDTO> _cameraRotateResultSubscriber;
        
        [Inject] private readonly ICameraMoveView _cameraMoveView;
        [Inject] private readonly ICameraZoomView _cameraZoomView;
        [Inject] private readonly ICameraRotateView _cameraRotateView;
        
        private DisposableBagBuilder _disposable;

        public void Handle(CameraMoveResultDTO dto) => SetPosition(dto.Position);
        public void Handle(CameraZoomResultDTO dto) => SetZoom(dto.Zoom);
        public void Handle(CameraRotateResultDTO dto) => Rotate(dto.DeltaX, dto.RotateSpeed);

        public void Initialize()
        {
            _disposable = DisposableBag.CreateBuilder();
            
            _cameraMoveResultSubscriber.Subscribe(this).AddTo(_disposable);
            _cameraZoomResultSubscriber.Subscribe(this).AddTo(_disposable);
            _cameraRotateResultSubscriber.Subscribe(this).AddTo(_disposable);
        }

        private void SetPosition(Vector3 position)
        {
            _cameraMoveView.SetNewPosition(position);
        }

        private void SetZoom(float zoom)
        {
            _cameraZoomView.SetNewZoom(zoom);
        }

        private void Rotate(float deltaX, float rotateSpeed)
        {
            _cameraRotateView.RotateCamera(deltaX,  rotateSpeed);
        }
        
        public void Dispose() => _disposable?.Build().Dispose();
    }
}