namespace Marvelous.Maui.Extensions
{
    public static class ColorExtensions
    {
        public static Color WithOpacity(this Color color, double opacity) =>
            Color.FromRgba(color.Red, color.Green, color.Blue, opacity);
    }
}
