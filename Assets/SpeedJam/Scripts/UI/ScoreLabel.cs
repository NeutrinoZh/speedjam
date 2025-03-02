using TMPro;
using UnityEngine;
using Zenject;

namespace SpeedJam
{
    public class ScoreLabel : MonoBehaviour
    {
        private TextMeshProUGUI _scoreLabel;
        private string _pattern;

        private ScoreManager _scoreManager;
        private Player _player;

        [Inject]
        public void Construct(ScoreManager scoreManager, Player player)
        {
            _scoreManager = scoreManager;
            _player = player;
        }
        
        private void Awake()
        {
            _scoreLabel = GetComponent<TextMeshProUGUI>();
            _pattern = _scoreLabel.text;
        }

        private void Start()
        {
            _player.OnCollectStar += Handle;
            
            Handle();
        }

        private void OnDestroy()
        {
            _player.OnCollectStar -= Handle;
        }

        private void Handle()
        {
            _scoreLabel.text = _pattern
                .Replace("{n}", _scoreManager.Score.ToString())
                .Replace("{m}", _scoreManager.MaxScore.ToString());
        }

    }
}