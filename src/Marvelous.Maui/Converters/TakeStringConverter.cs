using System.Globalization;

namespace Marvelous.Maui.Converters
{
    public class TakeStringConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (!int.TryParse(parameter.ToString(), out int count))
                return value;

            if (value is null || value?.ToString()?.Length < count)
                return "";

            return value?.ToString()?.Substring(0, count);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
