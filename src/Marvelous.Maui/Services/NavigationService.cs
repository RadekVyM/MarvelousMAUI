using Marvelous.Core.Interfaces.Services;
using Marvelous.Core.Interfaces.ViewModels;

namespace Marvelous.Maui.Services
{
    public class NavigationService : INavigationService
    {
        private IList<PageType> rootPages = new List<PageType> { PageType.MainMenuPage, PageType.WonderMainPage, PageType.WonderPhotoPage, PageType.WonderArtifactsPage, PageType.WonderHistoryPage };

        public NavigationService()
        {
        }

        public void GoBack()
        {
            Shell.Current.GoToAsync("..");
        }

        public async void GoTo(PageType pageType, IParameters parameters = null)
        {
            await Shell.Current.GoToAsync(GetPageRoute(pageType), true, new Dictionary<string, object>
            { 
                ["Parameters"] = parameters
            });
        }

        private string GetPageRoute(PageType pageType)
        {
            if (rootPages.Contains(pageType))
                return $"///{pageType.ToString()}";
            return pageType.ToString();
        }
    }
}
