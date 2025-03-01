using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace SpeedJam
{
    public class CameraController : MonoBehaviour
    {
        [SerializeField] private Bounds _bounds;
        [SerializeField] private Vector3 _cameraOffset;
        [SerializeField] private float _cameraSpeed;
        
        private Transform _player;
        
        [Inject]
        public void Construct(Player player)
        {
            _player = player.transform;
        }

        private void Update()
        {
            var position = Vector3.Lerp(transform.position, _player.position + _cameraOffset, _cameraSpeed * Time.deltaTime);
            
            position = new Vector3(
                Mathf.Clamp(position.x, _bounds.min.x, _bounds.max.x),
                Mathf.Clamp(position.y, _bounds.min.y,  _bounds.max.y),
                position.z);
            
            transform.position = position;
        }
    }
}