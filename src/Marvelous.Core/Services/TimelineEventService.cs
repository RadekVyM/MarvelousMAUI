using Marvelous.Core.Interfaces.Repositories;
using Marvelous.Core.Interfaces.Services;
using Marvelous.Core.Models;

namespace Marvelous.Core.Services
{
    public class TimelineEventService : ITimelineEventService
    {
        private readonly ITimelineEventRepository timelineEventRepository;
        private readonly IWonderRepository wonderRepository;

        public TimelineEventService(ITimelineEventRepository timelineEventRepository, IWonderRepository wonderRepository)
        {
            this.timelineEventRepository = timelineEventRepository;
            this.wonderRepository = wonderRepository;
        }

        public IList<TimelineEvent> GetGlobalTimelineEvents()
        {
            return timelineEventRepository.GetGlobalEvents().ToList();
        }

        public IList<TimelineEvent> GetGlobalTimelineEventsWithWonderBeginnigs()
        {
            var wonders = wonderRepository.GetWonders();

            return timelineEventRepository
                .GetGlobalEvents()
                .Concat(wonders.Select(w => new TimelineEvent
                {
                    Year = w.StartYr,
                    DescriptionKey = string.Format(Localization.timelineLabelConstruction, Localization.ResourceManager.GetString(w.Title))
                }))
                .OrderBy(w => w.Year)
                .ToList();
        }

        public IList<TimelineEvent> GetTimelineEventsForWonder(WonderType wonderType)
        {
            var wonder = wonderRepository.GetWonder(wonderType);

            return wonder.Events.Select(e => new TimelineEvent { Year = e.Key, DescriptionKey = e.Value }).ToList();
        }
    }
}
