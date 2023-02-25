using Marvelous.Core.Interfaces.Repositories;
using Marvelous.Core.Models;

namespace Marvelous.Data.Repositories
{
    public class TimelineEraRepository : ITimelineEraRepository
    {
        private static readonly TimelineEra prehistory = new TimelineEra
        {
            TitleKey = "eraPrehistory",
            StartYear = int.MinValue,
            EndYear = -600
        };

        private static readonly TimelineEra classical = new TimelineEra
        {
            TitleKey = "eraClassical",
            StartYear = -600,
            EndYear = 470
        };

        private static readonly TimelineEra earlyModern = new TimelineEra
        {
            TitleKey = "eraEarlyModern",
            StartYear = 470,
            EndYear = 1450
        };

        private static readonly TimelineEra modern = new TimelineEra
        {
            TitleKey = "eraModern",
            StartYear = 1450,
            EndYear = int.MaxValue
        };

        private static readonly IList<TimelineEra> timelineEras = new List<TimelineEra>
        {
            prehistory, classical, earlyModern, modern
        };

        public IList<TimelineEra> GetTimelineEras()
        {
            return timelineEras.ToList();
        }

        public TimelineEra GetTimelineEra(int year)
        {
            return timelineEras.LastOrDefault(e => e.StartYear < year);
        }
    }
}
