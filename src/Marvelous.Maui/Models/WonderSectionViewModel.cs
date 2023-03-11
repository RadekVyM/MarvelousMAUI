using Marvelous.Core.Models;
using Marvelous.Core.ViewModels;

namespace Marvelous.Maui.Models
{
    public class WonderSectionViewModel : BaseViewModel
    {
        private bool collapsedSeparator;

        public string Icon { get; init; }
        public string Title { get; init; }
        public int VisibleCollectiblePosition { get; init; }
        public WonderType WonderType { get; init; }
        public bool CollapsedSeparator
        {
            get => collapsedSeparator;
            set
            {
                collapsedSeparator = value;
                OnPropertyChanged(nameof(CollapsedSeparator));
            }
        }
    }
}
