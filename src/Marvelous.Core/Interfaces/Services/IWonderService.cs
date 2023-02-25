using Marvelous.Core.Models;

namespace Marvelous.Core.Interfaces.Services
{
    public interface IWonderService
    {
        Wonder GetWonder(WonderType wonder);
        IList<Wonder> GetWonders();
    }
}
