using Marvelous.Core.Models;
using System.Windows.Input;

namespace Marvelous.Core.Interfaces.ViewModels
{
    public interface IWonderPhotoPageViewModel : IBaseWonderCollectiblePageViewModel
    {
        IList<UnsplashPhoto> UnsplashPhotos { get; }
        WonderType CurrentWonder { get; }
        ICommand UnsplashPhotoDetailCommand { get; }
    }
}
