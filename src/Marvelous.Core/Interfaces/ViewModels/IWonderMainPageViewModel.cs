using Marvelous.Core.Models;
using System.Windows.Input;

namespace Marvelous.Core.Interfaces.ViewModels
{
    public interface IWonderMainPageViewModel : IBaseWonderCollectiblePageViewModel
    {
        Wonder CurrentWonder { get; }
        ICommand PlayVideoCommand { get; }
    }
}
