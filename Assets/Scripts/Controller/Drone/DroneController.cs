using System;
using MessagePipe;
using UnityEngine;
using VContainer;

namespace Controller.Drone
{
    public class DroneController : MonoBehaviour
    {
        [Inject] private readonly IPublisher<DroneStartedFlyDTO> _droneStartedFlyPublisher;
        [Inject] private readonly IPublisher<DroneSpottedBaseDTO> _droneSpottedBasePublisher;
        [Inject] private readonly IPublisher<DroneHitBaseDTO> _droneHitBasePublisher;
        [Inject] private readonly IPublisher<DroneHitDTO> _droneHitPublisher;

        private IDroneModel _droneModel;
        private Transform _base;
        private Vector3 _lastPosition;
        private Vector3 _velocity;

        public Vector3 GetPosition() => transform.position;
        public Vector3 GetVelocity() => _velocity;
        
        private void Update()
        {
            if (Time.deltaTime > 0)
                _velocity = (transform.position - _lastPosition) / Time.deltaTime;
            _lastPosition = transform.position;
        }

        public void Setup(IDroneModel model, Transform baseTransform)
        {
            _droneModel = model;
            _base = baseTransform;
        }

        public void StartFly()
        {
            _droneStartedFlyPublisher.Publish(
                new DroneStartedFlyDTO(_droneModel, transform.position, _base.position));
        }

        public void OnPathCompleted()
        {
            if (_base) 
                _droneStartedFlyPublisher.Publish(new DroneStartedFlyDTO(_droneModel, transform.position, _base.position));
        }

        public void OnHitDrone()
        {
            _droneHitPublisher.Publish(new DroneHitDTO(_droneModel));
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.GetComponent<BaseSpottedZone>()) 
                _droneSpottedBasePublisher.Publish(new DroneSpottedBaseDTO(_droneModel, _base.position));
            
            if (other.GetComponent<BaseHitZone>()) 
                _droneHitBasePublisher.Publish(new DroneHitBaseDTO(_droneModel));
        }
    }
}