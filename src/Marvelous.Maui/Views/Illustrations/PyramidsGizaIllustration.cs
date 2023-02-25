using Marvelous.Core.Models;

namespace Marvelous.Maui.Views.Illustrations
{
    public class PyramidsGizaIllustration : BaseIllustration
    {
        private double pyramidsHeight => (Height / 2.4);
        private double pyramidsTop => (Height - pyramidsHeight) / 2;
        private double skySphereSize => (pyramidsHeight / 2.3);
        private double skySphereTop => pyramidsTop - skySphereSize + 20;
        private double dunesTop => Math.Max(pyramidsTop + pyramidsHeight - (Width / config.ForegroundLeftRatio), Height * 0.4);
        private double sphinxWidth => sphinxHeight * config.ForegroundRightRatio;
        private double sphinxHeight => Math.Max(sphinxIdealHeight, Width / config.ForegroundRightRatio);
        private double sphinxIdealHeight => Height - dunesTop;
        private double centerCloudWidth => skySphereSize * 1.9;
        private double centerCloudHeight => centerCloudWidth / cloudRatio;
        private double centerCloudTop => skySphereTop + 8;

        protected override Rect defaultMainObjectBounds => new Rect((Width - (pyramidsHeight * config.MainObjectRatio)) / 2, pyramidsTop, pyramidsHeight * config.MainObjectRatio, pyramidsHeight);

        protected override Rect defaultSkySphereBounds => new Rect(Width - skySphereSize - 40, skySphereTop, skySphereSize, skySphereSize);

        protected override Rect defaultForegroundLeftBounds => new Rect(0, dunesTop, Width, Width / config.ForegroundLeftRatio);

        protected override Rect defaultForegroundRightBounds => new Rect((Width - sphinxWidth) / 2, Height - sphinxIdealHeight, sphinxWidth, sphinxHeight);

        protected override Rect defaultCloud1Bounds => new Rect(Width / 10, centerCloudTop - centerCloudHeight + 10, centerCloudWidth * 0.9, centerCloudHeight * 0.9);

        protected override Rect defaultCloud2Bounds => new Rect((Width - centerCloudWidth) / 2, centerCloudTop, centerCloudWidth, centerCloudHeight);

        protected override Rect defaultCloud3Bounds => new Rect(Width - (Width / 10) - centerCloudWidth * 0.7, skySphereTop + skySphereSize - 10 - centerCloudHeight * 0.7, centerCloudWidth * 0.7, centerCloudHeight * 0.7);

        protected override Rect outForegroundLeftBounds => new Rect(defaultForegroundLeftBounds.X, defaultForegroundLeftBounds.Y + 30, defaultForegroundLeftBounds.Width, defaultForegroundLeftBounds.Height);

        protected override Rect outForegroundRightBounds => new Rect(defaultForegroundRightBounds.X, defaultForegroundRightBounds.Y + 50, defaultForegroundRightBounds.Width, defaultForegroundRightBounds.Height);

        protected override Rect outSkySphereBounds => new Rect(defaultSkySphereBounds.X, defaultSkySphereBounds.Y + (defaultSkySphereBounds.Height * 1.8), defaultSkySphereBounds.Width, defaultSkySphereBounds.Height);

        public PyramidsGizaIllustration() : base(WonderType.PyramidsGiza)
        {
        }
    }
}
