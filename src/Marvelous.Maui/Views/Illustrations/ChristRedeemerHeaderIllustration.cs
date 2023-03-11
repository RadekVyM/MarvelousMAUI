using Marvelous.Core.Models;

namespace Marvelous.Maui.Views.Illustrations
{
    public class ChristRedeemerHeaderIllustration : BaseHeaderIllustration
    {
        private double christVisibleHeight => Height * 0.8;
        private double christHeight => christVisibleHeight / 0.45;
        private double christWidth => christHeight * config.MainObjectRatio;
        private double sphereSize => Width * 0.37;
        private double centerCloudWidth => sphereSize * 1.3;
        private double centerCloudTop => Height * 0.26;

        protected override Rect defaultMainObjectBounds => new Rect((Width - christWidth) / 2, Height - christVisibleHeight, christWidth, christHeight);

        protected override Rect defaultSkySphereBounds => new Rect((Width / 2) - 20, -3, sphereSize, sphereSize);

        protected override Rect defaultCloud1Bounds => new Rect((Width - centerCloudWidth) * 0.4, centerCloudTop, centerCloudWidth, centerCloudWidth / CloudRatio);

        protected override Rect defaultCloud2Bounds => new Rect(-20, centerCloudTop + (centerCloudWidth / CloudRatio) + 10, centerCloudWidth * 0.5, (centerCloudWidth * 0.5) / CloudRatio);

        protected override Rect defaultCloud3Bounds => new Rect(Width / 8, centerCloudTop + ((centerCloudWidth / CloudRatio) * 2) + 10, centerCloudWidth * 0.8, (centerCloudWidth * 0.8) / CloudRatio);

        protected override Rect backgroundBounds => new Rect(0, 0, Width, Height * 0.95);


        public ChristRedeemerHeaderIllustration() : base(WonderType.ChristRedeemer)
        {
        }
    }
}
