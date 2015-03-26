using System;
using System.Windows;
using System.Windows.Threading;
using Hardcodet.Wpf.TaskbarNotification;
using Ninject;
using NLog;
using TimeTrackR.Classes;
using TimeTrackR.ViewModels;

namespace TimeTrackR
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private TaskbarIcon notifyIcon;
        private Logger _logger;

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            IKernel kernel = new StandardKernel(new DiModule());

            _logger  = kernel.Get<Logger>();
            var viewModel = kernel.Get<NotifyIconViewModel>();

            DispatcherUnhandledException += OnDispatcherUnhandledException;
            AppDomain.CurrentDomain.UnhandledException += CurrentDomainOnUnhandledException;

            // Create the notifyicon (it's a resource declared in NotifyIconResources.xaml)
            notifyIcon = (TaskbarIcon)FindResource("SysTrayNotifyIcon");

            if(notifyIcon != null)
            {
                notifyIcon.DataContext = viewModel;
            }
        }

        private void CurrentDomainOnUnhandledException(object sender, UnhandledExceptionEventArgs unhandledExceptionEventArgs)
        {
            _logger.FatalException("An unhandled exception has been thrown", unhandledExceptionEventArgs.ExceptionObject as Exception);
        }

        private void OnDispatcherUnhandledException(object sender, DispatcherUnhandledExceptionEventArgs dispatcherUnhandledExceptionEventArgs)
        {
            _logger.FatalException("An unhandled exception has been thrown", dispatcherUnhandledExceptionEventArgs.Exception);
        }

        protected override void OnExit(ExitEventArgs e)
        {
            notifyIcon.Dispose();
            base.OnExit(e);
        }
    }
}
