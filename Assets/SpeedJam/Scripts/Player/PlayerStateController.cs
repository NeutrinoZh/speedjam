using UnityEngine;
using Zenject;

namespace SpeedJam
{
    public class PlayerStateController : MonoBehaviour
    {
        private Player _player;
        private PlayerOnGround _playerOnGround;

        [Inject]
        public void Construct(Player player, PlayerOnGround playerOnGround)
        {
            _player = player;
            _playerOnGround = playerOnGround;
        }

        private void Cling(GravitationalObject obj)
        {
            _playerOnGround.ClingObject = obj;
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (_player.State != Player.CharacterState.OnAir)
                return;

            if (!collision.transform.TryGetComponent(out GravitationalObject obj))
                return;

            if (obj.IsTrap) Debug.Log("Die");

            Cling(obj);
            _player.State = Player.CharacterState.OnGround;
        }
    }
}