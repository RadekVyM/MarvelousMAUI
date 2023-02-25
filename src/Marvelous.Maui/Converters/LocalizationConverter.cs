using System.Globalization;
using Marvelous.Core;

namespace Marvelous.Maui.Converters
{
    public class LocalizationConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var str = Localization.ResourceManager.GetString(value.ToString());

            return string.IsNullOrEmpty(str) ? value.ToString() : str;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return null;
        }
    }
}
