using Marvelous.Core.Models;

namespace Marvelous.Core.Interfaces.ViewModels
{
    public interface IGlobalTimelinePageViewModel : IBasePageViewModel
    {
        IList<TimelineEvent> TimelineEvents { get; set; }
        IList<Wonder> Wonders { get; set; }
        WonderType CurrentWonderType { get; set; }

        TimelineEra GetTimelineEra(int year);
    }
}
