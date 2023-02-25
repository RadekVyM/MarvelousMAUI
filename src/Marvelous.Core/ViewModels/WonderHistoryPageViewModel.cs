using System.Windows.Input;
using Marvelous.Core.Interfaces.Services;
using Marvelous.Core.Interfaces.ViewModels;
using Marvelous.Core.Models;
using Marvelous.Core.ViewModels.Parameters;

namespace Marvelous.Core.ViewModels
{
    public class WonderHistoryPageViewModel : BaseWonderCollectiblePageViewModel, IWonderHistoryPageViewModel
    {
        private IList<TimelineEvent> timelineEvents;
        private TimelineEra timelineEra;
        private Wonder currentWonder;
        private IList<Wonder> wonders;

        private readonly ITimelineEraService timelineEraService;
        private readonly ITimelineEventService timelineEventService;
        private readonly IWonderService wonderService;


        public IList<TimelineEvent> TimelineEvents
        {
            get => timelineEvents;
            private set
            {
                timelineEvents = value;
                OnPropertyChanged(nameof(TimelineEvents));
            }
        }

        public TimelineEra TimelineEra
        {
            get => timelineEra;
            private set
            {
                timelineEra = value;
                OnPropertyChanged(nameof(TimelineEra));
            }
        }

        public Wonder CurrentWonder
        {
            get => currentWonder;
            private set
            {
                currentWonder = value;
                OnPropertyChanged(nameof(CurrentWonder));
            }
        }

        public IList<Wonder> Wonders
        {
            get => wonders;
            private set
            {
                wonders = value;
                OnPropertyChanged(nameof(Wonders));
            }
        }

        public ICommand GlobalTimelineCommand { get; init; }


        public WonderHistoryPageViewModel(
            ITimelineEraService timelineEraService,
            ITimelineEventService timelineEventService,
            IWonderService wonderService,
            ICollectibleService collectibleService,
            INavigationService navigationService) : base(navigationService, collectibleService)
        {
            this.timelineEraService = timelineEraService;
            this.timelineEventService = timelineEventService;
            this.wonderService = wonderService;

            GlobalTimelineCommand = new RelayCommand(() =>
            {
                navigationService.GoTo(PageType.GlobalTimelinePage, new WonderPageParameters(CurrentWonder.Type));
            });
        }


        public override void OnApplyFirstParameters(IParameters parameters)
        {
            base.OnApplyFirstParameters(parameters);

            if (parameters is not WonderPageParameters wonderParameters)
                return;

            CurrentWonder = wonderService.GetWonder(wonderParameters.Wonder);
            Wonders = wonderService.GetWonders();
            TimelineEvents = timelineEventService.GetTimelineEventsForWonder(wonderParameters.Wonder);
            TimelineEra = timelineEraService.GetTimelineEra(CurrentWonder.StartYr);
            Collectible = collectibleService.GetCollectiblesForWonder(wonderParameters.Wonder)[2];
        }
    }
}
