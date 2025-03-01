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
        private float _radius;
        private float _angle;
        
        public GravitationalObject ClingObject
        {
            get => _clingObject;
            set
            {
                _clingObject = value;

                var offset = (transform.position - _clingObject.transform.position);
                _angle = Mathf.Atan2(offset.y, offset.x);
                _radius = offset.magnitude;
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

        private void Awake()
        {
            _controls.Enable();
        }

        private void OnDestroy()
        {
            _controls.Disable();
        }

        private void Update()
        {
            if (_player.State != Player.CharacterState.OnGround)
                return;

            var moveDirection = _controls.Player.Move.ReadValue<Vector2>();
            MoveAroundCircle(-moveDirection.x);

            if (moveDirection.y > 0)
            {
                _player.State = Player.CharacterState.OnAir;
                
                var direction = (transform.position - _clingObject.transform.position).normalized;
                _rigidbody.AddForce(direction * _player.JumpImpulse, ForceMode2D.Impulse);
                
                StartCoroutine(ColliderActivation());
            }
        }

        private void MoveAroundCircle(float direction)
        {
            _angle += direction * _player.SpeedOnGround * Time.deltaTime;

            float x = _clingObject.transform.position.x + _radius * Mathf.Cos(_angle);
            float y = _clingObject.transform.position.y + _radius * Mathf.Sin(_angle);

            transform.position = new Vector3(x, y, transform.position.z);
            transform.rotation = Quaternion.Euler(0, 0, _angle * Mathf.Rad2Deg + 90);
        }

        private IEnumerator ColliderActivation()
        {
            _collider.enabled = false;
            yield return new WaitForSeconds(_player.ColliderActivationDelay);
            _collider.enabled = true;
        }
    }
}