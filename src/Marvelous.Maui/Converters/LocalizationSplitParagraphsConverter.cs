using System.Globalization;

namespace Marvelous.Maui.Converters
{
    public class LocalizationSplitParagraphsConverter : IValueConverter
    {
        private readonly LocalizationConverter localizationConverter = new LocalizationConverter();

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var text = localizationConverter.Convert(value, targetType, parameter, culture);
            return text?.ToString().Split("\n");
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
