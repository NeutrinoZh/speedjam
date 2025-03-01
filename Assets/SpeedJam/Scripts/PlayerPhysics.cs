using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using Zenject;

namespace SpeedJam
{
    public class PlayerPhysics : MonoBehaviour
    {
        [SerializeField] private float _forceModifier;
        
        private ListOfGravitationalObject _listOfObjects;
        private Rigidbody2D _rigidbody;

        [Inject]
        public void Construct(ListOfGravitationalObject listOfObjects)
        {
            _listOfObjects = listOfObjects;
            _rigidbody = GetComponent<Rigidbody2D>();
        }

        private void FixedUpdate()
        {
            foreach (GravitationalObject obj in _listOfObjects.Objects)
            {
                var diff = obj.transform.position - transform.position;
                var normal = diff.normalized;

                var distanceSquared = Vector3.Dot(diff, diff);
                var force = 1 / distanceSquared * normal;
                force *= _forceModifier * obj.ForceModifier;

                _rigidbody.AddForce(force, ForceMode2D.Force);
            }

            float angle = Mathf.Atan2(_rigidbody.velocity.y, _rigidbody.velocity.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0, 0, angle + 90);
        }
    }
}