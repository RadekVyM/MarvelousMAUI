using Marvelous.Core.Models;
using Marvelous.Core.ViewModels;

namespace Marvelous.Maui.Models
{
    public class ArtifactCarouselItemViewModel : BaseViewModel
    {
        private double imageScale = 1;
        private bool isImageScaleAnimated = false;

        public Artifact Artifact { get; init; }
        public int Position { get; init; }
        public bool IsImageScaleAnimated
        {
            get => isImageScaleAnimated;
            set
            {
                isImageScaleAnimated = value;
                OnPropertyChanged(nameof(IsImageScaleAnimated));
            }
        }
        public double ImageScale
        {
            get => imageScale;
            set
            {
                imageScale = value;
                OnPropertyChanged(nameof(ImageScale));
            }
        }
    }
}
