using PrimeTween;
using System.Collections;
using UnityEngine;

namespace SpeedJam
{
    public class TwinkleCircle : MonoBehaviour
    {
        [SerializeField] private Vector2 _rangeOfStartDelay;
        private SpriteRenderer _renderer;

        private void Awake()
        {
            _renderer = GetComponent<SpriteRenderer>();
        }
        
        private void Start()
        {
            StartCoroutine(StartAnimation());
        }

        private IEnumerator StartAnimation()
        {
            yield return new WaitForSeconds(Random.Range(_rangeOfStartDelay.x, _rangeOfStartDelay.y));
            Tween.Alpha(_renderer, 0.5f, 1, ease: Ease.Linear, cycles: -1, cycleMode: CycleMode.Yoyo);
        }
    }
}