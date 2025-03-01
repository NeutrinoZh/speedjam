using UnityEngine;

namespace SpeedJam
{
    public class GravitationalObject : MonoBehaviour
    {
        public float ForceModifier => _forceModifier;
        
        [SerializeField] private float _forceModifier;
    }
}