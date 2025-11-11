using System;
using System.Collections;
using System.Collections.Generic;
using Controller.Drone;
using UnityEngine;

namespace Presentation.Views.Drone
{
    public class DroneView : MonoBehaviour, IDroneView
    {
        [SerializeField] private GameObject _hitExplosion;
        [SerializeField] private GameObject _hitBaseExplosion;
        
        private Coroutine _flightCoroutine;

        public void FlyAlongPath(IDroneModel model, List<Vector3> path, float speedMultiplier, bool isDive)
        {
            if (_flightCoroutine != null)
                StopCoroutine(_flightCoroutine);

            _flightCoroutine = isDive
                ? StartCoroutine(FlyDiveCoroutine(model, path, speedMultiplier))
                : StartCoroutine(FlyWanderCoroutine(model, path));
        }

        public void DestroyDrone(bool isShotDown)
        {
            GameObject explosion = isShotDown ? _hitExplosion : _hitBaseExplosion;
            Instantiate(explosion, transform.position, Quaternion.identity);
            
            gameObject.SetActive(false);
        }

        private IEnumerator FlyWanderCoroutine(IDroneModel model, List<Vector3> path)
        {
            float speed = model.Speed;
            int index = 0;
            float midHeight = (model.MinHeight + model.MaxHeight) * 0.5f;

            while (index < path.Count)
            {
                Vector3 target = path[index];
        
                if (Vector3.Distance(transform.position, target) < 8f)
                {
                    index++;
                    continue;
                }

                Vector3 horizontalTarget = new Vector3(target.x, transform.position.y, target.z);
                Vector3 directionToTarget = (horizontalTarget - transform.position).normalized;
                RotateTowards(directionToTarget, model);

                ApplyBank(model, directionToTarget);

                Vector3 movement = CalculateMovement(model, speed, midHeight);
                transform.position += movement;

                yield return null;
            }

            GetComponent<DroneController>().OnPathCompleted();
        }

        private IEnumerator FlyDiveCoroutine(IDroneModel model, List<Vector3> path, float speedMultiplier)
        {
            Vector3 target = path[0];
            float baseSpeed = model.Speed;
            float maxSpeed = baseSpeed * speedMultiplier;

            float startDistance = Vector3.Distance(transform.position, target);

            while (true)
            {
                Vector3 toTarget = target - transform.position;
                float distance = toTarget.magnitude;

                Vector3 direction = toTarget.normalized;

                Quaternion targetRot = Quaternion.LookRotation(direction);
                transform.rotation = Quaternion.Slerp(transform.rotation, targetRot, model.TurnSpeed * Time.deltaTime);

                float progress = 1f - (distance / startDistance);
                float speedFactor = Mathf.SmoothStep(0f, 1f, progress);
                float currentSpeed = Mathf.Lerp(baseSpeed, maxSpeed, speedFactor);

                transform.position += direction * currentSpeed * Time.deltaTime;

                yield return null;
            }
        }

        private void RotateTowards(Vector3 direction, IDroneModel model)
        {
            Quaternion targetRot = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRot, model.TurnSpeed * Time.deltaTime);
        }

        private void ApplyBank(IDroneModel model, Vector3 directionToTarget)
        {
            Vector3 flatForward = Vector3.ProjectOnPlane(transform.forward, Vector3.up).normalized;
            float yawAngle = Vector3.SignedAngle(flatForward, directionToTarget, Vector3.up);
            float targetBank = Mathf.Clamp(yawAngle * 0.8f, -model.MaxBankAngle, model.MaxBankAngle);
            float currentBank = Mathf.Lerp(0f, targetBank, model.BankSpeed * Time.deltaTime);
            Quaternion bankRot = Quaternion.AngleAxis(currentBank, transform.right);
            transform.rotation = bankRot * transform.rotation;
        }

        private Vector3 CalculateMovement(IDroneModel model, float baseSpeed, float midHeight)
        {
            Vector3 move = transform.forward * baseSpeed * Time.deltaTime;
            float targetY = Mathf.Lerp(transform.position.y, midHeight, model.HeightLerpSpeed * Time.deltaTime);
            targetY = Mathf.Clamp(targetY, model.MinHeight, model.MaxHeight);
            move.y = targetY - transform.position.y;
            return move;
        }
    }
}