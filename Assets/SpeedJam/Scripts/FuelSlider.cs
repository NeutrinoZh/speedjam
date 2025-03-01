using UnityEngine;
using UnityEngine.UI;

namespace SpeedJam
{
    public class FuelSlider : MonoBehaviour
    {
        [SerializeField] private int _maxFuelAmount;

        private Slider _slider;

        public void ChangeFuelAmount(int value)
        {
            _slider.value = value;
        }

        private void Awake()
        {
            _slider = GetComponent<Slider>();
            _slider.maxValue = _maxFuelAmount;
            _slider.value = _maxFuelAmount;
        }
    }
}