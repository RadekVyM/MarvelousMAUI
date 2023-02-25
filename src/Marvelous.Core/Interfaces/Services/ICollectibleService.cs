using Marvelous.Core.Models;

namespace Marvelous.Core.Interfaces.Services
{
    public interface ICollectibleService
    {
        IList<Collectible> GetCollectibles();
        IList<Collectible> GetCollectiblesForWonder(WonderType wonder);
        Collectible GetLastDiscoveredCollectible();
        void UpdateCollectibleState(Collectible collectible, CollectibleState state);
        void ResetAllCollectiblesToLost();
    }
}
