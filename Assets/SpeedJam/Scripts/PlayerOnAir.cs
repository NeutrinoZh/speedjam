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
                _rigidbody.AddForce(-transform.up * _player.JetpackForce, ForceMode2D.Force);
            else 
                _rigidbody.AddForce(-_rigidbody.velocity.normalized * _player.Friction, ForceMode2D.Force);
            
            var euler = transform.eulerAngles;
            var angularVelocity = -moveDirection.x * _player.AngularSpeed * Time.deltaTime;
            transform.rotation = Quaternion.Euler(euler.x, euler.y, euler.z + angularVelocity);
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
        }
    }
}