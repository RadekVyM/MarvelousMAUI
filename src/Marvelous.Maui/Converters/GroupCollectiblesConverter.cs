using System;
using System.Globalization;
using Marvelous.Core.Models;
using Marvelous.Maui.Models;

namespace Marvelous.Maui.Converters
{
    public class GroupCollectiblesConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var collectibles = value as IEnumerable<Collectible>;

            if (collectibles is null)
                return null;

            return collectibles
                .GroupBy(c => c.Wonder)
                .Select(g =>
                {
                    return new CollectibleGroupViewModel(g.FirstOrDefault()?.WonderName, g);
                });
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
