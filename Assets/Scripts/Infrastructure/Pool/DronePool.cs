using System.Collections.Generic;
using Presentation.Views.Drone;
using UnityEngine;
using VContainer;

namespace Infrastructure.Pool
{
    public class DronePool
    {
        private readonly DroneView _prefab;
        private readonly List<DroneView> _pool = new();
        private readonly Transform _parent;
        private readonly bool _isReplenished;

        public DronePool(DroneView prefab, int preloadCount, bool isReplenished)
        {
            _prefab = prefab;
            _isReplenished = isReplenished;
            _parent = new GameObject("[DronePool]").transform;

            Preload(preloadCount);
        }

        private void Preload(int count)
        {
            for (int i = 0; i < count; i++)
            {
                var drone = CreateDrone();
                drone.gameObject.SetActive(false);
                _pool.Add(drone);
            }
        }

        public DroneView Get()
        {
            var drone = _pool.Find(d => !d.gameObject.activeInHierarchy);
            if (drone == null && _isReplenished)
            {
                drone = CreateDrone();
                _pool.Add(drone);
            }
            return drone;
        }

        private DroneView CreateDrone()
        {
            var go = Object.Instantiate(_prefab.gameObject, _parent);
            var view = go.GetComponent<DroneView>();
            return view;
        }
    }
}