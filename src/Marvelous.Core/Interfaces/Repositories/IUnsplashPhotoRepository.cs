namespace Marvelous.Core.Interfaces.Repositories
{
    public interface IUnsplashPhotoRepository
    {
        IDictionary<string, IList<string>> GetPhotosByCollectionId();
    }
}
