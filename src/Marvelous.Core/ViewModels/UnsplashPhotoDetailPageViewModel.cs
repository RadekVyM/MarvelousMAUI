using Marvelous.Core.Interfaces.Services;
using Marvelous.Core.Interfaces.ViewModels;
using Marvelous.Core.Models;
using Marvelous.Core.ViewModels.Parameters;

namespace Marvelous.Core.ViewModels
{
    public class UnsplashPhotoDetailPageViewModel : BasePageViewModel, IUnsplashPhotoDetailPageViewModel
    {
        private readonly IUnsplashPhotoService unsplashPhotoService;

        private IList<UnsplashPhoto> unsplashPhotos;
        private WonderType currentWonder;
        private UnsplashPhoto currentUnsplashPhoto;
        private int currentUnsplashPhotoIndex;


        public IList<UnsplashPhoto> UnsplashPhotos
        {
            get => unsplashPhotos;
            set
            {
                unsplashPhotos = value;
                OnPropertyChanged(nameof(UnsplashPhotos));
            }
        }

        public UnsplashPhoto CurrentUnsplashPhoto
        {
            get => currentUnsplashPhoto;
            set
            {
                currentUnsplashPhoto = value;
                currentUnsplashPhotoIndex = currentUnsplashPhoto is null ? 0 : UnsplashPhotos.IndexOf(value);
                OnPropertyChanged(nameof(CurrentUnsplashPhoto));
                OnPropertyChanged(nameof(CurrentUnsplashPhotoIndex));
            }
        }

        public int CurrentUnsplashPhotoIndex
        {
            get => currentUnsplashPhotoIndex;
            set
            {
                currentUnsplashPhotoIndex = value;
                currentUnsplashPhoto = UnsplashPhotos.ElementAt(currentUnsplashPhotoIndex);
                OnPropertyChanged(nameof(CurrentUnsplashPhotoIndex));
            }
        }

        public WonderType CurrentWonder
        {
            get => currentWonder;
            set
            {
                currentWonder = value;
                OnPropertyChanged(nameof(CurrentWonder));
            }
        }


        public UnsplashPhotoDetailPageViewModel(IUnsplashPhotoService unsplashPhotoService)
        {
            this.unsplashPhotoService = unsplashPhotoService;
        }


        public override void OnApplyParameters(IParameters parameters)
        {
            base.OnApplyParameters(parameters);

            if (parameters is not UnsplashPhotoDetailPageParameters unsplashPhotoDetailPageParameters)
                return;

            CurrentWonder = unsplashPhotoDetailPageParameters.Wonder;
            UnsplashPhotos = unsplashPhotoService.GetUnsplashPhotosOfWonder(unsplashPhotoDetailPageParameters.Wonder);
            CurrentUnsplashPhoto = UnsplashPhotos.FirstOrDefault(up => up.Url == unsplashPhotoDetailPageParameters.CurrentImageUrl);
        }
    }
}
