using System.Windows.Input;
using Marvelous.Core.Models;

namespace Marvelous.Core.Interfaces.ViewModels
{
    public interface IArtifactsPageViewModel : IBasePageViewModel
    {
        public static int MinYearsRange { get; } = 400;

        IList<Wonder> Wonders { get; }
        Wonder CurrentWonder { get; }
        string SearchTerm { get; set; }
        IList<string> SearchSuggestions { get; }
        IList<Search> Searches { get; }
        IList<Search> AllSearches { get; }
        int MinYear { get; }
        int MaxYear { get; }
        int FromYear { get; set; }
        int ToYear { get; set; }

        ICommand ArtifactCommand { get; }

        void UpdateSearches();
        void UpdateSearchSuggestions();
    }
}
