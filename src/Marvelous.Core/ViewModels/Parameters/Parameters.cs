using Marvelous.Core.Interfaces.ViewModels;
using Marvelous.Core.Models;

namespace Marvelous.Core.ViewModels.Parameters
{
    public record WonderPageParameters(WonderType Wonder) : IParameters;
    public record UnsplashPhotoDetailPageParameters(WonderType Wonder, string CurrentImageUrl) : IParameters;
    public record ArtifactPageParameters(WonderType Wonder, string ArtifactId) : IParameters;
    public record DiscoveredArtifactPageParameters(Collectible Collectible) : IParameters;
}
