using System.ComponentModel;
using System.Windows;
using System.Windows.Input;
using TimeTrackR.Core.Hotkeys;
using TimeTrackR.Core.Tags;
using TimeTrackR.Core.Timer;

namespace TimeTrackR.ViewModels
{
    /// <summary>
    /// Provides bindable properties and commands for the NotifyIcon. In this sample, the
    /// view model is assigned to the NotifyIcon in XAML. Alternatively, the startup routing
    /// in App.xaml.cs could have created this view model, and assigned it to the NotifyIcon.
    /// </summary>
    public class NotifyIconViewModel : INotifyPropertyChanged
    {
        public Timer Timer { get; set; }
        public ITagSetProvider TagSetProvider { get; set; }
        public IHotKeyRegisterCallback HotKeyRegisterCallback { get; set; }

        public NotifyIconViewModel(Timer timer, ITagSetProvider tagSetProvider, IHotKeyRegisterCallback hotKeyRegisterCallback)
        {
            Timer = timer;
            TagSetProvider = tagSetProvider;
            HotKeyRegisterCallback = hotKeyRegisterCallback;

            RegisterHotkeys();
        }

        private void RegisterHotkeys()
        {
            HotKeyRegisterCallback.SetCallback(HotkeyActions.StartTimer, StartTimer);
            HotKeyRegisterCallback.SetCallback(HotkeyActions.StopTimer, StopTimer);
            HotKeyRegisterCallback.SetCallback(HotkeyActions.SetTags, SetTags);
            HotKeyRegisterCallback.SetCallback(HotkeyActions.ShowReportWindow, ShowReport);
        }

        private void StartTimer()
        {
            if(Timer.State == Timer.States.Stopped)
            {
                Timer.Start();
                OnPropertyChanged("SystemTrayIcon");
            }
        }

        private void StopTimer()
        {
            if(Timer.State == Timer.States.Started)
            {
                Timer.Stop();
                OnPropertyChanged("SystemTrayIcon");
                OnPropertyChanged("Timer");
            }
        }

        public string SystemTrayIcon
        {
            get { return Timer.State == Timer.States.Started ? "/Resources/button_red_record.ico" : "/Resources/button_grey_record.ico"; }
        }

        private void SetTags()
        {
            var viewModel = new TagSelectionViewModel(Timer, TagSetProvider);
            var window = new TagSelection {DataContext = viewModel};
            window.ShowDialog();

            if(viewModel.HaveTagsChanged)
            {
                if(Timer.State == Timer.States.Started)
                {
                    // Restart the timer so that the new tagset is used
                    Timer.Stop();
                    Timer.Start();
                }
            }
        }

        private void ShowReport()
        {
            var window = new Report { DataContext = new ReportViewModel(Timer.HistoryItems) };
            window.Show();
        }

        /// <summary>
        /// Shows a window, if none is already open.
        /// </summary>
        public ICommand ShowOptionsWindowCommand
        {
            get
            {
                return new DelegateCommand
                {
                    CanExecuteFunc = () => Application.Current.MainWindow == null,
                    CommandAction = o =>
                    {
                        Application.Current.MainWindow = new OptionsWindow();
                        Application.Current.MainWindow.Show();
                    }
                };
            }
        }

        public ICommand SetCurrentTagsCommand
        {
            get
            {
                return new DelegateCommand
                {
                    CanExecuteFunc = () => true,
                    CommandAction = o => SetTags()
                };
            }
        }

        /// <summary>
        /// Starts the timer
        /// </summary>
        public ICommand StartTimerCommand
        {
            get
            {
                return new DelegateCommand
                {
                    CanExecuteFunc = () => Timer.State == Timer.States.Stopped,
                    CommandAction = o => StartTimer()
                };
            }
        }

        /// <summary>
        /// Stops the timer
        /// </summary>
        public ICommand StopTimerCommand
        {
            get
            {
                return new DelegateCommand
                {
                    CanExecuteFunc = () => Timer.State == Timer.States.Started,
                    CommandAction = o => StopTimer()
                };
            }
        }

        /// <summary>
        /// Report
        /// </summary>
        public ICommand ShowReportWindowCommand
        {
            get
            {
                return new DelegateCommand
                {
                    CanExecuteFunc = () => true,
                    CommandAction = o => ShowReport()
                };
            }
        }

        /// <summary>
        /// Shuts down the application.
        /// </summary>
        public ICommand ExitApplicationCommand
        {
            get
            {
                return new DelegateCommand {CommandAction = o => Application.Current.Shutdown()};
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName = null)
        {
            var handler = PropertyChanged;
            if(handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
