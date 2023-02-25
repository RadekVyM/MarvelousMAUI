using Marvelous.Core.Models;

namespace Marvelous.Core.Interfaces.Services
{
    public interface ITimelineEraService
    {
        TimelineEra GetTimelineEra(int year);
        IList<TimelineEra> GetTimelineEras();
    }
}
