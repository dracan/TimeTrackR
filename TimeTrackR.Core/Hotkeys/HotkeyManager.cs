using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Windows.Interop;

namespace TimeTrackR.Core.Hotkeys
{
    public static class HotkeyManager
    {
        private static readonly List<GlobalHotkey> _hotkeys = new List<GlobalHotkey>
            {
                new GlobalHotkey(81 /* q */, GlobalHotkey.MOD_WIN, (ref MSG msg, ref bool handled) => Debug.WriteLine("Started")),
                new GlobalHotkey(87 /* w */, GlobalHotkey.MOD_WIN, (ref MSG msg, ref bool handled) => Debug.WriteLine("Stopped")),
            };

        private static Dictionary<ushort, GlobalHotkey> _hotkeyLookup;

        public static void Init()
        {
            _hotkeyLookup = _hotkeys.ToDictionary(k => k.HotkeyID, v => v);

            // Setup an event handler to capture the WN_HOTKEY message which occurs when the user pressed a registered hotkey
            ComponentDispatcher.ThreadFilterMessage += (ref MSG msg, ref bool handled) =>
                                                       {
                                                           if(msg.message == 0x0312) // WM_HOTKEY
                                                           {
                                                               var hotkeyID = (ushort)msg.wParam;
                                                               _hotkeyLookup[hotkeyID].Callback(ref msg, ref handled);
                                                           }
                                                       };
        }
    }
}