using System.Linq;
using System.Windows;

namespace TimeTrackR.Helpers
{
    internal static class WindowHelpers
    {
        public static bool IsWindowOpen<T>(string name = "") where T : Window
        {
            return string.IsNullOrEmpty(name)
               ? Application.Current.Windows.OfType<T>().Any()
               : Application.Current.Windows.OfType<T>().Any(w => w.Name.Equals(name));
        }

        public static T FindWindow<T>(string name = "") where T : Window
        {
            return string.IsNullOrEmpty(name)
               ? Application.Current.Windows.OfType<T>().FirstOrDefault()
               : Application.Current.Windows.OfType<T>().FirstOrDefault(w => w.Name.Equals(name));
        }
    }
}
