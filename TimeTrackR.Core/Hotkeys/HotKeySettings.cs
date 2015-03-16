using System.Collections.Generic;

namespace TimeTrackR.Core.Hotkeys
{
    public class HotKeySettings
    {
        public IEnumerable<Hotkey> HotKeys { get; set; }

        public HotKeySettings()
        {
            SetupDefaults();
        }

        /// <summary>
        /// Setup the default hotkeys. When hotkeys can be customised, then this class will be updated to save/load the hotkeys from a file, so this
        /// method should then be depreciated as the default hotkeys will be stored in the initial configuration file - not in code.
        /// </summary>
        private void SetupDefaults()
        {
            HotKeys = new List<Hotkey>
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
                          new Hotkey
                          {
                              Action = HotkeyActions.ShowQuickTagSelect,
                              GlobalHotkey = new GlobalHotkey(83 /* s */, GlobalHotkey.MOD_WIN)
                          },
                      };
        }
    }
}