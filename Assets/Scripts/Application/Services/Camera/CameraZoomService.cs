using System;
using MessagePipe;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Application.Services.Camera
{
    public class CameraZoomService : ICameraZoomService, IInitializable, IDisposable, IMessageHandler<CameraZoomDTO>
    {
        [Inject] private readonly ISubscriber<CameraZoomDTO> _zoomSubscriber;
        [Inject] private readonly IPublisher<CameraZoomResultDTO> _zoomPublisher;
        [Inject] private readonly ICameraConfig _cameraConfig;
        
        private DisposableBagBuilder _disposable;
        
        private float _zoomSpeed;
        private float _minZoom;
        private float _maxZoom;

        public void Handle(CameraZoomDTO dto) => SetZoom(dto.OrthographicSize,  dto.ZoomDelta);

        public void Initialize()
        {
            _disposable = DisposableBag.CreateBuilder();
            
            _zoomSubscriber.Subscribe(this).AddTo(_disposable);
            
            _zoomSpeed = _cameraConfig.CameraZoomSpeed;
            _minZoom = _cameraConfig.ZoomBorder.x;
            _maxZoom = _cameraConfig.ZoomBorder.y;
        }

        public void SetZoom(float orthographicSize, float zoomDelta)
        {
            if (!IsMovement(zoomDelta))
                return;

            float newSize = orthographicSize - zoomDelta * _zoomSpeed;
            newSize = Mathf.Clamp(newSize, _minZoom, _maxZoom);
            
            _zoomPublisher.Publish(new CameraZoomResultDTO(newSize));
        }

        private bool IsMovement(float zoomDelta)
        {
            return zoomDelta != 0;
        }
        
        public void Dispose() => _disposable?.Build().Dispose();
        
    }
}