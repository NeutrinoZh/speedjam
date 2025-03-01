using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;

namespace SpeedJam
{
    public class GameOverController : MonoBehaviour
    {
        private Player _player;
        private Rigidbody2D _rigidbody;

        [Inject]
        public void Construct(Player player)
        {
            _player = player;
            _rigidbody = _player.GetComponent<Rigidbody2D>();
        }

        private void Update()
        {
            if (_player.State != Player.CharacterState.OnAir)
                return;

            if (_player.JetpackCharge > 0.1f)
                return;

            if (_rigidbody.velocity.magnitude > 0.1f)
                return;

            Restart();
        }

        public void Restart()
        {
            SceneManager.LoadScene(0);
        }
    }
}