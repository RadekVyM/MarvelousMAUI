using Marvelous.Core.Interfaces.Repositories;
using Marvelous.Core.Interfaces.Services;
using Marvelous.Core.Models;

namespace Marvelous.Core.Services
{
    public class WonderService : IWonderService
    {
        private readonly ISearchService searchService;
        private readonly IWonderRepository wonderRepository;

        public WonderService(ISearchService searchService, IWonderRepository wonderRepository)
        {
            this.searchService = searchService;
            this.wonderRepository = wonderRepository;
        }

        public IList<Wonder> GetWonders()
        {
            return wonderRepository.GetWonders();
        }

        public Wonder GetWonder(WonderType wonder)
        {
            return wonderRepository.GetWonder(wonder);
        }
    }
}
