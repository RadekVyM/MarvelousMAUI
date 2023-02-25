using Marvelous.Core.Models;

namespace Marvelous.Maui.Views.Illustrations
{
    public class GreatWallHeaderIllustration : BaseHeaderIllustration
    {
        private double buildingWidth => Width * 0.85;
        private double buildingHeight => Math.Min(buildingWidth / config.MainObjectRatio, Height * 0.9);
        private double sphereSize => Width * 0.23;
        private double cloudWidth => sphereSize * 1.5;
        private double cloudHeight => cloudWidth / cloudRatio;

        protected override Rect defaultMainObjectBounds => new Rect((Width - buildingWidth) / 2, Height - buildingHeight, buildingWidth, buildingHeight);

        protected override Rect defaultSkySphereBounds => new Rect(Width / 9, -5, sphereSize, sphereSize);

        protected override Rect defaultCloud1Bounds => new Rect((Width / 2) - sphereSize + 30, (Height / 10) + 20, cloudWidth * 1.2, (cloudWidth * 1.2) / cloudRatio);

        protected override Rect defaultCloud2Bounds => new Rect(-20, (Height / 10) + sphereSize - cloudHeight - 10, cloudWidth, cloudHeight);

        protected override Rect defaultCloud3Bounds => new Rect(-(cloudWidth * 1.5) + 60, Height / 2, cloudWidth * 1.5, cloudHeight * 1.5);


        public GreatWallHeaderIllustration() : base(WonderType.GreatWall)
        {
        }
    }
}
