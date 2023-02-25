using System.Windows.Input;
using Marvelous.Core.Interfaces.Services;
using Marvelous.Core.Interfaces.ViewModels;
using Marvelous.Core.Models;
using Marvelous.Core.ViewModels.Parameters;

namespace Marvelous.Core.ViewModels
{
    public class WonderArtifactsPageViewModel : BasePageViewModel, IWonderArtifactsPageViewModel
    {
        private Wonder currentWonder;
        private IList<Artifact> currentWonderArtifacts;
        private Artifact currentArtifact;
        private int currentArtifactIndex;

        private readonly IWonderService wonderService;
        private readonly IArtifactService artifactService;

        public Wonder CurrentWonder
        {
            get => currentWonder;
            set
            {
                currentWonder = value;
                OnPropertyChanged(nameof(CurrentWonder));
            }
        }

        public Artifact CurrentArtifact
        {
            get => currentArtifact;
            set
            {
                currentArtifact = value;
                currentArtifactIndex = CurrentWonderArtifacts.IndexOf(value);
                OnPropertyChanged(nameof(CurrentArtifact));
                OnPropertyChanged(nameof(CurrentArtifactIndex));
            }
        }

        public int CurrentArtifactIndex
        {
            get => currentArtifactIndex;
            set
            {
                currentArtifactIndex = value;
                currentArtifact = CurrentWonderArtifacts[currentArtifactIndex];
                OnPropertyChanged(nameof(CurrentArtifactIndex));
                OnPropertyChanged(nameof(CurrentArtifact));
            }
        }

        public IList<Artifact> CurrentWonderArtifacts
        {
            get => currentWonderArtifacts;
            set
            {
                currentWonderArtifacts = value;
                OnPropertyChanged(nameof(CurrentWonderArtifacts));
            }
        }

        public ICommand BrowseArtifactsCommand { get; init; }
        public ICommand ArtifactCommand { get; init; }


        public WonderArtifactsPageViewModel(IWonderService wonderService, IArtifactService artifactService, INavigationService navigationService)
        {
            this.wonderService = wonderService;
            this.artifactService = artifactService;

            BrowseArtifactsCommand = new RelayCommand(() =>
            {
                navigationService.GoTo(PageType.ArtifactsPage, new WonderPageParameters(CurrentWonder.Type));
            });
            ArtifactCommand = new RelayCommand((parameter) =>
            {
                var position = (int)parameter;

                if (position == CurrentArtifactIndex)
                    navigationService.GoTo(PageType.ArtifactPage, new ArtifactPageParameters(currentWonder.Type, currentArtifact.ObjectId));
                else
                    CurrentArtifactIndex = position;
            });
        }


        public override void OnApplyFirstParameters(IParameters parameters)
        {
            base.OnApplyFirstParameters(parameters);

            if (parameters is not WonderPageParameters wonderParameters)
                return;

            CurrentWonder = wonderService.GetWonder(wonderParameters.Wonder);
            CurrentWonderArtifacts = artifactService.GetArtifactsForWonder(wonderParameters.Wonder);
            CurrentArtifact = currentWonderArtifacts?.FirstOrDefault();
        }
    }
}