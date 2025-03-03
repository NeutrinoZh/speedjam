using UnityEngine;

namespace SpeedJam
{
    public class GlobalData
    {
        private int _currentLevel;
        private string _leaderboardId;

        public int CurrentLevel
        {
            get => _currentLevel;
            set
            {
                _currentLevel = value;
                _leaderboardId = $"level{_currentLevel}";
            }
        }

        public string LeaderboardId => _leaderboardId;

        public string Username { get; set; }
        public int BestScore { get; set; }
    }
}