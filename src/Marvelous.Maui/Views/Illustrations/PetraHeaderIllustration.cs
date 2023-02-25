using Marvelous.Core.Models;

namespace Marvelous.Maui.Views.Illustrations
{
    public class PetraHeaderIllustration : BaseHeaderIllustration
    {
        private double buildingHeight => Math.Min(buildingWidth / config.MainObjectRatio, Height - 10);
        private double buildingWidth => Width * 1.05;
        private double buildingTop => Height - buildingHeight;
        private double sphereSize => Width / 4;
        private double cloudWidth => Width * 0.5;
        private double cloudHeight => cloudWidth / cloudRatio;

        protected override Rect defaultMainObjectBounds => new Rect((Width - buildingWidth) / 2, buildingTop, buildingWidth, buildingHeight);

        protected override Rect defaultSkySphereBounds => new Rect(sphereSize * 0.9, -10, sphereSize, sphereSize);

        protected override Rect defaultCloud1Bounds => new Rect((Width - cloudWidth) / 2, buildingTop - 10, cloudWidth, cloudHeight);

        protected override Rect defaultCloud2Bounds => new Rect((cloudWidth * -0.9) + 10, 30, cloudWidth * 0.9, cloudHeight * 0.9);

        protected override Rect defaultCloud3Bounds => new Rect(Width * 0.7, Height * 0.3, cloudWidth * 0.7, cloudHeight * 0.7);


        public PetraHeaderIllustration() : base(WonderType.Petra)
        {
        }
    }
}
