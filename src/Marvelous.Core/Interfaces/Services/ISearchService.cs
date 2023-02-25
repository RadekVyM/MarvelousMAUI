using Marvelous.Core.Models;

namespace Marvelous.Core.Interfaces.Services
{
    public interface ISearchService
    {
        void AssignSearchData(Wonder wonder);
        IEnumerable<string> GetSearchSuggestions(WonderType wonderType);
        IEnumerable<Search> GetSearches(WonderType wonderType, int fromYear = int.MinValue, int toYear = int.MaxValue, string searchTerm = null);
    }
}
