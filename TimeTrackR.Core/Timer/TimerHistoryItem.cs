using System;
using System.Collections.Generic;
using TimeTrackR.Core.Tags;

namespace TimeTrackR.Core.Timer
{
    public class TimerHistoryItem
    {
        public int Id { get; set; }
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
        public virtual ICollection<Tag> Tags { get; set; }

        public TimeSpan Length
        {
            get { return End - Start; }
        }
    }
}