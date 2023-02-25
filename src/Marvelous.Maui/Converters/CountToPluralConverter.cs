using System;
using System.Globalization;

namespace Marvelous.Maui.Converters
{
    public class CountToPluralConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var number = double.Parse(value.ToString());
            return Math.Abs(number) != 1 ? "s" : "";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}

