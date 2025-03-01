using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace SpeedJam
{
    public class FuelSlider : MonoBehaviour
    {
        private Slider _slider;
        private Player _player;

        [Inject]
        public void Construct(Player player)
        {
            _player = player;
        }
        
        private void ChangeFuelAmount()
        {
            _slider.value = _player.JetpackCharge / _player.MaxJetpackCharge;
        }

        private void Awake()
        {
            _slider = GetComponent<Slider>();
        }

        private void Start()
        {
            _player.OnChargeChange += ChangeFuelAmount;
        }

        private void OnDestroy()
        {
            _player.OnChargeChange -= ChangeFuelAmount;
        }
    }
}