using Marvelous.Core.Models;
using System.Windows.Input;

namespace Marvelous.Core.Interfaces.ViewModels
{
    public interface IBaseWonderCollectiblePageViewModel : IBasePageViewModel
    {
        Collectible Collectible { get; }
        ICommand CollectibleCommand { get; }
    }
}
