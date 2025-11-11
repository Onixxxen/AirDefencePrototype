using System.Collections;
using System.Collections.Generic;
using Controller.Drone;
using UnityEngine;

namespace Presentation.Views.AirDefence
{
    public class MissileView : MonoBehaviour
    {
        private Vector3 _finalDirection;
        private Coroutine _flightCoroutine;

        public void FlyAlongPath(List<Vector3> path, float speed)
        {
            if (_flightCoroutine != null)
                StopCoroutine(_flightCoroutine);
            
            _flightCoroutine = StartCoroutine(FlyCoroutine(path, speed));
        }

        private IEnumerator FlyCoroutine(List<Vector3> path, float speed)
        {
            for (int i = 0; i < path.Count - 1; i++)
            {
                Vector3 start = path[i];
                Vector3 end = path[i + 1];
                float segmentDistance = Vector3.Distance(start, end);
                float segmentTime = segmentDistance / speed;

                float elapsed = 0f;
                while (elapsed < segmentTime)
                {
                    elapsed += Time.deltaTime;
                    float t = elapsed / segmentTime;

                    transform.position = Vector3.Lerp(start, end, t);

                    if (i < path.Count - 1)
                    {
                        Vector3 lookDir = (end - start).normalized;
                        if (lookDir != Vector3.zero)
                            transform.rotation = Quaternion.LookRotation(lookDir);
                    }

                    yield return null;
                }
            }
            
            _finalDirection = (path[^1] - path[^2]).normalized;

            while (true)
            {
                transform.position += _finalDirection * speed * Time.deltaTime;
                transform.rotation = Quaternion.LookRotation(_finalDirection);

                if (IsOutOfBounds())
                {
                    Destroy(gameObject);
                    yield break;
                }

                yield return null;
            }
        }
        
        private bool IsOutOfBounds()
        {
            float mapSize = 200f;

            return Mathf.Abs(transform.position.x) > mapSize ||
                   Mathf.Abs(transform.position.z) > mapSize ||
                   transform.position.y < -10f || transform.position.y > 100f;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out DroneController drone))
            {
                drone.OnHitDrone();
                Destroy(gameObject);
            }
        }
    }
}