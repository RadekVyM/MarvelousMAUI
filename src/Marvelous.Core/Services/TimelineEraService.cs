using Marvelous.Core.Interfaces.Repositories;
using Marvelous.Core.Interfaces.Services;
using Marvelous.Core.Models;

namespace Marvelous.Core.Services
{
    public class TimelineEraService : ITimelineEraService
    {
        private readonly ITimelineEraRepository timelineEraRepository;

        public TimelineEraService(ITimelineEraRepository timelineEraRepository)
        {
            this.timelineEraRepository = timelineEraRepository;
        }

        public IList<TimelineEra> GetTimelineEras()
        {
            return timelineEraRepository.GetTimelineEras();
        }

        public TimelineEra GetTimelineEra(int year)
        {
            return timelineEraRepository.GetTimelineEra(year);
        }
    }
}
