using System.Collections.Generic;
using TimeTrackR.Core.Timer;

namespace TimeTrackR.Core.Tags
{
    public class Tag
    {
        public int id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<TimerHistoryItem> TimerHistoryItems { get; set; }
    }
}