using System;

namespace TimeTrackR.Core.Hotkeys
{
    public class Hotkey
    {
        public HotkeyActions Action { get; set; }
        public GlobalHotkey GlobalHotkey { get; set; }
        public Action Callback { get; set; }
    }
}