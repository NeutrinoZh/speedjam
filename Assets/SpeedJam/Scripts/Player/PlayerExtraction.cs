using System;
using UnityEngine;
using Zenject;

namespace SpeedJam
{
    public class PlayerExtraction : MonoBehaviour
    {
        public bool ExtractionInProgress => _extractionInProgress;
        
        private bool _extractionInProgress;
        private PlayerOnGround _playerOnGround;
        private Player _player;

        [Inject]
        public void Construct(Player player)
        {
            _player = player;
        }

        private void Awake()
        {
            _playerOnGround = GetComponent<PlayerOnGround>();
        }

        private void Update()
        {
            if (_player.State != Player.CharacterState.OnGround)
                return;
            
            var clingObject = _playerOnGround.ClingObject;
            _extractionInProgress = clingObject.Charge > 0 && _player.JetpackCharge < _player.MaxJetpackCharge;

            if (!_extractionInProgress)
                return;

            var changeValue = _player.JetpackChargeRefuelingRate * Time.deltaTime;
            _player.JetpackCharge = Mathf.Clamp(_player.JetpackCharge + changeValue, 0, _player.MaxJetpackCharge);
            clingObject.Charge = Mathf.Clamp(clingObject.Charge - changeValue, 0, 1000);
        }
    }
}