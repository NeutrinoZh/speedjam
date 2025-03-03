using UnityEngine;
using Zenject;

namespace SpeedJam
{
    public class PlayerCollider : MonoBehaviour
    {
        private Player _player;
        private PlayerOnGround _playerOnGround;
        private ScoreManager _scoreManager;
        private GameOverController _gameOverController;

        [Inject]
        public void Construct(ScoreManager scoreManager, GameOverController gameOverController)
        {
            _scoreManager = scoreManager;
            _gameOverController = gameOverController;
        }

        private void Awake()
        {
            _player = GetComponent<Player>();
            _playerOnGround = GetComponent<PlayerOnGround>();
        }

        private void Cling(GravitationalObject obj)
        {
            if (obj.IsTrap)
            {
                _gameOverController.PlayerDie();
                return;
            }

            _playerOnGround.ClingObject = obj;
            _player.State = Player.CharacterState.OnGround;
        }

        private void CollectStar(Star star)
        {
            _scoreManager.Score += 1;
            
            star.SpawnParticle();
            Destroy(star.gameObject);   
            
            _player.OnCollectStar?.Invoke();
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.transform.TryGetComponent(out Star star)) 
                CollectStar(star);
        }
        
        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (_player.State != Player.CharacterState.OnAir)
                return;

            if (collision.transform.TryGetComponent(out GravitationalObject obj))
                Cling(obj);
            
            if (collision.transform.GetComponentInParent<WorldBounds>() != null)
                _gameOverController.PlayerDie();
        }
    }
}