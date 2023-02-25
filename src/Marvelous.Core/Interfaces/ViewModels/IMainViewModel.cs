using System.Windows.Input;
using Marvelous.Core.Models;

namespace Marvelous.Core.Interfaces.ViewModels
{
    public interface IMainViewModel : IBaseViewModel
    {
        Wonder CurrentWonder { get; set; }
        IList<Wonder> Wonders { get; set; }
        ICommand GlobalTimelineCommand { get; init; }
        ICommand CollectionCommand { get; init; }
    }
}
