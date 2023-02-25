using System.Globalization;
using Marvelous.Core;

namespace Marvelous.Maui.Converters
{
    public class YearToCeConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is null)
                return "";

            return (int)value < 0 ? Localization.yearBCE : Localization.yearCE;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return null;
        }
    }
}
