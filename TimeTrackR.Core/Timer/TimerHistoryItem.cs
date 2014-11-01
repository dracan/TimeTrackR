using System;
using System.Collections.Generic;
using TimeTrackR.Core.Tags;

namespace TimeTrackR.Core.Timer
{
    public class TimerHistoryItem
    {
        public int id { get; set; }
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
        public ICollection<Tag> Tags { get; set; }

        public TimeSpan Length
        {
            get { return End - Start; }
        }
    }
}