using Marvelous.Core.Models;

namespace Marvelous.Maui.Views.Illustrations
{
    public class ColosseumHeaderIllustration : BaseHeaderIllustration
    {
        private double buildingWidth => Width * 0.9;
        private double buildingHeight => Math.Min(buildingWidth / config.MainObjectRatio, Height * 0.8);
        private double sphereSize => Width * 0.32;
        private double centerCloudWidth => sphereSize * 1.5;
        private double centerCloudTop => Height - buildingHeight - 10;

        protected override Rect defaultMainObjectBounds => new Rect((Width - buildingWidth) / 2, Height - buildingHeight, buildingWidth, buildingHeight);

        protected override Rect defaultSkySphereBounds => new Rect(Width / 9, Height * 0.05, sphereSize, sphereSize);

        protected override Rect defaultCloud1Bounds => new Rect((Width - centerCloudWidth) / 2, centerCloudTop, centerCloudWidth, centerCloudWidth / cloudRatio);

        protected override Rect defaultCloud2Bounds => new Rect(-20, centerCloudTop + (centerCloudWidth / cloudRatio) + 20, centerCloudWidth * 0.7, (centerCloudWidth * 0.7) / cloudRatio);

        protected override Rect defaultCloud3Bounds => new Rect(Width * 0.8, defaultCloud2Bounds.Top + defaultCloud2Bounds.Height + 5, centerCloudWidth * 0.8, (centerCloudWidth * 0.8) / cloudRatio);


        public ColosseumHeaderIllustration() : base(WonderType.Colosseum)
        {
        }
    }
}
