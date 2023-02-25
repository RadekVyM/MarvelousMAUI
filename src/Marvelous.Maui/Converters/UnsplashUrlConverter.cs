using System.Globalization;
using Marvelous.Core.Models;

namespace Marvelous.Maui.Converters
{
    public class UnsplashUrlConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var url = value as string;
            var wholeUrl = UnsplashPhoto.GetSelfHostedUrl(url, UnsplashPhotoSize.Med, 1);

            return wholeUrl;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
