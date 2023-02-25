using Marvelous.Core.Interfaces.Services;
using Marvelous.Core.Interfaces.ViewModels;
using Marvelous.Core.Models;
using Marvelous.Core.ViewModels.Parameters;

namespace Marvelous.Core.ViewModels
{
    public class GlobalTimelinePageViewModel : BasePageViewModel, IGlobalTimelinePageViewModel
    {
        private IList<TimelineEvent> timelineEvents;
        private IList<Wonder> wonders;
        private WonderType currentWonderType;

        private readonly ITimelineEventService timelineEventService;
        private readonly ITimelineEraService timelineEraService;
        private readonly IWonderService wonderService;

        public IList<TimelineEvent> TimelineEvents
        {
            get => timelineEvents;
            set
            {
                timelineEvents = value;
                OnPropertyChanged(nameof(TimelineEvents));
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

        public WonderType CurrentWonderType
        {
            get => currentWonderType;
            set
            {
                currentWonderType = value;
                OnPropertyChanged(nameof(CurrentWonderType));
            }
        }


        public GlobalTimelinePageViewModel(ITimelineEventService timelineEventService, ITimelineEraService timelineEraService, IWonderService wonderService)
        {
            this.timelineEventService = timelineEventService;
            this.timelineEraService = timelineEraService;
            this.wonderService = wonderService;
        }


        public override void OnApplyParameters(IParameters parameters)
        {
            base.OnApplyParameters(parameters);

            TimelineEvents ??= timelineEventService.GetGlobalTimelineEventsWithWonderBeginnigs();
            Wonders = wonderService.GetWonders();

            var wonderParameters = parameters as WonderPageParameters;
            if (wonderParameters is null)
                return;

            CurrentWonderType = wonderParameters.Wonder;
        }

        public TimelineEra GetTimelineEra(int year)
        {
            return timelineEraService.GetTimelineEra(year);
        }
    }
}
