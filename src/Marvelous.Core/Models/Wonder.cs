namespace Marvelous.Core.Models
{
    public class Wonder
    {
        public WonderType Type { get; init; }
        public string Title { get; init; }
        public string SubTitle { get; init; }
        public string RegionTitle { get; init; }
        public string HistoryInfo1 { get; init; }
        public string HistoryInfo2 { get; init; }
        public string ConstructionInfo1 { get; init; }
        public string ConstructionInfo2 { get; init; }
        public string LocationInfo1 { get; init; }
        public string LocationInfo2 { get; init; }
        public string PullQuote1Top { get; init; }
        public string PullQuote1Bottom { get; init; }
        public string PullQuote1Author { get; init; }
        public string PullQuote2 { get; init; }
        public string PullQuote2Author { get; init; }
        public string Callout1 { get; init; }
        public string Callout2 { get; init; }
        public string UnsplashCollectionId { get; init; }
        public string VideoId { get; init; }
        public string VideoCaption { get; init; }
        public string MapCaption { get; init; }
        public IList<string> ImageIds { get; init; }
        public IList<string> Facts { get; init; }
        public int StartYr { get; init; }
        public int EndYr { get; init; }
        public int ArtifactStartYr { get; init; }
        public int ArtifactEndYr { get; init; }
        public string ArtifactCulture { get; init; }
        public string ArtifactGeolocation { get; init; }
        public double Lat { get; init; }
        public double Lng { get; init; }
        public IList<string> HighlightArtifacts { get; init; } // IDs used to assemble HighlightsData, should not be used directly
        public IList<string> HiddenArtifacts { get; init; } // IDs used to assemble CollectibleData, should not be used directly
        public IDictionary<int, string> Events { get; init; }
        public IList<Search> SearchData { get; set; } = new List<Search>();
        public IList<string> SearchSuggestions { get; set; } = new List<string>();

        //public string TitleWithBreaks => Title.Remove(Title.IndexOf(" "), 1)(' ', '\n');

        IList<object> Props => new List<object> { Type, Title, HistoryInfo1, ImageIds, Facts };
    }
}
