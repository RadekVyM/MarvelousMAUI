using System.Windows.Input;
using Marvelous.Core.Models;

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
