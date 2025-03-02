using System;

namespace SpeedJam
{
    public class ScoreManager
    {
        public DateTime StartTime { get; set; } = DateTime.Now;
        public DateTime FinishTime { get; set; }
        public int Score { get; set; }
        public int MaxScore { get; set; }
        public bool Finished { get; set; }
        
    }
}