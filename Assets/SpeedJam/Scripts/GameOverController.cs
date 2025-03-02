using PrimeTween;
using System;
using System.Collections;
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
        
        private Player _player;
        private Rigidbody2D _rigidbody;
        private Controls _controls;

        [Inject]
        public void Construct(Player player, Controls controls)
        {
            _player = player;
            _controls = controls;
            _rigidbody = _player.GetComponent<Rigidbody2D>();
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
            if (_player.State != Player.CharacterState.OnAir)
                return;

            if (_player.JetpackCharge > 0.1f)
                return;

            if (_rigidbody.velocity.magnitude > 0.1f)
                return;

            PlayerDie();
        }

        public void PlayerDie()
        {
            if (!_player.gameObject.activeSelf)
                return;
            
            StartCoroutine(GameOverAnimation(() =>
            {
                SceneManager.LoadScene(0);
            }));
        }

        private IEnumerator GameOverAnimation(Action callback)
        {
            if (!_player.gameObject.activeSelf)
                yield break;
            
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