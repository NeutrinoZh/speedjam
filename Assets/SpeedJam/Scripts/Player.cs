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

        [field: SerializeField] public float SpeedOnGround { get; private set; }
        [field: SerializeField] public float JumpImpulse { get; private set; }
        [field: SerializeField] public float ColliderActivationDelay { get; private set; }
        [field: SerializeField] public float JetpackForce { get; private set; }
        [field: SerializeField] public float Friction { get; private set; }
        [field: SerializeField] public float AngularSpeed { get; private set; }
        [field: SerializeField] public float GroundedDelay { get; private set; }
        [field: SerializeField] public float MaxSpeed { get; private set; }
        [field: SerializeField] public float MaxJetpackCharge { get; private set; }
        [field: SerializeField] public float JetpackChargeConsumptionRate { get; private set; } 
        [field: SerializeField] public float JetpackChargeRefuelingRate { get; private set; } 
        
        public float JetpackCharge { get; set; }
        public float SqrMaxSpeed => MaxSpeed * MaxSpeed;
            
        private Controls _controls;
        
        [Inject]
        public void Construct(Controls controls)
        {
            _controls = controls;
        }
        
        private void Awake()
        {
            JetpackCharge = MaxJetpackCharge;
            
            _controls.Enable();
        }

        private void OnDestroy()
        {
            _controls.Disable();
        }
    }
}