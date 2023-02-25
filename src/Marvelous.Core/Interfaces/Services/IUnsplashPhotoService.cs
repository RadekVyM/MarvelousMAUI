using Marvelous.Core.Models;

namespace Marvelous.Core.Interfaces.Services
{
    public interface IUnsplashPhotoService
    {
        IDictionary<string, IList<string>> GetPhotosByCollectionId();
        IList<UnsplashPhoto> GetUnsplashPhotos();
        IList<UnsplashPhoto> GetUnsplashPhotosOfWonder(WonderType wonderType);
    }
}
