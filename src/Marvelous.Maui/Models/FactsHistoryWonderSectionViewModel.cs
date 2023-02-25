namespace Marvelous.Maui.Models
{
    public class FactsHistoryWonderSectionViewModel : WonderSectionViewModel
    {
        private Thickness margin;
        private double verticalDelta;
        private string pullQuoteImage;

        public string HistoryInfo1 { get; init; }
        public string HistoryInfo2 { get; init; }
        public string Callout1 { get; init; }
        public string PullQuote1Top { get; init; }
        public string PullQuote1Bottom { get; init; }
        public Thickness Margin
        {
            get => margin;
            set
            {
                margin = value;
                OnPropertyChanged(nameof(Margin));
            }
        }
        public double VerticalDelta
        {
            get => verticalDelta;
            set
            {
                verticalDelta = value;
                OnPropertyChanged(nameof(VerticalDelta));
            }
        }
        public string PullQuoteImage
        {
            get => pullQuoteImage;
            set
            {
                pullQuoteImage = value;
                OnPropertyChanged(nameof(PullQuoteImage));
            }
        }
    }
}
