using System;

namespace TimeTrackR.Core.Timer
{
    public class TimerHistoryItem
    {
        public DateTime Start { get; set; }
        public DateTime End { get; set; }

        public TimeSpan Length
        {
            get { return End - Start; }
        }
    }
}