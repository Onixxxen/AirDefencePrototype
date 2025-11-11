using System;
using MessagePipe;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Application.Services.Camera
{
    public class CameraMoveService : ICameraMoveService, IInitializable, IDisposable, IMessageHandler<CameraMoveDTO>
    {
        [Inject] private readonly IPublisher<CameraMoveResultDTO> _movePublisher;
        [Inject] private readonly ISubscriber<CameraMoveDTO> _cameraMoveSubscriber;

        [Inject] private readonly ICameraConfig _cameraConfig;

        private DisposableBagBuilder _disposable;
        
        private float _moveSpeed;

        public void Handle(CameraMoveDTO dto) => SetPosition(dto.Position, dto.CameraForward, dto.CameraRight, dto.InputDirection);

        public void Initialize()
        {
            _disposable = DisposableBag.CreateBuilder();
            _moveSpeed = _cameraConfig.CameraMoveSpeed;
            
            _cameraMoveSubscriber.Subscribe(this).AddTo(_disposable);
        }

        public void SetPosition(Vector3 position, Vector3 cameraForward, Vector3 cameraRight, Vector2 inputDirection)
        {
            if (!IsMovement(inputDirection))
                return;

            cameraForward.y = 0f;
            cameraRight.y = 0f;
            cameraForward.Normalize();
            cameraRight.Normalize();

            Vector3 movement = (cameraForward * inputDirection.y + cameraRight * inputDirection.x) * _moveSpeed * Time.deltaTime;
            Vector3 newPosition = position + movement;

            _movePublisher.Publish(new CameraMoveResultDTO(newPosition));
        }

        private bool IsMovement(Vector2 inputDirection) => inputDirection != Vector2.zero;

        public void Dispose() => _disposable?.Build().Dispose();
    }
}