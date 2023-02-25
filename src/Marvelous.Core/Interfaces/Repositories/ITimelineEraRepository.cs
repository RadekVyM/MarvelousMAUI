using Marvelous.Core.Models;

namespace Marvelous.Core.Interfaces.Repositories
{
    public interface ITimelineEraRepository
    {
        TimelineEra GetTimelineEra(int year);
        IList<TimelineEra> GetTimelineEras();
    }
}
