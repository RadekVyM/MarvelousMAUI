using Marvelous.Core;
using System.Globalization;

namespace Marvelous.Maui.Converters
{
    public class EmptyToSpacesLocalizationConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var str = Localization.ResourceManager.GetString(value.ToString());
            return string.Join(' ', (string.IsNullOrEmpty(str) ? value.ToString() : str).Split().Where(s => !string.IsNullOrWhiteSpace(s)));
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}

