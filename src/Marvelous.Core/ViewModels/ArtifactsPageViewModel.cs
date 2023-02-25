using System.Collections.ObjectModel;
using System.Windows.Input;
using Marvelous.Core.Extensions;
using Marvelous.Core.Interfaces.Services;
using Marvelous.Core.Interfaces.ViewModels;
using Marvelous.Core.Models;
using Marvelous.Core.ViewModels.Parameters;

namespace Marvelous.Core.ViewModels
{
    public class ArtifactsPageViewModel : BasePageViewModel, IArtifactsPageViewModel
    {
        private readonly IArtifactService artifactService;
        private readonly IWonderService wonderService;
        private readonly ISearchService searchService;

        private Wonder currentWonder;
        private IList<string> searchSuggestions;
        private IList<string> allSearchSuggestions;
        private IList<Search> searches;
        private string searchTerm;
        private int toYear;
        private int fromYear;
        private int minYear;
        private int maxYear;
        private IList<Search> allSearches;
        private IList<Wonder> wonders;


        public IList<Wonder> Wonders
        {
            get => wonders;
            set
            {
                wonders = value;
                OnPropertyChanged(nameof(Wonders));
            }
        }

        public Wonder CurrentWonder
        {
            get => currentWonder;
            set
            {
                currentWonder = value;
                OnPropertyChanged(nameof(CurrentWonder));
            }
        }

        public string SearchTerm
        {
            get => searchTerm;
            set
            {
                searchTerm = value;
                UpdateSearchSuggestions();
                OnPropertyChanged(nameof(SearchTerm));
            }
        }

        public IList<string> SearchSuggestions
        {
            get => searchSuggestions;
            set
            {
                searchSuggestions = value;
                OnPropertyChanged(nameof(SearchSuggestions));
            }
        }

        public IList<Search> AllSearches
        {
            get => allSearches;
            set
            {
                allSearches = value;
                OnPropertyChanged(nameof(AllSearches));
            }
        }

        public IList<Search> Searches
        {
            get => searches;
            set
            {
                searches = value;
                OnPropertyChanged(nameof(Searches));
            }
        }

        public int MinYear
        {
            get => minYear;
            set
            {
                minYear = value;
                OnPropertyChanged(nameof(MinYear));
            }
        }

        public int MaxYear
        {
            get => maxYear;
            set
            {
                maxYear = value;
                OnPropertyChanged(nameof(MaxYear));
            }
        }

        public int FromYear
        {
            get => fromYear;
            set
            {
                if (value < MinYear || value > MaxYear)
                    throw new ArgumentOutOfRangeException();

                fromYear = value;
                //UpdateSearches();
                OnPropertyChanged(nameof(FromYear));
            }
        }

        public int ToYear
        {
            get => toYear;
            set
            {
                if (value < MinYear || value > MaxYear)
                    throw new ArgumentOutOfRangeException();

                toYear = value;
                //UpdateSearches();
                OnPropertyChanged(nameof(ToYear));
            }
        }

        public ICommand ArtifactCommand { get; init; }


        public ArtifactsPageViewModel(IArtifactService artifactService, IWonderService wonderService, ISearchService searchService, INavigationService navigationService)
        {
            this.artifactService = artifactService;
            this.wonderService = wonderService;
            this.searchService = searchService;
            
            ArtifactCommand = new RelayCommand((parameter) =>
            {
                var id = parameter.ToString();
                navigationService.GoTo(PageType.ArtifactPage, new ArtifactPageParameters(CurrentWonder.Type, id));
            });
        }


        public override void OnApplyFirstParameters(IParameters parameters)
        {
            base.OnApplyFirstParameters(parameters);

            if (parameters is not WonderPageParameters wonderParameters)
                return;

            Wonders = wonderService.GetWonders();
            CurrentWonder = wonderService.GetWonder(wonderParameters.Wonder);
            SearchSuggestions = allSearchSuggestions = searchService.GetSearchSuggestions(wonderParameters.Wonder).ToList();
            AllSearches = searchService.GetSearches(CurrentWonder.Type).ToList();

            MinYear = Math.Min(Wonders.MinWonderYear(), AllSearches.MinSearchYear());
            MaxYear = Math.Max(Wonders.MaxWonderYear(), AllSearches.MaxSearchYear());

            FromYear = CurrentWonder.StartYr + IArtifactsPageViewModel.MinYearsRange <= MaxYear ?
                CurrentWonder.StartYr :
                MaxYear - IArtifactsPageViewModel.MinYearsRange;
            ToYear = FromYear + IArtifactsPageViewModel.MinYearsRange <= MaxYear ?
                FromYear + Math.Max(IArtifactsPageViewModel.MinYearsRange, Math.Abs(CurrentWonder.EndYr - FromYear)) :
                MaxYear;

            UpdateSearches();
        }

        public void UpdateSearchSuggestions()
        {
            var newSuggestions = allSearchSuggestions
                .Where(s => s.StartsWith(SearchTerm))
                .ToList();

            SearchSuggestions = newSuggestions;
        }

        public void UpdateSearches()
        {
            Searches = searchService
                .GetSearches(CurrentWonder.Type, FromYear, ToYear, SearchTerm)
                .ToList();
        }
    }
}