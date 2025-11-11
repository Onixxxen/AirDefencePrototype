using System.Collections.Generic;
using UnityEngine;

namespace Presentation.Views.AirDefence
{
    public class AirDefenceView : MonoBehaviour, IAirDefenceView
    {
        [SerializeField] private MissileView _missilePrefab;

        public void Shot(List<Vector3> path, float speed)
        {
            var missile = Instantiate(_missilePrefab, transform.position, Quaternion.identity);
            missile.FlyAlongPath(path, speed);
        }
    }
}