using System.Globalization;
using Marvelous.Core;

namespace Marvelous.Maui.Converters
{
    public class TimelineDateConverter : IMultiValueConverter
    {
        private readonly YearToCeConverter yearToCeConverter = new YearToCeConverter();

        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            var first = values.Length > 0 ? values[0] : null;
            var second = values.Length > 1 ? values[1] : null;

            return string.Format(
                Localization.timelineSemanticDate,
                $"{first} {yearToCeConverter.Convert(first, targetType, parameter, culture)}".Replace("-", ""),
                $"{second} {yearToCeConverter.Convert(second, targetType, parameter, culture)}".Replace("-", ""));
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
