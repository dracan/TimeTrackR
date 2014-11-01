using System;
using System.Collections.Generic;
using TimeTrackR.Core.Tags;

namespace TimeTrackR.Core.Timer
{
    public class TimerHistoryItem
    {
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
        public IEnumerable<Tag> Tags { get; set; }

        public TimeSpan Length
        {
            get { return End - Start; }
        }
    }
}