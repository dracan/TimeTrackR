using System;
using System.Collections.ObjectModel;
using TimeTrackR.Core.Tags;

namespace TimeTrackR.Core.Timer
{
    public class TimerHistoryItem
    {
        private int _id;
        private DateTime _start;
        private DateTime _end;
        private ObservableCollection<Tag> _tags;

        public int Id
        {
            get { return _id; }
            set
            {
                _id = value;
                Dirty = true;
            }
        }

        public DateTime Start
        {
            get { return _start; }
            set
            {
                _start = value;
                Dirty = true;
            }
        }

        public DateTime End
        {
            get { return _end; }
            set
            {
                _end = value;
                Dirty = true;
            }
        }

        public virtual ObservableCollection<Tag> Tags
        {
            get { return _tags; }
            set
            {
                _tags = value;
                _tags.CollectionChanged += (sender, args) => Dirty = true;
                Dirty = true;
            }
        }

        public bool Dirty = true;

        public TimeSpan Length
        {
            get { return End - Start; }
        }
    }
}