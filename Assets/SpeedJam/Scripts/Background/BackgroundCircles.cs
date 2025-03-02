using System.Collections.Generic;
using UnityEngine;
using Quaternion = UnityEngine.Quaternion;
using Vector3 = UnityEngine.Vector3;

namespace SpeedJam
{
    public class BackgroundCircles : MonoBehaviour
    {
        [SerializeField] private List<Color> _colors;
        [SerializeField] private Vector2 _rangeOfRadius;
        [SerializeField] private SpriteRenderer _circlePrefab;
        [SerializeField] private Bounds _bounds;
        [SerializeField] private int _count;

        private void Start()
        {
            for (int i = 0; i < _count; ++i)
            {
                var position = new Vector3(
                    Random.Range(-_bounds.extents.x, _bounds.extents.x),
                    Random.Range(-_bounds.extents.y, _bounds.extents.y),
                    0);

                var circle = Instantiate(_circlePrefab, transform, true);
                circle.transform.position = position;
                circle.color = _colors[Random.Range(0, _colors.Count)];
                
                var radius = Random.Range(_rangeOfRadius.x, _rangeOfRadius.y);
                circle.transform.localScale = new Vector3(radius, radius, 1);
            }
        }
    }
}