using System.Collections.Generic;
using System.Windows;
using TimeTrackR.Core.Timer;

namespace TimeTrackR
{
    /// <summary>
    /// Interaction logic for Report.xaml
    /// </summary>
    public partial class Report : Window
    {
        public IList<TimerHistoryItem> HistoryItems { get; set; }

        public Report(IList<TimerHistoryItem> historyItems)
        {
            HistoryItems = historyItems;

            InitializeComponent();
        }
    }
}
