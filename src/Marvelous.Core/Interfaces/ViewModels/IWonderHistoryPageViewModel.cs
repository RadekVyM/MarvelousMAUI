using Marvelous.Core.Models;
using System.Windows.Input;

namespace Marvelous.Core.Interfaces.ViewModels
{
    public interface IWonderHistoryPageViewModel : IBaseWonderCollectiblePageViewModel
    {
        IList<Wonder> Wonders { get; }
        Wonder CurrentWonder { get; }
        IList<TimelineEvent> TimelineEvents { get; }
        TimelineEra TimelineEra { get; }
        ICommand GlobalTimelineCommand { get; }
    }
}
