using System;

namespace TimeTrackR.Core.Hotkeys
{
    public interface IHotKeyRegisterCallback
    {
        void SetCallback(HotkeyActions action, Action callback);
    }
}