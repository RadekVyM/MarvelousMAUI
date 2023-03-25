using Marvelous.Core.Interfaces.Services;
using Marvelous.Core.Interfaces.ViewModels;
using Marvelous.Core.Models;
using Marvelous.Core.ViewModels.Parameters;
using System.Windows.Input;

namespace Marvelous.Core.ViewModels
{
    public class WonderPhotoPageViewModel : BaseWonderCollectiblePageViewModel, IWonderPhotoPageViewModel
    {
        private readonly IUnsplashPhotoService unsplashPhotoService;
        private IList<UnsplashPhoto> unsplashPhotos;
        private WonderType currentWonder;


        public IList<UnsplashPhoto> UnsplashPhotos
        {
            get => unsplashPhotos;
            private set
            {
                unsplashPhotos = value;
                OnPropertyChanged(nameof(UnsplashPhotos));
            }
        }

        public WonderType CurrentWonder
        {
            get => currentWonder;
            private set
            {
                currentWonder = value;
                OnPropertyChanged(nameof(CurrentWonder));
            }
        }

        public ICommand UnsplashPhotoDetailCommand { get; init; }


        public WonderPhotoPageViewModel(
            IUnsplashPhotoService unsplashPhotoService,
            ICollectibleService collectibleService,
            INavigationService navigationService) : base(navigationService, collectibleService)
        {
            this.unsplashPhotoService = unsplashPhotoService;

            UnsplashPhotoDetailCommand = new RelayCommand(parameter =>
            {
                if (parameter is not string url)
                    return;

                navigationService.GoTo(PageType.UnsplashPhotoDetailPage, new UnsplashPhotoDetailPageParameters(CurrentWonder, url));
            });
        }


        public override void OnApplyFirstParameters(IParameters parameters)
        {
            base.OnApplyFirstParameters(parameters);

            if (parameters is not WonderPageParameters wonderParameters)
                return;

            CurrentWonder = wonderParameters.Wonder;
            UnsplashPhotos = unsplashPhotoService.GetUnsplashPhotosOfWonder(wonderParameters.Wonder);
            Collectible = collectibleService.GetCollectiblesForWonder(wonderParameters.Wonder)[1];
        }
    }
}
