using System.Windows.Input;
using Marvelous.Core.Models;

namespace Marvelous.Core.Interfaces.ViewModels
{
    public interface IWonderMainPageViewModel : IBaseWonderCollectiblePageViewModel
    {
        Wonder CurrentWonder { get; }
        ICommand PlayVideoCommand { get; }
    }
}
