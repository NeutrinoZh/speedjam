using System;
using UnityEngine;

namespace SpeedJam
{
    public class PlayerAnimator : MonoBehaviour
    {
        private Animator _animator;
        private Player _player;
        private PlayerExtraction _extraction;
        private PlayerOnGround _playerOnGround;
        private PlayerOnAir _playerOnAir;
        private SpriteRenderer _sprite;
        
        private ParticleSystem _jetpackParticles;
        private ParticleSystem.EmissionModule _emission;
        private float _rateOverTime;
        
        private static readonly int k_velocity = Animator.StringToHash("velocity");
        private static readonly int k_absVelocity = Animator.StringToHash("absVelocity");
        private static readonly int k_extractionInProgress = Animator.StringToHash("extractionInProgress");
        private static readonly int k_onGround = Animator.StringToHash("onGround");

        private void Awake()
        {
            _animator = GetComponent<Animator>();
            _player = GetComponent<Player>();
            _extraction = GetComponent<PlayerExtraction>();
            _sprite = GetComponent<SpriteRenderer>();
            _playerOnGround = GetComponent<PlayerOnGround>();
            _playerOnAir = GetComponent<PlayerOnAir>();
            
            _jetpackParticles = GetComponentInChildren<ParticleSystem>();
            _emission = _jetpackParticles.emission;
            _rateOverTime = _emission.rateOverTime.constant;
        }
        
        private void Update()
        {
            if (_player.State == Player.CharacterState.OnAir && _playerOnAir.Direction.y > 0 && _player.JetpackCharge > 0)
                _emission.rateOverTime = _rateOverTime;
            else
                _emission.rateOverTime = 0;
            
            if (_player.State == Player.CharacterState.OnGround)
                _sprite.flipX = _playerOnGround.Direction.x > 0 || (!(_playerOnGround.Direction.x < 0) && _sprite.flipX);   
            
            _animator.SetFloat(k_velocity, _playerOnGround.Direction.x);
            _animator.SetFloat(k_absVelocity, Mathf.Abs(_playerOnGround.Direction.x));
            _animator.SetBool(k_extractionInProgress, _extraction.ExtractionInProgress);
            _animator.SetBool(k_onGround, _player.State == Player.CharacterState.OnGround);
        }
    }
}