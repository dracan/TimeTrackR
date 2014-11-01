using System.Data.Entity;
using FakeDbSet;
using TimeTrackR.Core.Data;
using TimeTrackR.Core.Tags;
using TimeTrackR.Core.Timer;

namespace TimeTrackR.Core.Tests
{
    public class FakeDataContext : IDataContext
    {
        public IDbSet<TimerHistoryItem> TimerHistoryItems { get; set; }
        public IDbSet<Tag> Tags { get; set; }

        public FakeDataContext()
        {
            TimerHistoryItems = new InMemoryDbSet<TimerHistoryItem>();
            Tags = new InMemoryDbSet<Tag>();
        }

        public void SaveChanges()
        {
        }
    }
}