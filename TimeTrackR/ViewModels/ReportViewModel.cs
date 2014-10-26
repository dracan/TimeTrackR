using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using TimeTrackR.Core.Annotations;
using TimeTrackR.Core.Timer;

namespace TimeTrackR.ViewModels
{
    public class ReportViewModel : INotifyPropertyChanged
    {
        public IEnumerable<TimerHistoryItem> HistoryItems { get; set; }
        public event PropertyChangedEventHandler PropertyChanged;

        public IEnumerable<TimerHistoryItem> FilteredHistoryItems
        {
            get
            {
                return (from hi in HistoryItems
                        where (hi.Start >= Filter_StartDateTime)
                              && ((hi.Start + hi.Length) <= Filter_EndDateTime)
                        select hi).ToList();
            }
        }

        private DateTime _filter_StartDateTime = DateTime.Now.Date;
        public DateTime Filter_StartDateTime
        {
            get { return _filter_StartDateTime; }
            set
            {
                _filter_StartDateTime = value;
                OnPropertyChanged("FilteredHistoryItems");
            }
        }

        private DateTime _filter_EndDateTime = DateTime.Now;
        public DateTime Filter_EndDateTime
        {
            get { return _filter_EndDateTime; }
            set
            {
                _filter_EndDateTime = value;
                OnPropertyChanged("FilteredHistoryItems");
            }
        }

        public ReportViewModel(IEnumerable<TimerHistoryItem> historyItems)
        {
            HistoryItems = historyItems;
        }

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            var handler = PropertyChanged;
            if(handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}