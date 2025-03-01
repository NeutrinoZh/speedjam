using System;

namespace SpeedJam
{
    public class ScoreManager
    {
        public DateTime StartTime { get; set; } = DateTime.Now;
        public DateTime FinishTime { get; set; }
        public int Score { get; set; }
    }
}