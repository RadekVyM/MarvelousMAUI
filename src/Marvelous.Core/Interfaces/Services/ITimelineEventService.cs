using Marvelous.Core.Models;

namespace Marvelous.Core.Interfaces.Services
{
    public interface ITimelineEventService
    {
        IList<TimelineEvent> GetGlobalTimelineEvents();
        IList<TimelineEvent> GetGlobalTimelineEventsWithWonderBeginnigs();
        IList<TimelineEvent> GetTimelineEventsForWonder(WonderType wonderType);
    }
}
