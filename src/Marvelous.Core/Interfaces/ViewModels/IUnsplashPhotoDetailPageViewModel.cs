using Marvelous.Core.Models;

namespace Marvelous.Core.Interfaces.ViewModels
{
    public interface IUnsplashPhotoDetailPageViewModel : IBasePageViewModel
    {
        IList<UnsplashPhoto> UnsplashPhotos { get; set; }
        UnsplashPhoto CurrentUnsplashPhoto { get; set; }
        int CurrentUnsplashPhotoIndex { get; set; }
        WonderType CurrentWonder { get; set; }
    }
}
