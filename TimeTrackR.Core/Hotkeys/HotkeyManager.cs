using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Interop;

namespace TimeTrackR.Core.Hotkeys
{
    public class HotkeyManager : IHotKeyRegisterCallback
    {
        private readonly List<Hotkey> _hotkeys = new List<Hotkey>
            {
                new Hotkey
                {
                    Action = HotkeyActions.StartTimer,
                    GlobalHotkey = new GlobalHotkey(81 /* q */, GlobalHotkey.MOD_WIN)
                },
                new Hotkey
                {
                    Action = HotkeyActions.StopTimer,
                    GlobalHotkey = new GlobalHotkey(87 /* w */, GlobalHotkey.MOD_WIN)
                },
                new Hotkey
                {
                    Action = HotkeyActions.SetTags,
                    GlobalHotkey = new GlobalHotkey(65 /* a */, GlobalHotkey.MOD_WIN)
                },
                new Hotkey
                {
                    Action = HotkeyActions.ShowReportWindow,
                    GlobalHotkey = new GlobalHotkey(90 /* z */, GlobalHotkey.MOD_WIN)
                },
            };

        private Dictionary<ushort, Hotkey> _hotkeyIDLookup;
        private Dictionary<HotkeyActions, Hotkey> _hotkeyActionLookup;
        private bool _initialised;

        private void Init()
        {
            _initialised = true;

            _hotkeyIDLookup = _hotkeys.ToDictionary(k => k.GlobalHotkey.HotkeyID, v => v);
            _hotkeyActionLookup = _hotkeys.ToDictionary(k => k.Action, v => v);

            // Setup an event handler to capture the WN_HOTKEY message which occurs when the user pressed a registered hotkey
            ComponentDispatcher.ThreadFilterMessage += (ref MSG msg, ref bool handled) =>
                                                       {
                                                           if(msg.message == 0x0312) // WM_HOTKEY
                                                           {
                                                               var hotkeyID = (ushort)msg.wParam;

                                                               if(_hotkeyIDLookup[hotkeyID] != null)
                                                               {
                                                                   _hotkeyIDLookup[hotkeyID].Callback();
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