using NLog;
using TimeTrackR.Core.Data;
using TimeTrackR.Core.Hotkeys;
using TimeTrackR.Core.Tags;
using TimeTrackR.Core.Timer;
using TimeTrackR.ViewModels;

namespace TimeTrackR.Classes
{
    class DiModule : Ninject.Modules.NinjectModule
    {
        public override void Load()
        {
            Bind<Timer>().To<Timer>().InSingletonScope();
            Bind<ITagSetProvider>().To<TagSetProvider>().InSingletonScope();
            Bind<NotifyIconViewModel>().To<NotifyIconViewModel>().InSingletonScope();
            Bind<IHotKeyRegisterCallback>().To<HotkeyManager>().InSingletonScope();
            Bind<IDataContext>().To<DataContext>().InSingletonScope();
            Bind<TimerHistoryItemRepository>().To<TimerHistoryItemRepository>().InSingletonScope();
            Bind<Logger>().ToMethod(x => LogManager.GetLogger("MainLog")).InSingletonScope();
        }
    }
}
