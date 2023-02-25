using Marvelous.Core.Interfaces.ViewModels;

namespace Marvelous.Core.Interfaces.Services
{
    public enum PageType
    {
        MainMenuPage,
        WonderMainPage,
        WonderPhotoPage,
        WonderArtifactsPage,
        WonderHistoryPage,
        GlobalTimelinePage,
        UnsplashPhotoDetailPage,
        ArtifactPage,
        ArtifactsPage,
        CollectionPage,
        DiscoveredArtifactPage
    }

    public interface INavigationService
    {
        void GoTo(PageType pageType, IParameters parameters = null);
        void GoBack();
    }
}
