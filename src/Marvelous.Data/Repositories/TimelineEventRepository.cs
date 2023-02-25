using Marvelous.Core.Interfaces.Repositories;
using Marvelous.Core.Models;

namespace Marvelous.Data.Repositories
{
    public class TimelineEventRepository : ITimelineEventRepository
    {
        private static readonly IList<TimelineEvent> globalEvents = new List<TimelineEvent>
        {
            new TimelineEvent { Year = -2900, DescriptionKey = "timelineEvent2900bce" },
            new TimelineEvent { Year = -2700, DescriptionKey = "timelineEvent2700bce" },
            new TimelineEvent { Year = -2600, DescriptionKey = "timelineEvent2600bce" },
            new TimelineEvent { Year = -2560, DescriptionKey = "timelineEvent2560bce" },
            new TimelineEvent { Year = -2500, DescriptionKey = "timelineEvent2500bce" },
            new TimelineEvent { Year = -2200, DescriptionKey = "timelineEvent2200bce" },
            new TimelineEvent { Year = -2000, DescriptionKey = "timelineEvent2000bce" },
            new TimelineEvent { Year = -1800, DescriptionKey = "timelineEvent1800bce" },
            new TimelineEvent { Year = -890, DescriptionKey = "timelineEvent890bce" },
            new TimelineEvent { Year = -776, DescriptionKey = "timelineEvent776bce" },
            new TimelineEvent { Year = -753, DescriptionKey = "timelineEvent753bce" },
            new TimelineEvent { Year = -447, DescriptionKey = "timelineEvent447bce" },
            new TimelineEvent { Year = -427, DescriptionKey = "timelineEvent427bce" },
            new TimelineEvent { Year = -322, DescriptionKey = "timelineEvent322bce" },
            new TimelineEvent { Year = -200, DescriptionKey = "timelineEvent200bce" },
            new TimelineEvent { Year = -44, DescriptionKey = "timelineEvent44bce" },
            new TimelineEvent { Year = -4, DescriptionKey = "timelineEvent4bce" },
            new TimelineEvent { Year = 43, DescriptionKey = "timelineEvent43ce" },
            new TimelineEvent { Year = 79, DescriptionKey = "timelineEvent79ce" },
            new TimelineEvent { Year = 455, DescriptionKey = "timelineEvent455ce" },
            new TimelineEvent { Year = 500, DescriptionKey = "timelineEvent500ce" },
            new TimelineEvent { Year = 632, DescriptionKey = "timelineEvent632ce" },
            new TimelineEvent { Year = 793, DescriptionKey = "timelineEvent793ce" },
            new TimelineEvent { Year = 800, DescriptionKey = "timelineEvent800ce" },
            new TimelineEvent { Year = 1001, DescriptionKey = "timelineEvent1001ce" },
            new TimelineEvent { Year = 1077, DescriptionKey = "timelineEvent1077ce" },
            new TimelineEvent { Year = 1117, DescriptionKey = "timelineEvent1117ce" },
            new TimelineEvent { Year = 1199, DescriptionKey = "timelineEvent1199ce" },
            new TimelineEvent { Year = 1227, DescriptionKey = "timelineEvent1227ce" },
            new TimelineEvent { Year = 1337, DescriptionKey = "timelineEvent1337ce" },
            new TimelineEvent { Year = 1347, DescriptionKey = "timelineEvent1347ce" },
            new TimelineEvent { Year = 1428, DescriptionKey = "timelineEvent1428ce" },
            new TimelineEvent { Year = 1439, DescriptionKey = "timelineEvent1439ce" },
            new TimelineEvent { Year = 1492, DescriptionKey = "timelineEvent1492ce" },
            new TimelineEvent { Year = 1760, DescriptionKey = "timelineEvent1760ce" },
            new TimelineEvent { Year = 1763, DescriptionKey = "timelineEvent1763ce" },
            new TimelineEvent { Year = 1783, DescriptionKey = "timelineEvent1783ce" },
            new TimelineEvent { Year = 1789, DescriptionKey = "timelineEvent1789ce" },
            new TimelineEvent { Year = 1914, DescriptionKey = "timelineEvent1914ce" },
            new TimelineEvent { Year = 1929, DescriptionKey = "timelineEvent1929ce" },
            new TimelineEvent { Year = 1939, DescriptionKey = "timelineEvent1939ce" },
            new TimelineEvent { Year = 1957, DescriptionKey = "timelineEvent1957ce" },
            new TimelineEvent { Year = 1969, DescriptionKey = "timelineEvent1969ce" },
        };

        public IList<TimelineEvent> GetGlobalEvents()
        {
            return globalEvents;
        }
    }
}
