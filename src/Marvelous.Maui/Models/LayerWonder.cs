using Marvelous.Core.Models;

namespace Marvelous.Maui.Models
{
    public record LayerWonder(WonderType WonderType, int StartYear, int EndYear, string ImagePath, Color Color)
    {
        public double Start { get; set; }
        public double End { get; set; }
        public double ImageOffset { get; set; }
        public Image Image { get; set; }
    }
}
