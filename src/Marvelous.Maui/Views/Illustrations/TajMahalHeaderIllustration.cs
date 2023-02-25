using Marvelous.Core.Models;

namespace Marvelous.Maui.Views.Illustrations
{
    public class TajMahalHeaderIllustration : BaseHeaderIllustration
    {
        private double buildingHeight => Math.Min(buildingWidth / config.MainObjectRatio, Height * 0.9);
        private double buildingWidth => Width * 1.1;
        private double buildingTop => Height - buildingHeight;
        private double sphereSize => Width * 0.45;
        private double cloudWidth => Width * 0.6;
        private double cloudHeight => cloudWidth / cloudRatio;

        protected override Rect defaultMainObjectBounds => new Rect((Width - buildingWidth) / 2, buildingTop, buildingWidth, buildingHeight);

        protected override Rect defaultSkySphereBounds => new Rect(-(sphereSize * 0.2), -(sphereSize * 0.2), sphereSize, sphereSize);

        protected override Rect defaultCloud1Bounds => new Rect((Width / 2) - cloudWidth + 20, buildingTop + (buildingHeight * 0.25), cloudWidth, cloudHeight);

        protected override Rect defaultCloud2Bounds => new Rect(-cloudWidth + 10, sphereSize * 0.4, cloudWidth, cloudHeight);

        protected override Rect defaultCloud3Bounds => new Rect(Width / 2, buildingTop + 10, cloudWidth * 0.8, cloudHeight * 0.8);


        public TajMahalHeaderIllustration() : base(WonderType.TajMahal)
        {
        }
    }
}
