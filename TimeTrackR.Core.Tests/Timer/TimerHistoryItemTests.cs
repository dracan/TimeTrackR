using System;
using System.Collections.ObjectModel;
using System.Diagnostics.CodeAnalysis;
using NUnit.Framework;
using TimeTrackR.Core.Tags;
using TimeTrackR.Core.Timer;

namespace TimeTrackR.Core.Tests.Timer
{
    [TestFixture]
    [SuppressMessage("ReSharper", "UseObjectOrCollectionInitializer")]
    class TimerHistoryItemTests
    {
        [Test]
        public void EnsureInitiallyDirty()
        {
            var thi = new TimerHistoryItem();
            Assert.That(thi.Dirty);
        }

        [Test]
        public void EnsureDirtyAfterEditProperty_Id()
        {
            var thi = new TimerHistoryItem {Dirty = false};
            thi.Id = 123;
            Assert.That(thi.Dirty);
        }

        [Test]
        public void EnsureDirtyAfterEditProperty_Start()
        {
            var thi = new TimerHistoryItem {Dirty = false};
            thi.Start = DateTime.Now;
            Assert.That(thi.Dirty);
        }

        [Test]
        public void EnsureDirtyAfterEditProperty_End()
        {
            var thi = new TimerHistoryItem {Dirty = false};
            thi.End = DateTime.Now;
            Assert.That(thi.Dirty);
        }

        [Test]
        public void EnsureDirtyAfterEditProperty_Tags()
        {
            var thi = new TimerHistoryItem {Dirty = false};
            thi.Tags = new ObservableCollection<Tag>();
            Assert.That(thi.Dirty);
        }

        [Test]
        public void EnsureDirtyAfterAddToTagsCollection()
        {
            var thi = new TimerHistoryItem();
            thi.Tags = new ObservableCollection<Tag>();
            thi.Dirty = false;
            thi.Tags.Add(new Tag());
            Assert.That(thi.Dirty);
        }

        [Test]
        public void EnsureDirtyAfterRemoveFromTagsCollection()
        {
            var thi = new TimerHistoryItem();
            thi.Tags = new ObservableCollection<Tag>();
            thi.Tags.Add(new Tag());
            thi.Dirty = false;
            thi.Tags.RemoveAt(0);
            Assert.That(thi.Dirty);
        }
    }
}
