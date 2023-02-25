using System.Globalization;
using Marvelous.Core.Models;
using Marvelous.Maui.Models;

namespace Marvelous.Maui.Converters
{
    public class ArtifactCarouselItemsConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var list = new List<ArtifactCarouselItemViewModel>();

            if (value is IEnumerable<Artifact> artifacts)
            {
                int position = 0;

                foreach (var artifact in artifacts)
                {
                    list.Add(new ArtifactCarouselItemViewModel
                    {
                        Artifact = artifact,
                        Position = position
                    });

                    position++;
                }
            }

            return list;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
