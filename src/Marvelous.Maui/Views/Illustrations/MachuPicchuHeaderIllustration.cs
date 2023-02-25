using Marvelous.Core.Models;

namespace Marvelous.Maui.Views.Illustrations
{
    public class MachuPicchuHeaderIllustration : BaseHeaderIllustration
    {
        private double mountainHeight => Height * 0.9;
        private double mountainWidth => mountainHeight * config.MainObjectRatio;
        private double mountainTop => Height - mountainHeight;
        private double sphereSize => Width * 0.2;
        private double centerCloudWidth => sphereSize * 2.5;
        private double centerCloudTop => mountainTop + 90;

        protected override Rect defaultMainObjectBounds => new Rect((Width - mountainWidth) / 2, mountainTop, mountainWidth, mountainHeight);

        protected override Rect defaultSkySphereBounds => new Rect((Width * 0.5) - (sphereSize / 2), mountainTop, sphereSize, sphereSize);

        protected override Rect defaultCloud1Bounds => new Rect((Width - centerCloudWidth) / 2, centerCloudTop, centerCloudWidth, centerCloudWidth / cloudRatio);

        protected override Rect defaultCloud2Bounds => new Rect(Width * 0.7, centerCloudTop - (centerCloudWidth / cloudRatio) + 15, centerCloudWidth * 0.9, (centerCloudWidth * 0.9) / cloudRatio);

        protected override Rect defaultCloud3Bounds => new Rect(Width / 3, Height * 0.4, centerCloudWidth * 0.7, centerCloudWidth * 0.7);


        public MachuPicchuHeaderIllustration() : base(WonderType.MachuPicchu)
        {
        }
    }
}
