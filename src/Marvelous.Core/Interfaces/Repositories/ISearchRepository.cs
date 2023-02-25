using Marvelous.Core.Models;

namespace Marvelous.Core.Interfaces.Repositories
{
    public interface ISearchRepository
    {
        Search GetSearch(WonderType wonder, string artifactId);
        IList<Search> GetSearches(WonderType wonder);
        IList<string> GetSearchSuggestions(WonderType wonder);
    }
}
