using Marvelous.Core.Interfaces.Repositories;
using Marvelous.Core.Interfaces.Services;
using Marvelous.Core.Models;

namespace Marvelous.Core.Services
{
    public class CollectibleService : ICollectibleService
    {
        private readonly IWonderRepository wonderRepository;
        private readonly ICollectibleRepository collectibleRepository;


        public CollectibleService(IWonderRepository wonderRepository, ICollectibleRepository collectibleRepository)
        {
            this.wonderRepository = wonderRepository;
            this.collectibleRepository = collectibleRepository;
        }


        public IList<Collectible> GetCollectibles()
        {
            var collectibles = collectibleRepository.GetCollectibles();

            AssignWonderInfo(collectibles);

            return collectibles;
        }

        public IList<Collectible> GetCollectiblesForWonder(WonderType wonder)
        {
            var collectibles = collectibleRepository.GetCollectiblesForWonder(wonder);

            AssignWonderInfo(collectibles);

            return collectibles
                .OrderBy(c => c.ArtifactId)
                .ToList();
        }

        public Collectible GetLastDiscoveredCollectible()
        {
            var collectibles = collectibleRepository.GetCollectibles();

            return collectibles
                .Where(c => c.CollectibleState == CollectibleState.Discovered)
                .OrderByDescending(c => c.DiscoveredTime)
                .FirstOrDefault();
        }

        public void UpdateCollectibleState(Collectible collectible, CollectibleState state)
        {
            collectibleRepository.UpdateCollectibleState(collectible, state);
        }

        public void ResetAllCollectiblesToLost()
        {
            foreach (var collectible in collectibleRepository.GetCollectibles())
            {
                UpdateCollectibleState(collectible, CollectibleState.Lost);
            }
        }


        private void AssignWonderInfo(Collectible collectible)
        {
            var wonder = wonderRepository.GetWonder(collectible.Wonder);

            collectible.Subtitle = wonder?.ArtifactCulture;
            collectible.WonderName = wonder?.Title;
        }

        private void AssignWonderInfo(IEnumerable<Collectible> collectibles)
        {
            foreach (var collectible in collectibles)
            {
                AssignWonderInfo(collectible);
            }
        }
    }
}
