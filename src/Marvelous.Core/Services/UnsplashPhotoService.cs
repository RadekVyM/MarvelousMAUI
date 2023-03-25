using Marvelous.Core.Interfaces.Repositories;
using Marvelous.Core.Interfaces.Services;
using Marvelous.Core.Models;

namespace Marvelous.Core.Services
{
    public class UnsplashPhotoService : IUnsplashPhotoService
    {
        private readonly IUnsplashPhotoRepository unsplashPhotoRepository;
        private readonly IWonderRepository wonderRepository;

        public UnsplashPhotoService(IUnsplashPhotoRepository unsplashPhotoRepository, IWonderRepository wonderRepository)
        {
            this.unsplashPhotoRepository = unsplashPhotoRepository;
            this.wonderRepository = wonderRepository;
        }

        public IDictionary<string, IList<string>> GetPhotosByCollectionId()
        {
            return unsplashPhotoRepository.GetPhotosByCollectionId();
        }

        public IList<UnsplashPhoto> GetUnsplashPhotos()
        {
            return GetPhotosByCollectionId()
                .SelectMany(kvp => kvp.Value.Select(url => new UnsplashPhoto { Id = kvp.Key, Url = url }))
                .ToList();
        }

        public IList<UnsplashPhoto> GetUnsplashPhotosOfWonder(WonderType wonderType)
        {
            var wonder = wonderRepository.GetWonder(wonderType);

            return unsplashPhotoRepository
                .GetPhotosByCollectionId()[wonder.UnsplashCollectionId]
                .Select(url => new UnsplashPhoto { Id = wonder.UnsplashCollectionId, Url = url })
                .ToList();
        }
    }
}
