using System.Windows.Input;
using Marvelous.Core.Models;

namespace Marvelous.Core.Interfaces.ViewModels
{
    public interface IWonderArtifactsPageViewModel : IBasePageViewModel
    {
        Wonder CurrentWonder { get; set; }
        Artifact CurrentArtifact { get; set; }
        int CurrentArtifactIndex { get; set; }
        IList<Artifact> CurrentWonderArtifacts { get; set; }
        ICommand BrowseArtifactsCommand { get; }
        ICommand ArtifactCommand { get; }
    }
}
