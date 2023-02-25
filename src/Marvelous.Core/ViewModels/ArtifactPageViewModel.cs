using Marvelous.Core.Interfaces.Services;
using Marvelous.Core.Interfaces.ViewModels;
using Marvelous.Core.Models;
using Marvelous.Core.ViewModels.Parameters;

namespace Marvelous.Core.ViewModels
{
    public class ArtifactPageViewModel : BasePageViewModel, IArtifactPageViewModel
    {
        private readonly IArtifactService artifactService;
        private Artifact currentArtifact;

        public Artifact CurrentArtifact
        {
            get => currentArtifact;
            set
            {
                currentArtifact = value;
                OnPropertyChanged(nameof(CurrentArtifact));
            }
        }


        public ArtifactPageViewModel(IArtifactService artifactService)
        {
            this.artifactService = artifactService;
        }


        public override void OnApplyParameters(IParameters parameters)
        {
            base.OnApplyParameters(parameters);

            if (parameters is not ArtifactPageParameters artifactParameters)
                return;

            CurrentArtifact = artifactService.GetArtifact(artifactParameters.Wonder, artifactParameters.ArtifactId);
        }
    }
}