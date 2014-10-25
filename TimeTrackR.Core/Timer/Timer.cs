using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using TimeTrackR.Core.Annotations;
using TimeTrackR.Core.Tags;

namespace TimeTrackR.Core.Timer
{
    public class Timer : INotifyPropertyChanged
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

        public string TagsAsString
        {
            get { return string.Join(", ", _tagSetProvider.GetCurrentTagSet().Select(t => t.Name)); }
        }

        public bool HasTags
        {
            get { return _tagSetProvider.GetCurrentTagSet().Any(); }
        }

        public Timer(ITagSetProvider tagSetProvider)
        {
            _tagSetProvider = tagSetProvider;

            _tagSetProvider.OnTagChanged += (sender, args) =>
                                            {
                                                OnPropertyChanged("TagsAsString");
                                                OnPropertyChanged("HasTags");
                                            };

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

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            var handler = PropertyChanged;
            if(handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
