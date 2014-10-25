using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace TimeTrackR.Converters
{
    [ValueConversion(typeof(bool), typeof(GridLength))]
    public class BoolToGridLengthConverter : IValueConverter
    {
        #region Implementation of IValueConverter

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return (bool)value ? new GridLength(1, GridUnitType.Star) : new GridLength(0);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}