using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.Serialization;

namespace SpeedJam
{
    public class GravitationalObject : MonoBehaviour
    {
        private static readonly int k_Blend = Shader.PropertyToID("_Blend");

        [field: SerializeField] public float ForceModifier { get; private set; }
        [field: SerializeField] public float GravitationalField { get; private set; }
        [field: SerializeField] public float MaxCharge { get; private set; }
        [field: SerializeField] public bool IsTrap { get; private set; }

        private float _charge = 0f;

        public float Charge
        {
            get => _charge;
            set
            {
                _spriteRenderer.material.SetFloat(k_Blend, 1 - value / MaxCharge);
                
                if (_light)
                    _light.intensity = value / MaxCharge;
                
                _charge = value;
            }
        }

        private SpriteRenderer _spriteRenderer;
        private Light2D _light;

        private void Awake()
        {
            _spriteRenderer = GetComponent<SpriteRenderer>();
            _light = GetComponent<Light2D>();

            Charge = MaxCharge;
        }
    }
}