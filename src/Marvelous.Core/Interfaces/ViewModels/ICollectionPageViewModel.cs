using Marvelous.Core.Models;
using System.Windows.Input;

namespace Marvelous.Core.Interfaces.ViewModels
{
    public interface ICollectionPageViewModel : IBasePageViewModel
    {
        IList<Collectible> Collectibles { get; }
        Collectible LastDiscoveredCollectible { get; }
        int DiscoveredCollectiblesPercentage { get; }
        int DiscoveredCollectiblesCount { get; }
        int CollectiblesCount { get; }
        int DiscoveredAndNotExploredCollectiblesCount { get; }
        ICommand ResetCollectionCommand { get; }
        ICommand ArtifactCommand { get; }
    }
}
