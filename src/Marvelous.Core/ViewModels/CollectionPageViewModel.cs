using Marvelous.Core.Interfaces.Services;
using Marvelous.Core.Interfaces.ViewModels;
using Marvelous.Core.Models;
using Marvelous.Core.ViewModels.Parameters;
using System.Windows.Input;

namespace Marvelous.Core.ViewModels
{
    public class CollectionPageViewModel : BasePageViewModel, ICollectionPageViewModel
    {
        private readonly ICollectibleService collectibleService;
        private IList<Collectible> collectibles;

        public IList<Collectible> Collectibles
        {
            get => collectibles;
            private set
            {
                collectibles = value;
                OnPropertyChanged(nameof(Collectibles));
                OnPropertyChanged(nameof(CollectiblesCount));
                OnPropertyChanged(nameof(DiscoveredCollectiblesCount));
                OnPropertyChanged(nameof(DiscoveredCollectiblesPercentage));
                OnPropertyChanged(nameof(DiscoveredAndNotExploredCollectiblesCount));
            }
        }

        public Collectible LastDiscoveredCollectible => collectibleService.GetLastDiscoveredCollectible();

        public int DiscoveredAndNotExploredCollectiblesCount => Collectibles?.Count(c => c.CollectibleState == CollectibleState.Discovered) ?? 0;
        public int DiscoveredCollectiblesPercentage => (int)Math.Round(((double)DiscoveredCollectiblesCount / CollectiblesCount) * 100d);
        public int DiscoveredCollectiblesCount => Collectibles?.Count(c => c.CollectibleState != CollectibleState.Lost) ?? 0;
        public int CollectiblesCount => Collectibles?.Count ?? 1;

        public ICommand ResetCollectionCommand { get; private set; }
        public ICommand ArtifactCommand { get; private set; }


        public CollectionPageViewModel(ICollectibleService collectibleService, INavigationService navigationService)
        {
            this.collectibleService = collectibleService;

            ResetCollectionCommand = new RelayCommand(() =>
            {
                collectibleService.ResetAllCollectiblesToLost();
                Collectibles = collectibleService.GetCollectibles();
            });
            ArtifactCommand = new RelayCommand((parameter) =>
            {
                var collectible = parameter as Collectible;

                collectibleService.UpdateCollectibleState(collectible, CollectibleState.Explored);
                navigationService.GoTo(PageType.ArtifactPage, new ArtifactPageParameters(collectible.Wonder, collectible.ArtifactId));
            });
        }


        public override void OnApplyFirstParameters(IParameters parameters)
        {
            base.OnApplyFirstParameters(parameters);

            Collectibles = collectibleService.GetCollectibles();
        }
    }
}
