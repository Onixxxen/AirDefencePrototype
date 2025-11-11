using Controller.Drone;
using MessagePipe;
using UnityEngine;
using VContainer;

namespace Controller.AirDefence
{
    public class AirDefenceController : MonoBehaviour
    {
        [Inject] private readonly IPublisher<DroneEnterADZoneDTO> _droneEnterADZonePublisher;
        
        private void OnTriggerStay(Collider other)
        {
            if (other.TryGetComponent<DroneController>(out var droneController)) 
                _droneEnterADZonePublisher.Publish(new DroneEnterADZoneDTO(droneController.GetPosition(), droneController.GetVelocity(), transform.position));
        }
    } }