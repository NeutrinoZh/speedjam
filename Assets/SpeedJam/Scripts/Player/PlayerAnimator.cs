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
        
        private static readonly int k_flyingAnimation = Animator.StringToHash("Base Layer.Flying");
        private static readonly int k_runAnimation = Animator.StringToHash("Base Layer.Run");
        private static readonly int k_extractionAnimation = Animator.StringToHash("Base Layer.Extraction");
        private static readonly int k_idleAnimation = Animator.StringToHash("Base Layer.Idle");
        private static readonly int k_extractionIdleAnimation = Animator.StringToHash("Base Layer.ExtractionIdle");

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
            else 
                _sprite.flipX = _playerOnAir.Direction.x > 0 || (!(_playerOnAir.Direction.x < 0) && _sprite.flipX);
            
            if (_player.State == Player.CharacterState.OnAir)
                _animator.Play(k_flyingAnimation);
            else
            {
                if (Mathf.Abs(_playerOnGround.Direction.x) > 0.1f)
                {
                    if (_extraction.ExtractionInProgress)
                        _animator.Play(k_extractionAnimation);
                    else 
                        _animator.Play(k_runAnimation);
                }
                else
                {
                    if (_extraction.ExtractionInProgress)
                        _animator.Play(k_extractionIdleAnimation);
                    else 
                        _animator.Play(k_idleAnimation);
                }
            }
        }
    }
}