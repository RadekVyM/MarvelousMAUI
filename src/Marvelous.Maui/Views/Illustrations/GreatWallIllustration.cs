using Marvelous.Core.Models;

namespace Marvelous.Maui.Views.Illustrations
{
    public class GreatWallIllustration : BaseIllustration
    {
        private double buildingHeight => buildingWidth / config.MainObjectRatio;
        private double buildingWidth => Math.Min(Width * 1.2, Height * 0.9 * config.MainObjectRatio);
        private double sphereSize => buildingWidth * 0.28;
        private double leavesHeight => Height * 0.475;
        private double cloudWidth => sphereSize * 1.5;
        private double cloudHeight => cloudWidth / CloudRatio;

        protected override Rect defaultMainObjectBounds => new Rect((Width - buildingWidth) / 2, (Height - buildingHeight) / 2, buildingWidth, buildingWidth);

        protected override Rect defaultSkySphereBounds => new Rect(Width / 6, Height / 10, sphereSize, sphereSize);

        protected override Rect defaultForegroundLeftBounds => new Rect(leavesHeight * config.ForegroundLeftRatio * -0.3, Height - leavesHeight, leavesHeight * config.ForegroundLeftRatio, leavesHeight);

        protected override Rect defaultForegroundRightBounds => new Rect(Width - (leavesHeight * 1.2 * config.ForegroundRightRatio * 0.375), Height - (leavesHeight * 1.2), leavesHeight * 1.2 * config.ForegroundRightRatio, leavesHeight * 1.2);

        protected override Rect defaultCloud1Bounds => new Rect((Width / 2) - sphereSize + 30, (Height / 10) + 20, sphereSize, sphereSize / CloudRatio);

        protected override Rect defaultCloud2Bounds => new Rect(-20, (Height / 10) + sphereSize - cloudHeight - 10, cloudWidth, cloudHeight);

        protected override Rect defaultCloud3Bounds => new Rect(-(cloudWidth * 1.2) + 60, (Height / 2) - (cloudHeight * 1.2) - 40, cloudWidth * 1.2, cloudHeight * 1.2);

        protected override Rect outSkySphereBounds => new Rect(defaultSkySphereBounds.X, defaultSkySphereBounds.Y + (defaultSkySphereBounds.Height * 0.7), defaultSkySphereBounds.Width, defaultSkySphereBounds.Height);

        public GreatWallIllustration() : base(WonderType.GreatWall)
        {
        }
    }
}
