using System.Data.Entity;
using TimeTrackR.Core.Tags;
using TimeTrackR.Core.Timer;

namespace TimeTrackR.Core.Data
{
    public interface IDataContext
    {
        IDbSet<TimerHistoryItem> TimerHistoryItems { get; set; }
        IDbSet<Tag> Tags { get; set; }

        void SaveChanges();
    }
}