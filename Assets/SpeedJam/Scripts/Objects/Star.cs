using PrimeTween;
using System.Collections;
using UnityEngine;
using UnityEngine.Rendering.Universal;

namespace SpeedJam
{
    public class Star : MonoBehaviour
    {
        [SerializeField] private Vector2 _rangeOfStartDelay;
        [SerializeField] private Transform _collectStarParticle;
        [SerializeField] private Transform _particleParent;

        private Light2D _light;

        private void Awake()
        {
            _light = GetComponent<Light2D>();
        }

        
        private void Start()
        {
            StartCoroutine(StartAnimation());
        }

        private IEnumerator StartAnimation()
        {
            yield return new WaitForSeconds(Random.Range(_rangeOfStartDelay.x, _rangeOfStartDelay.y));
            
            Tween.Custom(
                _light.intensity, 0.3f,
                1f, 
                value => _light.intensity = value, 
                cycles: -1, cycleMode: CycleMode.Yoyo 
                );
        }

        public void SpawnParticle()
        {
            Transform particle = Instantiate(_collectStarParticle, _particleParent, true);
            particle.position = transform.position;
        }
    }
}
