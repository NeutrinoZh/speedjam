using System;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using Zenject;

namespace SpeedJam
{
    public class CompleteLevelController : MonoBehaviour
    {
        private ScoreManager _scoreManager;
        private EndGameCanvas _endGameCanvas;
        private GameOverController _gameOverController;
        private Controls _controls;
        private Transform _hud;
        private Rigidbody2D _player;
        private GlobalData _globalData;

        [Inject]
        public void Construct(ScoreManager scoreManager,  EndGameCanvas endGameCanvas, Controls controls, TimerLabel timerLabel, Player player, GameOverController gameOverController, GlobalData globalData)
        {
            _scoreManager = scoreManager;
            _endGameCanvas = endGameCanvas;
            _controls = controls;
            _hud = timerLabel.transform.parent;
            _player = player.GetComponent<Rigidbody2D>();
            _gameOverController = gameOverController;
            _globalData = globalData;
        }

        private void Start()
        {
            _controls.UI.Restart.performed += Restart;
            _controls.UI.NextLevel.performed += NextLevel;
        }

        private void OnDestroy()
        {
            _controls.UI.Restart.performed -= Restart;   
            _controls.UI.NextLevel.performed -= NextLevel;
        }

        private void Restart(InputAction.CallbackContext ctx)
        {
            _endGameCanvas.BlackScreenAnimate();
            _gameOverController.PlayerDie();   
        }

        private void NextLevel(InputAction.CallbackContext ctx)
        {
            StartCoroutine(NextLevelAnimation());
        }

        private IEnumerator NextLevelAnimation()
        {
            _endGameCanvas.BlackScreenAnimate();
            
            yield return new WaitForSeconds(_endGameCanvas.EndGameBlackScreenDuration);
            
            _controls.UI.Disable();
            
            _globalData.CurrentLevel += 1;
            _globalData.CurrentLevel %= SceneManager.sceneCountInBuildSettings;
            
            SceneManager.LoadScene(_globalData.CurrentLevel);
        }
        
        private void Update()
        {
            if (Keyboard.current.cKey.wasPressedThisFrame)
                _scoreManager.Score += 1;
                
            if (_scoreManager.Score != _scoreManager.MaxScore)
                return;

            _controls.Player.Disable();
            _controls.UI.Enable();
            
            _scoreManager.Finished = true;
            _scoreManager.FinishTime = DateTime.Now;
            _player.isKinematic = true;
            _player.velocity = Vector2.zero;
            _endGameCanvas.gameObject.SetActive(true);
            _hud.gameObject.SetActive(false);
        }
    }
}