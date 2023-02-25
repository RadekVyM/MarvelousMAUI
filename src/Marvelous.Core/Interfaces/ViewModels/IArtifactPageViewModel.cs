using Marvelous.Core.Models;

namespace Marvelous.Core.Interfaces.ViewModels
{
    public interface IArtifactPageViewModel : IBasePageViewModel
    {
        Artifact CurrentArtifact { get; set; }
    }
}
