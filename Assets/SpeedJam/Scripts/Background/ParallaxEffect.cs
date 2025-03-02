using UnityEngine;
using UnityEngine.Serialization;
using Zenject;

namespace SpeedJam
{
    public class ParallaxEffect: MonoBehaviour {
        [SerializeField] private float _parallaxFactor;
        private Transform _target;
        private Vector3 _lastTargetPosition;

        [Inject]
        public void Construct(CameraController cameraController)
        {
            _target = cameraController.transform;
        }
        
        private void Awake() {
            _lastTargetPosition = _target.position;
        }

        private void Update() {
            Vector3 deltaMovement = _target.position - _lastTargetPosition;
            transform.position += new Vector3(deltaMovement.x * _parallaxFactor, deltaMovement.y * _parallaxFactor, 0);
            _lastTargetPosition = _target.position;
        }
    }
}