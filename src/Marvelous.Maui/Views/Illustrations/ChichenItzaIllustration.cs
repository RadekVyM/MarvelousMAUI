using Marvelous.Core.Models;

namespace Marvelous.Maui.Views.Illustrations
{
    public class ChichenItzaIllustration : BaseIllustration
    {
        private double buildingHeight => Math.Max(Height * 0.4, Width / config.MainObjectRatio);
        private double buildingWidth => buildingHeight * config.MainObjectRatio;
        private double buildignTop => (Height - buildingHeight) / 2;
        private double sunWidth => buildingWidth * 0.21;
        private double leavesHeight => Height * 0.6;
        private double leavesTop => leavesHeight * -0.5;
        private double cloudWidth => Width * 0.6;
        private double cloudHeight => cloudWidth / CloudRatio;

        protected override Rect defaultMainObjectBounds => new Rect((Width - buildingWidth) / 2, buildignTop, buildingWidth, buildingHeight);

        protected override Rect defaultSkySphereBounds => new Rect((Width / 2) - 20, buildignTop - 30, sunWidth, sunWidth / config.SkySphereRatio);

        protected override Rect defaultForegroundLeftBounds => new Rect(Height * 0.5 * config.ForegroundLeftRatio * -0.38, Height * 0.5, Height * 0.5 * config.ForegroundLeftRatio, Height * 0.5);

        protected override Rect defaultForegroundRightBounds => new Rect(Width - (Height * 0.3 * config.ForegroundLeftRatio * 0.5), Height * 0.6, Height * 0.3 * config.ForegroundLeftRatio, Height * 0.3);

        protected override Rect defaultTopLeftBounds => new Rect(leavesHeight * config.TopLeftRatio * -0.375, leavesTop, leavesHeight * config.TopLeftRatio, leavesHeight);

        protected override Rect defaultTopRightBounds => new Rect(Width - (leavesHeight * config.TopRightRatio * 0.55), leavesTop * 0.6, leavesHeight * config.TopRightRatio, leavesHeight);

        protected override Rect defaultCloud1Bounds => new Rect(-20, buildignTop - 10, cloudWidth, cloudHeight);

        protected override Rect defaultCloud2Bounds => new Rect(Width / 2, Height * 0.12, cloudWidth * 0.9, cloudHeight * 0.9);

        protected override Rect defaultCloud3Bounds => new Rect((Width / 2) - (cloudWidth / 2), 20, cloudWidth * 0.8, cloudHeight * 0.8);

        protected override Rect outSkySphereBounds => new Rect(defaultSkySphereBounds.X, defaultSkySphereBounds.Y + 60, defaultSkySphereBounds.Width, defaultSkySphereBounds.Height);

        public ChichenItzaIllustration() : base(WonderType.ChichenItza)
        {
        }
    }
}
