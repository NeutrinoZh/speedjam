using PrimeTween;
using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace SpeedJam
{
    public class EndGameCanvas : MonoBehaviour
    {
        [SerializeField] private Transform _popup;
        [SerializeField] private Transform _leaderboard;
        
        [SerializeField] private TextMeshProUGUI _timerLabel;
        [SerializeField] private TextMeshProUGUI _bestScoreLabel;
        
        [SerializeField] private Image _blackScreen;
        [SerializeField] private float _blackScreenDuration;
        
        [SerializeField] private Image _endGameBlackScreen;
        [SerializeField] private float _endGameBlackScreenDuration;

        private string _bestScorePattern;
        
        private ScoreManager _scoreManager;
        private GlobalData _globalData;
        
        public float EndGameBlackScreenDuration => _endGameBlackScreenDuration;
        
        [Inject]
        public void Construct(ScoreManager scoreManager, GlobalData globalData)
        {
            _scoreManager = scoreManager;
            _globalData = globalData;
        }

        private void Awake()
        {
            _bestScorePattern = _bestScoreLabel.text;
        }

        private void OnEnable()
        {
            float alpha = _blackScreen.color.a;
            _blackScreen.color = new Color(
                _blackScreen.color.r,
                _blackScreen.color.g,
                _blackScreen.color.b, 
                0);
            
            Tween.Alpha(_blackScreen, alpha, _blackScreenDuration, ease: Ease.Linear)
                  .OnComplete(() =>
                  {
                      _popup.gameObject.SetActive(true);
                      _leaderboard.gameObject.SetActive(true);
                  });
            
            _timerLabel.text = (_scoreManager.FinishTime - _scoreManager.StartTime).ToString(@"mm\:ss\.ff");

            var bestScoreText = TimeSpan.FromMilliseconds(_globalData.BestScore).ToString(@"mm\:ss\.ff");
            _bestScoreLabel.text = _bestScorePattern.Replace("{}", bestScoreText);
        }

        public void BlackScreenAnimate()
        {
            Tween.Alpha(_endGameBlackScreen, 1f, _endGameBlackScreenDuration);
        }
    }
}
