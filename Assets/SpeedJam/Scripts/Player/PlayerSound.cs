using System;
using UnityEngine;

namespace SpeedJam
{
    public class PlayerSound : MonoBehaviour
    {
        [SerializeField] private AudioSource _walkingSound;
        [SerializeField] private AudioSource _defaultSource;
        
        [SerializeField] private AudioClip _landSound;
        [SerializeField] private AudioClip _jumpSound;
        
        private PlayerOnGround _playerOnGround;
        private Player _player;
        
        private void Awake()
        {
            _playerOnGround = GetComponent<PlayerOnGround>();
            _player = GetComponent<Player>();
        }

        private void Start()
        {
            _player.OnLand += Land;
            _player.OnJump += Jump;
        }

        private void OnDestroy()
        {
            _player.OnLand -= Land;
            _player.OnJump -= Jump;
        }

        private void Land() => _defaultSource.PlayOneShot(_landSound);
        private void Jump() => _defaultSource.PlayOneShot(_jumpSound);

        private void Update()
        {
            _walkingSound.volume = Mathf.Abs(_playerOnGround.Direction.x) > 0.01f ? 1f : 0f;
        }
    }
}