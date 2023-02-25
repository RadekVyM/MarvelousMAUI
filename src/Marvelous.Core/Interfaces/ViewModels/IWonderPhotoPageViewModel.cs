using System.Windows.Input;
using Marvelous.Core.Models;

namespace Marvelous.Core.Interfaces.ViewModels
{
    public interface IWonderPhotoPageViewModel : IBaseWonderCollectiblePageViewModel
    {
        IList<UnsplashPhoto> UnsplashPhotos { get; }
        WonderType CurrentWonder { get; }
        ICommand UnsplashPhotoDetailCommand { get; }
    }
}
