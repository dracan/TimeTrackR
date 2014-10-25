using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Input;
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

            DateTimePickerStart.Value = DateTime.Today;
            DateTimePickerEnd.Value = DateTime.Now;

            PreviewKeyDown += OnKeyDown;
        }

        private void OnKeyDown(object sender, KeyEventArgs e)
        {
            if(e.Key == Key.Escape)
            {
                Close();
            }
        }
    }
}
