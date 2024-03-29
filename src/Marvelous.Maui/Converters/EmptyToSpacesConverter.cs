﻿using System.Globalization;

namespace Marvelous.Maui.Converters
{
    public class EmptyToSpacesConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return string.Join(' ', value.ToString().Split().Where(s => !string.IsNullOrWhiteSpace(s)));
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
