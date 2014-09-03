using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Input;
using TimeTrackR.Core.Hotkeys;
using TimeTrackR.Core.Tags;
using TimeTrackR.Core.Timer;

namespace TimeTrackR
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
        }

        private void StartTimer()
        {
            if(Timer.State == Timer.States.Stopped)
            {
                Timer.Start();
            }
        }

        private void StopTimer()
        {
            if(Timer.State == Timer.States.Started)
            {
                Timer.Stop();
                OnPropertyChanged("Timer");
            }
        }

        private void SetTags()
        {
            var window = new TagSelection(Timer, TagSetProvider);
            window.Show();
        }

        private void ShowReport()
        {
            var window = new Report(Timer.HistoryItems);
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
                    CommandAction = () =>
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
                    CommandAction = () => SetTags()
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
                    CommandAction = () => StartTimer()
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
                    CommandAction = () => StopTimer()
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
                    CommandAction = () => ShowReport()
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
                return new DelegateCommand {CommandAction = () => Application.Current.Shutdown()};
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName = null)
        {
            var handler = PropertyChanged;
            if(handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        }
    }

    /// <summary>
    /// Simplistic delegate command for the demo.
    /// </summary>
    public class DelegateCommand : ICommand
    {
        public Action CommandAction { get; set; }
        public Func<bool> CanExecuteFunc { get; set; }

        public void Execute(object parameter)
        {
            CommandAction();
        }

        public bool CanExecute(object parameter)
        {
            return CanExecuteFunc == null  || CanExecuteFunc();
        }

        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }
    }
}
