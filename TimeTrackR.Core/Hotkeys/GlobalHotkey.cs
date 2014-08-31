using System;
using System.Globalization;
using System.Runtime.InteropServices;
using System.Threading;

namespace TimeTrackR.Core.Hotkeys
{
    /// <summary> This class allows you to manage a hotkey. Code has been pinched from http://www.pinvoke.net/default.aspx/user32/RegisterHotKey.html</summary>, and tweaked a bit.
    public class GlobalHotkey : IDisposable
    {
        [DllImport("user32", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool RegisterHotKey(IntPtr hwnd, int id, uint fsModifiers, uint vk);
        [DllImport("user32", SetLastError = true)]
        public static extern int UnregisterHotKey(IntPtr hwnd, int id);
        [DllImport("kernel32", SetLastError = true)]
        public static extern ushort GlobalAddAtom(string lpString);
        [DllImport("kernel32", SetLastError = true)]
        public static extern ushort GlobalDeleteAtom(ushort nAtom);

        public const int MOD_ALT = 1;
        public const int MOD_CONTROL = 2;
        public const int MOD_SHIFT = 4;
        public const int MOD_WIN = 8;

        public const int WM_HOTKEY = 0x312;

        private static ushort _nextHotkeyHandleAtomSeed;

        public GlobalHotkey()
        {
            //Handle = Process.GetCurrentProcess().Handle;
            Handle = IntPtr.Zero;
        }

        public GlobalHotkey(int hotkey, int modifiers)
        {
            //Handle = Process.GetCurrentProcess().Handle;
            Handle = IntPtr.Zero;

            RegisterGlobalHotKey(hotkey, modifiers);
        }

        /// <summary>Handle of the current process</summary>
        public IntPtr Handle;

        /// <summary>The ID for the hotkey</summary>
        public ushort HotkeyID { get; private set; }

        /// <summary>Register the hotkey</summary>
        public void RegisterGlobalHotKey(int hotkey, int modifiers, IntPtr handle)
        {
            UnregisterGlobalHotKey();
            Handle = handle;
            RegisterGlobalHotKey(hotkey, modifiers);
        }

        /// <summary>Register the hotkey</summary>
        public void RegisterGlobalHotKey(int hotkey, int modifiers)
        {
            UnregisterGlobalHotKey();

            try
            {
                // use the GlobalAddAtom API to get a unique ID (as suggested by MSDN)
                var atomName = Thread.CurrentThread.ManagedThreadId.ToString("X8") + GetType().FullName + (_nextHotkeyHandleAtomSeed++);

                HotkeyID = GlobalAddAtom(atomName);

                if(HotkeyID == 0)
                {
                    throw new Exception("Unable to generate unique hotkey ID. Error: " + Marshal.GetLastWin32Error().ToString(CultureInfo.InvariantCulture));
                }

                // register the hotkey, throw if any error
                if(!RegisterHotKey(Handle, HotkeyID, (uint)modifiers, (uint)hotkey))
                {
                    throw new Exception("Unable to register hotkey. Error: " + Marshal.GetLastWin32Error().ToString(CultureInfo.InvariantCulture));
                }
            }
            catch(Exception)
            {
                // clean up if hotkey registration failed
                Dispose();
                throw;
            }
        }

        /// <summary>Unregister the hotkey</summary>
        public void UnregisterGlobalHotKey()
        {
            if(HotkeyID != 0)
            {
                UnregisterHotKey(Handle, HotkeyID);
                // clean up the atom list
                GlobalDeleteAtom(HotkeyID);
                HotkeyID = 0;
            }
        }

        public void Dispose()
        {
            UnregisterGlobalHotKey();
        }
    }
}
