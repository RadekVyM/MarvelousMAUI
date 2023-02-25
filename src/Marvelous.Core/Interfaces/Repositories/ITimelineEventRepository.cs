using Marvelous.Core.Models;

namespace Marvelous.Core.Interfaces.Repositories
{
    public interface ITimelineEventRepository
    {
        IList<TimelineEvent> GetGlobalEvents();
    }
}
