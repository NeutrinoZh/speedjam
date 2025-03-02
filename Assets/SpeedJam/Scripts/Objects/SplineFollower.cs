using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Splines;

namespace SpeedJam
{
    public class SplineFollower : MonoBehaviour
    {
        [SerializeField] private float _speed;
        [SerializeField] private SplineContainer _splineContainer;
        [SerializeField] private int _splineIndex;
        
        private Spline _spline;

        private int _knotIndex;

        private Vector3 ToVector3(float3 position)
        {
            return new Vector3(position.x, position.y, position.z);
        }
        
        private void Start()
        {
            _spline = _splineContainer.Splines[_splineIndex];
        }

        private void Update()
        {
            Vector3 offset = _splineContainer.transform.position + ToVector3(_spline[_knotIndex].Position) - transform.position;
            Vector3 normal = offset.normalized;
            
            transform.position += Time.deltaTime * _speed * normal;

            if (!(offset.sqrMagnitude < 0.1f))
                return;

            _knotIndex += 1;
            if (_knotIndex >= _spline.Count - 1)
                _knotIndex = 0;
        }
    }
}