using System.Globalization;
using Marvelous.Core.Models;

namespace Marvelous.Maui.Converters
{
    public class CollectibleStateToIsVisibleConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var state = (CollectibleState)value;

            return state switch
            {
                CollectibleState.Lost => false,
                CollectibleState.Discovered => true,
                CollectibleState.Explored => true,
                _ => false
            };
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
