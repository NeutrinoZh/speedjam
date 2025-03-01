using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using Zenject;

namespace SpeedJam
{
    public class PlayerOnAir : MonoBehaviour
    {
        [SerializeField] private float _forceModifier;
        
        private Player _player;
        private ListOfGravitationalObject _listOfObjects;
        private Rigidbody2D _rigidbody;
        private Controls _controls;

        [Inject]
        public void Construct(ListOfGravitationalObject listOfObjects, Player player, Controls controls)
        {
            _listOfObjects = listOfObjects;
            _rigidbody = GetComponent<Rigidbody2D>();
            _player = player;
            _controls = controls;   
        }

        private void FixedUpdate()
        {
            if (_player.State != Player.CharacterState.OnAir)
                return;
            
            ApplyForces();
            
            var moveDirection = _controls.Player.Move.ReadValue<Vector2>();
            if (moveDirection.y > 0)
            {
                _rigidbody.AddForce(transform.forward * _player.JetpackForce, ForceMode2D.Force);
                Debug.Log(moveDirection.y);
            }
            
            float angle = Mathf.Atan2(_rigidbody.velocity.y, _rigidbody.velocity.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0, 0, angle + 90);
        }

        private void ApplyForces()
        {
            foreach (GravitationalObject obj in _listOfObjects.Objects)
            {
                var diff = obj.transform.position - transform.position;
                var distance = diff.magnitude;
                
                if (distance > obj.GravitationalField)
                    continue;
                
                var normal = diff.normalized;

                var force = 1 / distance * normal;
                force *= _forceModifier * obj.ForceModifier;

                _rigidbody.AddForce(force, ForceMode2D.Force);
            }   
            
            _rigidbody.AddForce(-_rigidbody.velocity / 2f, ForceMode2D.Force);
        }
    }
}