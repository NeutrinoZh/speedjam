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
        [SerializeField] private TextMeshProUGUI _timerLabel;
        
        [SerializeField] private Image _blackScreen;
        [SerializeField] private float _blackScreenDuration;
        
        [SerializeField] private Image _endGameBlackScreen;
        [SerializeField] private float _endGameBlackScreenDuration;
        
        private ScoreManager _scoreManager;

        public float EndGameBlackScreenDuration => _endGameBlackScreenDuration;
        
        [Inject]
        public void Construct(ScoreManager scoreManager)
        {
            _scoreManager = scoreManager;
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
                  .OnComplete(() => _popup.gameObject.SetActive(true));
            
            _timerLabel.text = (_scoreManager.FinishTime - _scoreManager.StartTime).ToString(@"mm\:ss");
        }

        public void BlackScreenAnimate()
        {
            Tween.Alpha(_endGameBlackScreen, 1f, _endGameBlackScreenDuration);
        }
    }
}
