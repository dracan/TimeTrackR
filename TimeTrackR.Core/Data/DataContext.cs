using System.Data.Entity;
using TimeTrackR.Core.Tags;
using TimeTrackR.Core.Timer;

namespace TimeTrackR.Core.Data
{
    public class DataContext : DbContext, IDataContext
    {
        public IDbSet<TimerHistoryItem> TimerHistoryItems { get; set; }
        public IDbSet<Tag> Tags { get; set; }

        public new void SaveChanges()
        {
            base.SaveChanges();
        }
    }
}