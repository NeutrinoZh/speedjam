using System;
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
        
        public Action OnChargeChange = null;
        public Action OnChangeState = null;
        
        public Action OnCollectStar = null;
        public Action OnLand = null;
        public Action OnJump = null;
        public Action OnDeath = null;
        public Action OutOfFuel = null;

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
        
        private CharacterState _state = CharacterState.OnAir;
        public CharacterState State
        {
            get => _state;
            set
            {
                if (_state != value && value == CharacterState.OnGround)
                    OnLand?.Invoke();
                
                if (_state != value && value == CharacterState.OnAir)
                    OnJump?.Invoke();
                    
                _state = value;
                OnChangeState?.Invoke();
            }
        }
        
        private float _jetpackCharge;
        public float JetpackCharge
        {
            get => _jetpackCharge;
            set
            {
                _jetpackCharge = value;
                OnChargeChange?.Invoke();
            }
        }
        

        public float SqrMaxSpeed => MaxSpeed * MaxSpeed;
            
        private Controls _controls;
        
        [Inject]
        public void Construct(Controls controls)
        {
            _controls = controls;
        }
        
        private void Awake()
        {
            JetpackCharge = MaxJetpackCharge / 4;
            
            _controls.Enable();
        }

        private void OnDestroy()
        {
            _controls.Disable();
        }
    }
}