using System.Windows;
using Hardcodet.Wpf.TaskbarNotification;
using Ninject;
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

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            IKernel kernel = new StandardKernel(new DiModule());

            var viewModel = kernel.Get<NotifyIconViewModel>();

            // Create the notifyicon (it's a resource declared in NotifyIconResources.xaml)
            notifyIcon = (TaskbarIcon)FindResource("SysTrayNotifyIcon");

            if(notifyIcon != null)
            {
                notifyIcon.DataContext = viewModel;
            }
        }

        protected override void OnExit(ExitEventArgs e)
        {
            notifyIcon.Dispose();
            base.OnExit(e);
        }
    }
}
