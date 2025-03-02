using UnityEngine;

namespace SpeedJam
{
    public class Satellite : MonoBehaviour
    {
        [SerializeField] private Vector2 _rangeOfSpeed;
        [SerializeField] private Bounds _bounds;
        
        private float _speed;
        
        private void Start()
        {
            _speed = Random.Range(_rangeOfSpeed.x, _rangeOfSpeed.y);    
        }

        private void Update()
        {
            Vector3 position = transform.position;
            
            position += Time.deltaTime * _speed * transform.up;
            if (position.x < _bounds.min.x) position.x = _bounds.max.x;
            if (position.x > _bounds.max.x) position.x = _bounds.min.x;
            if (position.y > _bounds.max.y) position.y = _bounds.min.y;
            if (position.y < _bounds.min.y) position.y = _bounds.max.y;
            
            transform.position = position;
        }
    }
}