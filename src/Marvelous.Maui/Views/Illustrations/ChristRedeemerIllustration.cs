using Marvelous.Core.Models;

namespace Marvelous.Maui.Views.Illustrations
{
    public class ChristRedeemerIllustration : BaseIllustration
    {
        private double christWidth => Width * 2.8;
        private double christTop => 100;
        private double sphereSize => Width * 0.45;
        private double leavesHeight => Height * 0.6;
        private double centerCloudWidth => sphereSize * 1.3;
        private double centerCloudTop => christTop + 100;

        protected override Rect defaultMainObjectBounds => new Rect((Width - christWidth) / 2, christTop, christWidth, christWidth / config.MainObjectRatio);

        protected override Rect defaultSkySphereBounds => new Rect((Width / 2) - 20, christTop + 20, sphereSize, sphereSize);

        protected override Rect defaultForegroundLeftBounds => new Rect((leavesHeight * config.ForegroundLeftRatio) * -0.45, Height * 0.45, leavesHeight * config.ForegroundLeftRatio, leavesHeight);

        protected override Rect defaultForegroundRightBounds => new Rect(Width - (leavesHeight * config.ForegroundRightRatio * 0.55), Height * 0.55, leavesHeight * config.ForegroundRightRatio, leavesHeight);

        protected override Rect defaultCloud1Bounds => new Rect((Width - centerCloudWidth) / 2, centerCloudTop, centerCloudWidth, centerCloudWidth / cloudRatio);

        protected override Rect defaultCloud2Bounds => new Rect(-20, centerCloudTop + (centerCloudWidth / cloudRatio) + 10, centerCloudWidth * 0.5, (centerCloudWidth * 0.5) / cloudRatio);

        protected override Rect defaultCloud3Bounds => new Rect(Width / 8, centerCloudTop + ((centerCloudWidth / cloudRatio) * 2) + 10, centerCloudWidth * 0.8, (centerCloudWidth * 0.8) / cloudRatio);

        protected override Rect outSkySphereBounds => new Rect(defaultSkySphereBounds.X, defaultSkySphereBounds.Y - (defaultSkySphereBounds.Height * 0.7), defaultSkySphereBounds.Width, defaultSkySphereBounds.Height);

        public ChristRedeemerIllustration() : base(WonderType.ChristRedeemer)
        {
        }
    }
}
