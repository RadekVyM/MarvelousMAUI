using System.Windows.Input;
using Marvelous.Core.Models;

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
