using System;
using System.Globalization;

namespace Marvelous.Maui.Converters
{
    public class NetMuseumImageConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var path = value.ToString();
            if (!path.StartsWith("http"))
                path = $"https://images.metmuseum.org/CRDImages/{value.ToString()}";
            return path;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}