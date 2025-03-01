using UnityEngine;
using Zenject;

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
        [field: SerializeField] public float Friction { get; set; }
        [field: SerializeField] public float AngularSpeed { get; set; }
        [field: SerializeField] public float GroundedDelay { get; set; }

        private Controls _controls;
        
        [Inject]
        public void Construct(Controls controls)
        {
            _controls = controls;
        }
        
        private void Awake()
        {
            _controls.Enable();
        }

        private void OnDestroy()
        {
            _controls.Disable();
        }
    }
}