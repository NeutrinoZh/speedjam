using UnityEngine;

namespace SpeedJam
{
    public class PlayerDie : MonoBehaviour
    {
        [SerializeField] private Vector2 _rangeOfImpulse;
        
        private void Start()
        {
            foreach (Transform child in transform)
                if (child.TryGetComponent(out Rigidbody2D rigidbody))
                {
                    var velocity = new Vector2(
                        Random.Range(_rangeOfImpulse.x, _rangeOfImpulse.y),
                        Random.Range(_rangeOfImpulse.x, _rangeOfImpulse.y));
                    rigidbody.AddForce(velocity, ForceMode2D.Impulse);
                }
        }
    }
}