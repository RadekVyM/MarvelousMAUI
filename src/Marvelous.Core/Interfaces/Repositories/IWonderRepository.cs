using Marvelous.Core.Models;

namespace Marvelous.Core.Interfaces.Repositories
{
    public interface IWonderRepository
    {
        Wonder GetWonder(WonderType wonder);
        IList<Wonder> GetWonders();
    }
}
