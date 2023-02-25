using System.Globalization;
using Marvelous.Core;
using Marvelous.Core.Models;
using Marvelous.Maui.Models;

namespace Marvelous.Maui.Converters
{
    public class WonderToListViewModelsConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is not Wonder wonder)
                return new List<WonderSectionViewModel>();

            return new List<WonderSectionViewModel>
            {
                new FactsHistoryWonderSectionViewModel
                {
                    Title = Localization.appBarTitleFactsHistory,
                    HistoryInfo1 = wonder.HistoryInfo1,
                    HistoryInfo2 = wonder.HistoryInfo2,
                    Callout1 = wonder.Callout1,
                    PullQuote1Top = wonder.PullQuote1Top,
                    PullQuote1Bottom = wonder.PullQuote1Bottom,
                },
                new ConstructionWonderSectionViewModel
                {
                    Title = Localization.appBarTitleConstruction,
                    ConstructionInfo1 = wonder.ConstructionInfo1,
                    ConstructionInfo2 = wonder.ConstructionInfo2,
                    Callout2 = wonder.Callout2,
                    VideoCaption = wonder.VideoCaption,
                    VideoId = wonder.VideoId,
                },
                new LocationWonderSectionViewModel
                {
                    Title = Localization.appBarTitleLocation,
                    LocationInfo1 = wonder.LocationInfo1,
                    LocationInfo2 = wonder.LocationInfo2,
                    MapCaption = wonder.MapCaption,
                    PullQuote2 = wonder.PullQuote2,
                    PullQuote2Author = wonder.PullQuote2Author,
                    Lat = wonder.Lat,
                    Lng = wonder.Lng,
                }
            };
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
