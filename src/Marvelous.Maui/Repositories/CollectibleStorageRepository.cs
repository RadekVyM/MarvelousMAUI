using System;
using Marvelous.Core.Models;
using Marvelous.Data.Repositories;

namespace Marvelous.Maui.Repositories
{
	public class CollectibleStorageRepository : CollectibleRepository
    {
        public override IList<Collectible> GetCollectibles()
        {
            return base
                .GetCollectibles()
                .Select(c =>
                {
                    c.CollectibleState = GetCollectibleStateFromStorage(c.Id);
                    c.DiscoveredTime = GetCollectibleiscoveredTimeFromStorage(c.Id);
                    return c;
                })
                .ToList();
        }

        public override void UpdateCollectibleState(Collectible collectible, CollectibleState state)
        {
            var key = GetCollectibleStateKey(collectible.Id);

            Preferences.Set(key, (int)state);
            collectible.CollectibleState = state;

            if (collectible.CollectibleState != CollectibleState.Explored)
            {
                var time = collectible.CollectibleState == CollectibleState.Discovered ? DateTime.UtcNow : DateTime.MinValue;

                Preferences.Set(GetCollectibleDiscoveredTimeKey(collectible.Id), time);
                collectible.DiscoveredTime = time;
            }
        }

        private CollectibleState GetCollectibleStateFromStorage(string id)
        {
            var key = GetCollectibleStateKey(id);
            var value = (CollectibleState)Preferences.Get(key, (int)CollectibleState.Lost);

            if (!Preferences.ContainsKey(key))
                Preferences.Set(key, (int)value);

            return value;
        }

        private DateTime GetCollectibleiscoveredTimeFromStorage(string id)
        {
            var key = GetCollectibleDiscoveredTimeKey(id);
            var value = (DateTime)Preferences.Get(key, DateTime.MinValue);

            if (!Preferences.ContainsKey(key))
                Preferences.Set(key, value);

            return value;
        }

        private string GetCollectibleStateKey(string id)
        {
            return $"Collectible_State_{id}";
        }

        private string GetCollectibleDiscoveredTimeKey(string id)
        {
            return $"Collectible_Discovered_Time_{id}";
        }
    }
}
