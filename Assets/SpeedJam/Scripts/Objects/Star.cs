using UnityEngine;

namespace SpeedJam
{
    public class Star : MonoBehaviour
    {
        [SerializeField] private Transform _collectStarParticle;
        [SerializeField] private Transform _particleParent;
        
        private void OnDestroy()
        {
            Transform particle = Instantiate(_collectStarParticle, _particleParent, true);
            particle.position = transform.position;
        }
    }
}
