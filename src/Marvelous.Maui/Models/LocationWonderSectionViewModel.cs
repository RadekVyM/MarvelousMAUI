namespace Marvelous.Maui.Models
{
    public class LocationWonderSectionViewModel : WonderSectionViewModel
    {
        public string LocationInfo1 { get; init; }
        public string LocationInfo2 { get; init; }
        public string PullQuote2 { get; init; }
        public string PullQuote2Author { get; init; }
        public string MapCaption { get; init; }
        public string MapImage { get; init; }
        public double Lat { get; init; }
        public double Lng { get; init; }
        public IDictionary<string, object> LatLngParameters { get; set; } = new Dictionary<string, object>();
    }
}
