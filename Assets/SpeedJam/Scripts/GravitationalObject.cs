using UnityEngine;
using UnityEngine.Serialization;

namespace SpeedJam
{
    public class GravitationalObject : MonoBehaviour
    {
        [field: SerializeField] public float ForceModifier { get;private set; }
        [field: SerializeField] public float GravitationalField { get; private set; }
        [field: SerializeField] public float Charge { get; set; }
    }
}