using Marvelous.Core.Models;
using System.Windows.Input;

namespace Marvelous.Core.Interfaces.ViewModels
{
    public interface IDiscoveredArtifactPageViewModel : IBasePageViewModel
    {
        ICommand CollectionCommand { get; }
        Collectible Collectible { get; }
    }
}
