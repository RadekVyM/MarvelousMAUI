using System.Windows.Input;
using Marvelous.Core.Models;

namespace Marvelous.Core.Interfaces.ViewModels
{
	public interface IBaseWonderCollectiblePageViewModel : IBasePageViewModel
    {
        Collectible Collectible { get; }
        ICommand CollectibleCommand { get; }
    }
}
