using UnityEngine;

namespace SpeedJam
{
    public class Player : MonoBehaviour
    {
        public enum CharacterState
        {
            OnGround, 
            OnAir,
        }
        
        public CharacterState State { get; set; } = CharacterState.OnAir;

        [field: SerializeField] public float SpeedOnGround { get; set; }
        [field: SerializeField] public float JumpImpulse { get; set; }
        [field: SerializeField] public float ColliderActivationDelay { get; set; }
        [field: SerializeField] public float JetpackForce { get; set; }
    }
}