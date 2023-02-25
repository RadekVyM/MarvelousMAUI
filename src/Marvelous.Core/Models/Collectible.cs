namespace Marvelous.Core.Models
{
    public enum CollectibleState
    {
        Lost = 0,
        Discovered = 1,
        Explored = 2
    }

    public enum CollectibleIconType
    {
        Jewelry, Picture, Statue, Textile, Vase, Scroll
    }

    public class Collectible
    {
        public string Id => ArtifactId;
        public string Title { get; init; }
        public string ImageUrl { get; init; }
        public string ImageUrlSmall { get; init; }
        public CollectibleIconType IconType { get; init; }
        public string ArtifactId { get; init; }
        public WonderType Wonder { get; init; }
        public string WonderName { get; set; }
        public string Subtitle { get; set; }
        public CollectibleState CollectibleState { get; set; } = CollectibleState.Lost;
        public DateTime DiscoveredTime { get; set; } = DateTime.MinValue;
    }
}
