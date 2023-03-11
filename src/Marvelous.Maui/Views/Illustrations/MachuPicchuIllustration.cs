using Marvelous.Core.Models;

namespace Marvelous.Maui.Views.Illustrations
{
    public class MachuPicchuIllustration : BaseIllustration
    {
        private double mountainHeight => mountainWidth / config.MainObjectRatio;
        private double mountainWidth => Math.Max(Height * 0.8 * config.MainObjectRatio, Width * 1.2);
        private double mountainTop => Math.Max((Height - mountainHeight) / 2, 10);
        private double hillHeight => Math.Max(Width / config.ForegroundLeftRatio, Height - hillTop);
        private double hillWidth => hillHeight * config.ForegroundLeftRatio;
        private double hillTop => Height * 0.6;
        private double bushWidth => Math.Min(Width * 1.2, Height * 0.9 * config.ForegroundRightRatio);
        private double sphereSize => mountainWidth * 0.115;
        private double centerCloudWidth => sphereSize * 2.5;
        private double centerCloudTop => mountainTop + 90;

        protected override Rect defaultMainObjectBounds => new Rect(((Width - mountainWidth) / 2) - (Width / 10), mountainTop, mountainWidth, mountainHeight);

        protected override Rect defaultSkySphereBounds => new Rect((Width * 0.75) - (sphereSize / 2), mountainTop, sphereSize, sphereSize);

        protected override Rect defaultForegroundLeftBounds => new Rect((Width - hillWidth) / 2, hillTop, hillWidth, hillHeight);

        protected override Rect defaultForegroundRightBounds => new Rect(bushWidth * -0.45, Height - (bushWidth * 0.5), bushWidth, bushWidth / config.ForegroundRightRatio);

        protected override Rect defaultCloud1Bounds => new Rect((Width - centerCloudWidth) / 2, centerCloudTop, centerCloudWidth, centerCloudWidth / CloudRatio);

        protected override Rect defaultCloud2Bounds => new Rect(Width * 0.7, centerCloudTop - (centerCloudWidth / CloudRatio) + 15, centerCloudWidth * 0.9, (centerCloudWidth * 0.9) / CloudRatio);

        protected override Rect defaultCloud3Bounds => new Rect(Width / 3, Height * 0.4, centerCloudWidth * 0.7, centerCloudWidth * 0.7);

        protected override Rect outForegroundLeftBounds => new Rect(defaultForegroundLeftBounds.X, defaultForegroundLeftBounds.Y + 40, defaultForegroundLeftBounds.Width, defaultForegroundLeftBounds.Height);
        
        protected override Rect outForegroundRightBounds => new Rect(-defaultForegroundRightBounds.Width, defaultForegroundRightBounds.Y, defaultForegroundRightBounds.Width, defaultForegroundRightBounds.Height);

        protected override Rect outSkySphereBounds => new Rect(defaultSkySphereBounds.X, defaultSkySphereBounds.Y + defaultSkySphereBounds.Height, defaultSkySphereBounds.Width, defaultSkySphereBounds.Height);

        public MachuPicchuIllustration() : base(WonderType.MachuPicchu)
        {
        }
    }
}
