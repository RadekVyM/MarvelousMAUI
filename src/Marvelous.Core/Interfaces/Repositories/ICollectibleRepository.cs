using Marvelous.Core.Models;

namespace Marvelous.Core.Interfaces.Repositories
{
    public interface ICollectibleRepository
    {
        IList<Collectible> GetCollectibles();
        IList<Collectible> GetCollectiblesForWonder(WonderType wonder);
        void UpdateCollectibleState(Collectible collectible, CollectibleState state);
    }
}
