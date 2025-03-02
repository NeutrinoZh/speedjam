using PrimeTween;
using TMPro;
using UnityEngine;
using Zenject;

namespace SpeedJam
{
    public class AdviceLabel : MonoBehaviour
    {
        private TextMeshProUGUI _adviceLabel;
        private Player _player;

        [Inject]
        public void Construct(Player player)
        {
            _player = player;
        }

        private void Awake()
        {
            _adviceLabel = GetComponentInChildren<TextMeshProUGUI>();
        }

        private void Start()
        {
            Tween.Alpha(_adviceLabel, 0.4f, 1, ease: Ease.Linear, cycles: -1, cycleMode: CycleMode.Yoyo);
        }

        private void OnEnable()
        {
            transform.rotation = Quaternion.Euler(0, 0, 0);
            transform.position = _player.transform.position;
        }
    }
}