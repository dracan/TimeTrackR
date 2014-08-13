using System;
using System.Collections.Generic;
using System.Linq;
using TimeTrackR.Core.Tags;

namespace TimeTrackR.Core.Timer
{
    public class Timer
    {
        private readonly ITagSetProvider _tagSetProvider;

        public enum States
        {
            Stopped,
            Started
        }

        public States State { get; set; }

        public List<TimerHistoryItem> HistoryItems;
        private TimerHistoryItem _currentHistoryItem;

        public TimeSpan TotalTime
        {
            get { return new TimeSpan(HistoryItems.Sum(x => x.Length.Ticks)); }
        }

        public Timer(ITagSetProvider tagSetProvider)
        {
            _tagSetProvider = tagSetProvider;

            Reset();
        }

        public void Reset()
        {
            State = States.Stopped;
            HistoryItems = new List<TimerHistoryItem>();
        }

        public void Start()
        {
            if(_currentHistoryItem != null)
            {
                throw new Exception("_currentHistoryItem should be null when calling start!");
            }

            State = States.Started;

            _currentHistoryItem = new TimerHistoryItem { Start = DateTime.Now, TagSet = _tagSetProvider.GetCurrentTagSet()};
        }

        public void Stop()
        {
            if(_currentHistoryItem == null)
            {
                throw new NullReferenceException("_currentHistoryItem should not be null when calling stop!");
            }

            State = States.Stopped;

            _currentHistoryItem.End = DateTime.Now;

            HistoryItems.Add(_currentHistoryItem);
            _currentHistoryItem = null;
        }
    }
}
