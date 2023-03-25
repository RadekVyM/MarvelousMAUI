using Marvelous.Core.Interfaces.Services;
using Marvelous.Core.Interfaces.ViewModels;
using Marvelous.Core.Models;
using Marvelous.Core.ViewModels.Parameters;
using System.Windows.Input;

namespace Marvelous.Core.ViewModels
{
    public class MainViewModel : BaseViewModel, IMainViewModel
    {
        private Wonder currentWonder;
        private IList<Wonder> wonders;

        private readonly IWonderService wonderService;
        private readonly INavigationService navigationService;

        public Wonder CurrentWonder
        {
            get => currentWonder;
            set
            {
                currentWonder = value;
                OnPropertyChanged(nameof(CurrentWonder));
            }
        }

        public IList<Wonder> Wonders
        {
            get => wonders;
            set
            {
                wonders = value;
                OnPropertyChanged(nameof(Wonders));
            }
        }

        public ICommand GlobalTimelineCommand { get; init; }
        public ICommand CollectionCommand { get; init; }

        public MainViewModel(IWonderService wonderService, INavigationService navigationService)
        {
            this.wonderService = wonderService;
            this.navigationService = navigationService;
            Wonders = wonderService.GetWonders();
            CurrentWonder = Wonders.FirstOrDefault();

            GlobalTimelineCommand = new RelayCommand(() =>
            {
                navigationService.GoTo(PageType.GlobalTimelinePage, new WonderPageParameters(CurrentWonder.Type));
            });
            CollectionCommand = new RelayCommand(() =>
            {
                navigationService.GoTo(PageType.CollectionPage);
            });
        }
    }
}
