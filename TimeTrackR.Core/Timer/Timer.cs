using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using TimeTrackR.Core.Annotations;
using TimeTrackR.Core.Data;
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

        public delegate void OnStateChangedDelegate(object sender, OnStateChangedEventArgs e);

        public event OnStateChangedDelegate OnStateChanged;

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

        public Timer(ITagSetProvider tagSetProvider, TimerHistoryItemRepository historyItemRepository)
        {
            _tagSetProvider = tagSetProvider;

            _tagSetProvider.OnTagChanged += (sender, args) =>
                                            {
                                                OnPropertyChanged("TagsAsString");
                                                OnPropertyChanged("HasTags");
                                            };

            Reset();

            HistoryItems = historyItemRepository.ListAll().Where(x => x.Start > DateTime.Today).ToList();
        }

        public void Reset()
        {
            State = States.Stopped;
            HistoryItems = new List<TimerHistoryItem>();

            if(OnStateChanged != null)
            {
                OnStateChanged(this, new OnStateChangedEventArgs {State = State});
            }
        }

        public void Start()
        {
            if(_currentHistoryItem != null)
            {
                throw new Exception("_currentHistoryItem should be null when calling start!");
            }

            State = States.Started;

            _currentHistoryItem = new TimerHistoryItem { Start = DateTime.Now, Tags = _tagSetProvider.GetCurrentTagSet()};

            if(OnStateChanged != null)
            {
                OnStateChanged(this, new OnStateChangedEventArgs {State = State});
            }
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

            if(OnStateChanged != null)
            {
                OnStateChanged(this, new OnStateChangedEventArgs {State = State});
            }
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
