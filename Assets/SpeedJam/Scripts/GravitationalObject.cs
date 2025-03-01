using UnityEngine;
using UnityEngine.Serialization;

namespace SpeedJam
{
    public class GravitationalObject : MonoBehaviour
    {
        public float ForceModifier => _forceModifier;
        public float GravitationalField => _gravitationalField;
        
        [SerializeField] private float _forceModifier;
        [SerializeField] private float _gravitationalField;
    }
}