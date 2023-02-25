using Marvelous.Core.Models;

namespace Marvelous.Maui.Views.Illustrations
{
    public class PyramidsGizaHeaderIllustration : BaseHeaderIllustration
    {
        private double pyramidsWidth => Width * 1.12;
        private double pyramidsHeight => Math.Min(pyramidsWidth / config.MainObjectRatio, Height * 0.8);
        private double pyramidsTop => Height - pyramidsHeight;
        private double skySphereSize => (pyramidsHeight * 0.3);
        private double skySphereTop => pyramidsTop - skySphereSize + 20;
        private double centerCloudWidth => skySphereSize * 2.8;
        private double centerCloudHeight => centerCloudWidth / cloudRatio;
        private double centerCloudTop => skySphereTop - 8;

        protected override Rect defaultMainObjectBounds => new Rect((Width - pyramidsWidth) / 2, pyramidsTop, pyramidsWidth, pyramidsHeight);

        protected override Rect defaultSkySphereBounds => new Rect(Width - (skySphereSize * 2), skySphereTop, skySphereSize, skySphereSize);

        protected override Rect defaultCloud1Bounds => new Rect(Width / 10, centerCloudTop + (1.6 * centerCloudHeight), centerCloudWidth * 0.9, centerCloudHeight * 0.9);

        protected override Rect defaultCloud2Bounds => new Rect((Width - centerCloudWidth) / 2, centerCloudTop, centerCloudWidth, centerCloudHeight);

        protected override Rect defaultCloud3Bounds => new Rect(Width - (Width / 10) - centerCloudWidth * 0.7, skySphereTop + skySphereSize - 10 - centerCloudHeight * 0.7, centerCloudWidth * 0.7, centerCloudHeight * 0.7);


        public PyramidsGizaHeaderIllustration() : base(WonderType.PyramidsGiza)
        {
        }
    }
}
