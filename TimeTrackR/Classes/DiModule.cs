using TimeTrackR.Core.Timer;

namespace TimeTrackR.Classes
{
    class DiModule : Ninject.Modules.NinjectModule
    {
        public override void Load()
        {
            Bind<Timer>().To<Timer>();
            Bind<NotifyIconViewModel>().To<NotifyIconViewModel>();
        }
    }
}
