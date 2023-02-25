using System.Globalization;

namespace Marvelous.Maui.Converters
{
    public class WithoutFirstConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is null)
                return null;

            var enumerable = value as IEnumerable<BaseShellItem>;
            var list = GetWithoutFirst(enumerable).ToList();

            return list;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value;
        }

        private IEnumerable<BaseShellItem> GetWithoutFirst(IEnumerable<BaseShellItem> enumerable)
        {
            bool isFirst = true;

            foreach (var e in enumerable)
            {
                if (isFirst)
                {
                    isFirst = false;
                    continue;
                }

                yield return e;
            }
        }
    }
}
