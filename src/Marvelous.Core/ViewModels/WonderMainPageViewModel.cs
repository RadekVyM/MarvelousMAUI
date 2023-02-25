using System.Windows.Input;
using Marvelous.Core.Interfaces.Services;
using Marvelous.Core.Interfaces.ViewModels;
using Marvelous.Core.Models;
using Marvelous.Core.ViewModels.Parameters;

namespace Marvelous.Core.ViewModels
{
    public class WonderMainPageViewModel : BaseWonderCollectiblePageViewModel, IWonderMainPageViewModel
    {
        private Wonder currentWonder;
        private Collectible collectible;

        private readonly IWonderService wonderService;


        public Wonder CurrentWonder
        {
            get => currentWonder;
            private set
            {
                currentWonder = value;
                OnPropertyChanged(nameof(CurrentWonder));
            }
        }

        public ICommand PlayVideoCommand { get; init; }


        public WonderMainPageViewModel(
            IWonderService wonderService,
            ICollectibleService collectibleService,
            INavigationService navigationService,
            IBrowser browser) : base(navigationService, collectibleService)
        {
            this.wonderService = wonderService;

            PlayVideoCommand = new RelayCommand(async parameter =>
            {
                await browser.GoToYouTubeAsync(parameter?.ToString());
            });
        }


        public override void OnApplyFirstParameters(IParameters parameters)
        {
            base.OnApplyFirstParameters(parameters);

            if (parameters is not WonderPageParameters wonderParameters)
                return;

            CurrentWonder = wonderService.GetWonder(wonderParameters.Wonder);
            Collectible = collectibleService.GetCollectiblesForWonder(wonderParameters.Wonder)[0];
        }
    }
}
