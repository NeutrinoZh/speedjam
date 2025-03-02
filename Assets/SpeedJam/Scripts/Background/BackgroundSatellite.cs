using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

namespace SpeedJam
{
    public class BackgroundSatellite : MonoBehaviour
    {
        [SerializeField] private List<Transform> _satellites;
        [SerializeField] private Bounds _bounds;
        [SerializeField] private int _satelliteCount;

        private void Start()
        {
            for (int i = 0; i < _satelliteCount; ++i)
            {
                Vector3 position = new(
                    Random.Range(-_bounds.extents.x, _bounds.extents.x),
                    Random.Range(-_bounds.extents.y, _bounds.extents.y),
                    0);

                int angle = Random.Range(0, 360);
                
                Transform prefab = _satellites[Random.Range(0, _satellites.Count)];
                Transform satellite = Instantiate(prefab, transform, true);
                satellite.position = position;
                satellite.rotation = Quaternion.Euler(0, 0, angle);
            }
        }
    }
}