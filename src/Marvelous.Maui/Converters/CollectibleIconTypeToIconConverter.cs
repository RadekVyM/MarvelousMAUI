using System.Globalization;
using Marvelous.Core.Models;

namespace Marvelous.Maui.Converters
{
    public class CollectibleIconTypeToIconConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var type = (CollectibleIconType)value;

            return type switch
            {
                CollectibleIconType.Jewelry => "collectibles_jewelry.png",
                CollectibleIconType.Picture => "collectibles_picture.png",
                CollectibleIconType.Scroll => "collectibles_scroll.png",
                CollectibleIconType.Statue => "collectibles_statue.png",
                CollectibleIconType.Textile => "collectibles_textile.png",
                CollectibleIconType.Vase => "collectibles_vase.png",
                _ => ""
            };
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
