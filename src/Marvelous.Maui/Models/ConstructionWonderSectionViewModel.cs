namespace Marvelous.Maui.Models
{
    public class ConstructionWonderSectionViewModel : WonderSectionViewModel
    {
        private double verticalDelta;

        public string ConstructionInfo1 { get; init; }
        public string ConstructionInfo2 { get; init; }
        public string Callout2 { get; init; }
        public string VideoId { get; init; }
        public string VideoCaption { get; init; }
        public double VerticalDelta
        {
            get => verticalDelta;
            set
            {
                verticalDelta = value;
                OnPropertyChanged(nameof(VerticalDelta));
            }
        }
    }
}
