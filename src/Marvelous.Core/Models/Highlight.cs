namespace Marvelous.Core.Models
{
    public class Highlight
    {
        public string Title { get; init; }
        public string ImageUrl { get; init; }
        public string ImageUrlSmall { get; init; }
        public string Culture { get; init; }
        public string Date { get; init; }
        public string ArtifactId { get; init; }
        public WonderType Wonder { get; init; }
    }
}
