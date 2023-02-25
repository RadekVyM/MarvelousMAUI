using Marvelous.Core.Interfaces.Repositories;
using Marvelous.Core.Interfaces.Services;
using Marvelous.Core.Models;

namespace Marvelous.Core.Services
{
    public class SearchService : ISearchService
    {
        private readonly ISearchRepository searchRepository;


        public SearchService(ISearchRepository searchRepository)
        {
            this.searchRepository = searchRepository;
        }


        public void AssignSearchData(Wonder wonder)
        {
            wonder.SearchData = searchRepository.GetSearches(wonder.Type);
            wonder.SearchSuggestions = searchRepository.GetSearchSuggestions(wonder.Type);
        }

        public IEnumerable<string> GetSearchSuggestions(WonderType wonderType)
        {
            return searchRepository.GetSearchSuggestions(wonderType);
        }

        public IEnumerable<Search> GetSearches(WonderType wonderType, int fromYear = int.MinValue, int toYear = int.MaxValue, string searchTerm = null)
        {
            var query = searchRepository
                .GetSearches(wonderType)
                .Where(s => s.Year >= fromYear && s.Year <= toYear);

            if (searchTerm is not null)
                query = query.Where(s => s.Keywords.Contains(searchTerm));

            return query;
        }
    }
}
