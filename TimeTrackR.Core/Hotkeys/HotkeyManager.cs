using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Interop;

namespace TimeTrackR.Core.Hotkeys
{
    public class HotkeyManager : IHotKeyRegisterCallback
    {
        private readonly HotKeySettings _hotKeySettings;

        private Dictionary<ushort, Hotkey> _hotkeyIdLookup;
        private Dictionary<HotkeyActions, Hotkey> _hotkeyActionLookup;
        private bool _initialised;

        public HotkeyManager(HotKeySettings hotKeySettings)
        {
            _hotKeySettings = hotKeySettings;
        }

        private void Init()
        {
            _initialised = true;

            _hotkeyIdLookup = _hotKeySettings.HotKeys.ToDictionary(k => k.GlobalHotkey.HotkeyID, v => v);
            _hotkeyActionLookup = _hotKeySettings.HotKeys.ToDictionary(k => k.Action, v => v);

            // Setup an event handler to capture the WN_HOTKEY message which occurs when the user pressed a registered hotkey
            ComponentDispatcher.ThreadFilterMessage += (ref MSG msg, ref bool handled) =>
                                                       {
                                                           if(msg.message == 0x0312) // WM_HOTKEY
                                                           {
                                                               var hotkeyID = (ushort)msg.wParam;

                                                               if(_hotkeyIdLookup[hotkeyID] != null)
                                                               {
                                                                   _hotkeyIdLookup[hotkeyID].Callback();
                                                               }
                                                           }
                                                       };
        }

        public void SetCallback(HotkeyActions action, Action callback)
        {
            if(!_initialised)
            {
                Init();
            }

            _hotkeyActionLookup[action].Callback = callback;
        }
    }
}