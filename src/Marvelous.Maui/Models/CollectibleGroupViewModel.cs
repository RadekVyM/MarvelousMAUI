using Marvelous.Core.Models;

namespace Marvelous.Maui.Models
{
	public class CollectibleGroupViewModel : List<Collectible>
    {
        public string WonderTitle { get; }

        public CollectibleGroupViewModel(string wonderTitle, IEnumerable<Collectible> collectibles) : base(collectibles)
		{
            WonderTitle = wonderTitle;
        }
    }
}
