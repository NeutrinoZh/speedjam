using UnityEngine;
using UnityEngine.Serialization;

namespace SpeedJam
{
    public class GravitationalObject : MonoBehaviour
    {
        private static readonly int k_Blend = Shader.PropertyToID("Blend");
        
        [field: SerializeField] public float ForceModifier { get;private set; }
        [field: SerializeField] public float GravitationalField { get; private set; }
        [field: SerializeField] public float MaxCharge { get; private set; }
        
        private float _charge = 0f;
        
        public float Charge
        {
            get => _charge;
            set
            {
                _spriteRenderer.material.SetFloat(k_Blend, value / MaxCharge);
                _charge = value;
            }
        }

        private SpriteRenderer _spriteRenderer;

        private void Awake()
        {
            _spriteRenderer = GetComponent<SpriteRenderer>();
            
            Charge = MaxCharge;
        }
    }
}