using PrimeTween;
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using Zenject;

namespace SpeedJam
{
    public class PlayerOnGround : MonoBehaviour
    {
        private Player _player;
        private Rigidbody2D _rigidbody;
        private CapsuleCollider2D _collider;
        private Controls _controls;

        private GravitationalObject _clingObject;
        private CircleCollider2D _clingCollider;
        private float _radius;
        private float _angle;

        private Vector2 _direction = Vector2.zero; 
        private bool _isOnlyGrounded = false;
        public Vector2 Direction => _direction;
        
        public GravitationalObject ClingObject
        {
            get => _clingObject;
            set
            {
                _clingObject = value;

                var offset = (transform.position - _clingObject.transform.position);
                _angle = Mathf.Atan2(offset.y, offset.x);
                
                _clingCollider = _clingObject.GetComponent<CircleCollider2D>();
                _radius = _clingCollider.radius * _clingObject.transform.lossyScale.x * 1.3f;
                
                // Tween.LocalRotation(transform, Quaternion.Euler(0f, 0f, _angle * Mathf.Rad2Deg - 90), _player.RotationAnimationDuration);
                
                _isOnlyGrounded = true;
                StartCoroutine(GroundedDelay());
            }
        }
        
        [Inject]
        public void Construct(Player player, Controls controls)
        {
            _player = player;
            _controls = controls;
            _rigidbody = GetComponent<Rigidbody2D>();
            _collider = GetComponent<CapsuleCollider2D>();
        }
        
        private void Update()
        {
            if (_player.State != Player.CharacterState.OnGround)
                return;

            _direction = _controls.Player.Move.ReadValue<Vector2>();
            
            MoveAroundCircle(-_direction.x);
            if (_direction.y > 0 && !_isOnlyGrounded)
                Jump();
        }

        private void Jump()
        {
            _player.State = Player.CharacterState.OnAir;
                
            var direction = (transform.position - _clingObject.transform.position).normalized;
            _rigidbody.AddForce(direction * _player.JumpImpulse, ForceMode2D.Impulse);
                
            StartCoroutine(ColliderActivation());
        }

        private void MoveAroundCircle(float direction)
        {
            _angle += direction * _player.SpeedOnGround * Time.deltaTime;

            float x = _clingObject.transform.position.x + 
                      _clingCollider.offset.x +
                      _radius * Mathf.Cos(_angle);
            
            float y = _clingObject.transform.position.y + +
                      _clingCollider.offset.y +
                      _radius * Mathf.Sin(_angle);

            transform.position = new Vector3(x, y, transform.position.z);
            transform.rotation = Quaternion.Lerp(transform.rotation,
                Quaternion.Euler(0, 0, _angle * Mathf.Rad2Deg - 90), 0.1f);
        }

        private IEnumerator ColliderActivation()
        {
            _collider.enabled = false;
            yield return new WaitForSeconds(_player.ColliderActivationDelay);
            _collider.enabled = true;
        }

        private IEnumerator GroundedDelay()
        {
            yield return new WaitForSeconds(_player.GroundedDelay);
            _isOnlyGrounded = false;
        }
    }
}