using System.Globalization;
using Marvelous.Core.Models;

namespace Marvelous.Maui.Converters
{
    public class WonderMainPagePhotoConverter: IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is not WonderType wonder)
                return "";

            var photoName = parameter?.ToString();

            var prefix = wonder switch
            {
                WonderType.ChichenItza => "chichen_itza",
                WonderType.Colosseum => "colosseum",
                WonderType.MachuPicchu => "machu_picchu",
                WonderType.GreatWall => "great_wall_of_china",
                WonderType.ChristRedeemer => "christ_the_redeemer",
                WonderType.PyramidsGiza => "pyramids",
                WonderType.TajMahal => "taj_mahal",
                WonderType.Petra => "petra",
                _ => ""
            };

            return $"{prefix}_{photoName}";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
