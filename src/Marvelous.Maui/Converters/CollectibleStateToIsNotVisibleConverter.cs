using System.Globalization;
using Marvelous.Core.Models;

namespace Marvelous.Maui.Converters
{
    public class CollectibleStateToIsNotVisibleConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var state = (CollectibleState)value;

            return state switch
            {
                CollectibleState.Lost => true,
                CollectibleState.Discovered => false,
                CollectibleState.Explored => false,
                _ => true
            };
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
