using Marvelous.Core.Models;

namespace Marvelous.Core.Extensions
{
    public static class CollectionExtensions
    {
        public static int MinEventYear<T>(this T timelineEvents) where T : IEnumerable<TimelineEvent>
        {
            return CenturiesFloor(timelineEvents.Min(te => te.Year));
        }

        public static int MaxEventYear<T>(this T timelineEvents) where T : IEnumerable<TimelineEvent>
        {
            return CenturiesCeiling(timelineEvents.Max(te => te.Year));
        }

        public static int MinSearchYear<T>(this T timelineEvents) where T : IEnumerable<Search>
        {
            return CenturiesFloor(timelineEvents.Min(s => s.Year));
        }

        public static int MaxSearchYear<T>(this T timelineEvents) where T : IEnumerable<Search>
        {
            return CenturiesCeiling(timelineEvents.Max(s => s.Year));
        }

        public static int MinWonderYear<T>(this T wonders) where T : IEnumerable<Wonder>
        {
            return CenturiesFloor(wonders.Min(w => w.StartYr));
        }

        public static int MaxWonderYear<T>(this T wonders) where T : IEnumerable<Wonder>
        {
            return CenturiesCeiling(wonders.Max(w => w.EndYr));
        }

        private static int CenturiesCeiling(int year)
        {
            return ((int)Math.Ceiling(year / 100d) * 100) + 100;
        }

        private static int CenturiesFloor(int year)
        {
            return ((int)Math.Floor(year / 100d) * 100) - 100;
        }
    }
}
