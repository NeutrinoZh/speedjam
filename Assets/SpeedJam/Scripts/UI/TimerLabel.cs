using System;
using System.Collections;
using TMPro;
using UnityEngine;
using Zenject;

namespace SpeedJam
{
    public class TimerLabel : MonoBehaviour
    {
        private TextMeshProUGUI _timerLabel;
        private ScoreManager _scoreManager;
        
        [Inject]
        public void Construct(ScoreManager scoreManager)
        {
            _scoreManager = scoreManager;    
        }
        
        private void Awake()
        {
            _timerLabel = GetComponent<TextMeshProUGUI>();
        }

        private void Start()
        {
            StartCoroutine(UpdateTimer());
        }
        
        private IEnumerator UpdateTimer()
        {
            while (true)
            {
                _timerLabel.text = (DateTime.Now - _scoreManager.StartTime).ToString(@"mm\:ss");
                yield return new WaitForSeconds(1);
            }
        }
    }
}