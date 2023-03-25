using Marvelous.Core.Interfaces.Services;
using Marvelous.Core.Interfaces.ViewModels;
using Marvelous.Core.Models;
using Marvelous.Core.ViewModels.Parameters;
using System.Windows.Input;

namespace Marvelous.Core.ViewModels
{
    public abstract class BaseWonderCollectiblePageViewModel : BasePageViewModel, IBaseWonderCollectiblePageViewModel
    {
        private Collectible collectible;

        protected readonly INavigationService navigationService;
        protected readonly ICollectibleService collectibleService;


        public Collectible Collectible
        {
            get => collectible;
            protected set
            {
                collectible = value;
                OnPropertyChanged(nameof(Collectible));
                OnPropertyChanged(nameof(Collectible.CollectibleState));
            }
        }

        public ICommand CollectibleCommand { get; private set; }


        public BaseWonderCollectiblePageViewModel(INavigationService navigationService, ICollectibleService collectibleService)
        {
            this.navigationService = navigationService;
            this.collectibleService = collectibleService;

            CollectibleCommand = new RelayCommand(() =>
            {
                collectibleService.UpdateCollectibleState(collectible, CollectibleState.Discovered);
                OnPropertyChanged(nameof(Collectible.CollectibleState));

                navigationService.GoTo(PageType.DiscoveredArtifactPage, new DiscoveredArtifactPageParameters(Collectible));
            });
        }
    }
}
