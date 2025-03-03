using System;
using System.Collections;
using Unity.Services.Leaderboards;
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
        private Rigidbody2D _rigidbody;
        private GlobalData _globalData;
        private Player _player;
        

        [Inject]
        public void Construct(ScoreManager scoreManager,  EndGameCanvas endGameCanvas, Controls controls, TimerLabel timerLabel, Player player, GameOverController gameOverController, GlobalData globalData)
        {
            _scoreManager = scoreManager;
            _endGameCanvas = endGameCanvas;
            _controls = controls;
            _hud = timerLabel.transform.parent;
            _player = player;
            _rigidbody = player.GetComponent<Rigidbody2D>();
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
            if (!_scoreManager.Finished)
                return;
            
            _endGameCanvas.BlackScreenAnimate();
            _gameOverController.PlayerDie();   
        }

        private void NextLevel(InputAction.CallbackContext ctx)
        {
            if (!_scoreManager.Finished)
                return;
            
            StartCoroutine(NextLevelAnimation());
        }

        private IEnumerator NextLevelAnimation()
        {
            _endGameCanvas.BlackScreenAnimate();
            
            yield return new WaitForSeconds(_endGameCanvas.EndGameBlackScreenDuration);
            
            _controls.UI.Disable();
            
            _globalData.CurrentLevel += 1;
            _globalData.CurrentLevel %= SceneManager.sceneCountInBuildSettings;
            if (_globalData.CurrentLevel == 0)
                _globalData.CurrentLevel = 1;
            
            SceneManager.LoadScene(_globalData.CurrentLevel);
        }
        
        private void Update()
        {
        #if UNITY_EDITOR
            if (Keyboard.current.cKey.wasPressedThisFrame)
            {
                _scoreManager.Score += 1;
                _player.OnCollectStar?.Invoke();
            }
        #endif    
            
            if (_scoreManager.Score != _scoreManager.MaxScore)
                return;

            if (_scoreManager.Finished)
                return;

            _controls.Player.Disable();
            _controls.UI.Enable();
            
            _scoreManager.Finished = true;
            _scoreManager.FinishTime = DateTime.Now;
            
            BestScoreUpdate();
            
            _rigidbody.isKinematic = true;
            _rigidbody.velocity = Vector2.zero;
            _endGameCanvas.gameObject.SetActive(true);
            _hud.gameObject.SetActive(false);
            
            UpdateLeaderboardEntries();
        }

        private void BestScoreUpdate()
        {
            string dataRef = $"{DataRefs.bestScore}{_globalData.CurrentLevel}";
            int score = (int)(_scoreManager.FinishTime - _scoreManager.StartTime).TotalMilliseconds;
            
            int bestScore = PlayerPrefs.GetInt(dataRef, int.MaxValue);
            if (score < bestScore || bestScore == 0)
            {
                bestScore = score;
                PlayerPrefs.SetInt(dataRef, bestScore);
            }
            
            _globalData.BestScore = bestScore;
        }
        
        private async void UpdateLeaderboardEntries()
        {
            try
            {
                await LeaderboardsService.Instance.AddPlayerScoreAsync(_globalData.LeaderboardId, _globalData.BestScore);
            }
            catch (Exception e)
            {
                Debug.LogError(e);
            }
        }
    }
}