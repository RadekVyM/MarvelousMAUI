using Marvelous.Core.Models;
using System.Windows.Input;

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
