using System;
using System.Collections;
using UnityEngine;

namespace SpeedJam
{
    public class PlayerSound : MonoBehaviour
    {
        [SerializeField] private AudioSource _walkingSound;
        [SerializeField] private AudioSource _flyingSound;
        [SerializeField] private AudioSource _extractorSound;
        [SerializeField] private AudioSource _defaultSource;
        
        [SerializeField] private AudioClip _landSound;
        [SerializeField] private AudioClip _jumpSound;
        [SerializeField] private AudioClip _deathSound;
        [SerializeField] private AudioClip _deathBlackHoleSound;
        [SerializeField] private AudioClip _starCollect;
        
        [SerializeField] private AudioClip _outOfFuelSound;
        [SerializeField] private float _outOfFuelDelay;
        
        private PlayerOnGround _playerOnGround;
        private PlayerOnAir _playerOnAir;
        private Player _player;
        private PlayerExtraction _playerExtraction;
        
        private bool _canPlayOutOfFuel;
        
        private void Awake()
        {
            _playerOnGround = GetComponent<PlayerOnGround>();
            _playerExtraction = GetComponent<PlayerExtraction>();
            _playerOnAir = GetComponent<PlayerOnAir>();
            _player = GetComponent<Player>();
        }

        private void Start()
        {
            _player.OnLand += Land;
            _player.OnJump += Jump;
            _player.OnCollectStar += StarCollect;
            _player.OutOfFuel += OutOfFuel;
        }

        private void OnDestroy()
        {
            _player.OnLand -= Land;
            _player.OnJump -= Jump;
            _player.OnCollectStar -= StarCollect;
            _player.OutOfFuel -= OutOfFuel;
        }

        private void Land() => _defaultSource.PlayOneShot(_landSound);
        private void Jump() => _defaultSource.PlayOneShot(_jumpSound);
        private void StarCollect() => _defaultSource.PlayOneShot(_starCollect);

        private void OutOfFuel()
        {
            if (!_canPlayOutOfFuel)
                return;
            
            _canPlayOutOfFuel = false;
            _defaultSource.PlayOneShot(_outOfFuelSound);

            StartCoroutine(CallWithDelay(() => _canPlayOutOfFuel = true, _outOfFuelDelay));
        }

        private IEnumerator CallWithDelay(Action action, float delay)
        {
            yield return new WaitForSeconds(delay);
            action?.Invoke();
        }

        private void Update()
        {
            _walkingSound.volume = _player.State == Player.CharacterState.OnGround && Mathf.Abs(_playerOnGround.Direction.x) > 0.01f ? 1f : 0f;
            _flyingSound.volume = _player.State == Player.CharacterState.OnAir && _player.JetpackCharge > 0.01f && Mathf.Abs(_playerOnAir.Direction.y) > 0.01f ? 1f : 0f;
            _extractorSound.volume = _playerExtraction.ExtractionInProgress ? 0.1f : 0f;
        }
    }
}