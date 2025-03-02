using PrimeTween;
using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Zenject;

namespace SpeedJam
{
    public class GameOverController : MonoBehaviour
    {
        [SerializeField] private Transform _deadPlayerPrefab;
        [SerializeField] private float _deadDelay;
        [SerializeField] private float _blackScreenDuration;
        [SerializeField] private Image _blackScreen;
        
        public float BlackScreenDuration => _blackScreenDuration;
        
        private Player _player;
        private AdviceLabel _adviceLabel;
        private Rigidbody2D _rigidbody;
        private Controls _controls;
        private GlobalData _globalData;

        [Inject]
        public void Construct(Player player, Controls controls, AdviceLabel adviceLabel, GlobalData globalData)
        {
            _player = player;
            _adviceLabel = adviceLabel;
            _controls = controls;
            _rigidbody = _player.GetComponent<Rigidbody2D>();
            _globalData = globalData;
        }

        private void Start()
        {
            Tween.Alpha(_blackScreen, 0f, _blackScreenDuration);
            
            _controls.Player.Restart.performed += Handle;
        }

        private void OnDestroy()
        {
            _controls.Player.Restart.performed -= Handle;
        }

        private void Handle(InputAction.CallbackContext ctx)
        {
            PlayerDie();
        }

        private void Update()
        {
            bool showAdvice = _player.State == Player.CharacterState.OnAir &&
                              _player.JetpackCharge < 0.1f &&
                              _rigidbody.velocity.sqrMagnitude < 0.1f;
            
            _adviceLabel.gameObject.SetActive(showAdvice);
        }

        public void PlayerDie()
        {
            if (!_player.gameObject.activeSelf)
                return;
            
            StartCoroutine(GameOverAnimation(() =>
            {
                SceneManager.LoadScene(_globalData.CurrentLevel);
            }));
        }
        
        private IEnumerator GameOverAnimation(Action callback)
        {
            if (!_player.gameObject.activeSelf)
                yield break;
            
            _player.OnDeath?.Invoke();
            
            var deadPlayer = Instantiate(_deadPlayerPrefab);
            deadPlayer.position = _player.transform.position;   
            
            _player.gameObject.SetActive(false);
            
            yield return new WaitForSeconds(_deadDelay);
            
            Tween.Alpha(_blackScreen, 1f, _blackScreenDuration);
            
            yield return new WaitForSeconds(_blackScreenDuration);
            
            callback?.Invoke();
        }
    }
}