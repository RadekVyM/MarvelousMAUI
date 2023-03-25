using Marvelous.Core.Interfaces.Services;
using Marvelous.Core.Interfaces.ViewModels;
using Marvelous.Core.Models;
using Marvelous.Core.ViewModels.Parameters;
using System.Windows.Input;

namespace Marvelous.Core.ViewModels
{
    public class DiscoveredArtifactPageViewModel : BasePageViewModel, IDiscoveredArtifactPageViewModel
    {
        private Collectible collectible;


        public Collectible Collectible
        {
            get => collectible;
            protected set
            {
                collectible = value;
                OnPropertyChanged(nameof(Collectible));
            }
        }

        public ICommand CollectionCommand { get; private set; }


        public DiscoveredArtifactPageViewModel(INavigationService navigationService)
        {

            CollectionCommand = new RelayCommand(() =>
            {
                navigationService.GoTo(PageType.CollectionPage);
            });
        }


        public override void OnApplyFirstParameters(IParameters parameters)
        {
            base.OnApplyFirstParameters(parameters);

            if (parameters is not DiscoveredArtifactPageParameters discoveredArtifactParameters)
                return;

            Collectible = discoveredArtifactParameters.Collectible;
        }
    }
}
