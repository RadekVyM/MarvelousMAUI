using System;
using System.Globalization;
using Marvelous.Core;

namespace Marvelous.Maui.Converters
{
    public class EmptyToSpacesLocalizationConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var str = Localization.ResourceManager.GetString(value.ToString());
            return string.Join(' ', (string.IsNullOrEmpty(str) ? value.ToString() : str).Split());
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}

