using Marvelous.Core.Models;

namespace Marvelous.Maui.Views.Illustrations
{
    public class PetraIllustration : BaseIllustration
    {
        private double buildingHeight => Height * 0.70;
        private double buildingWidth => buildingHeight * config.MainObjectRatio;
        private double buildingTop => (Height - buildingHeight) / 3;
        private double sphereSize => buildingWidth * 0.115;
        private double cliffHeight => Height;
        private double cloudWidth => sphereSize * 2.1;
        private double cloudHeight => cloudWidth / cloudRatio;

        protected override Rect defaultMainObjectBounds => new Rect((Width - buildingWidth) / 2, buildingTop, buildingWidth, buildingHeight);

        protected override Rect defaultSkySphereBounds => new Rect(sphereSize * 0.9, -10, sphereSize, sphereSize);

        protected override Rect defaultForegroundLeftBounds => new Rect(-0.35 * cliffHeight * config.ForegroundLeftRatio, cliffHeight * -0.01, cliffHeight * config.ForegroundLeftRatio, cliffHeight);

        protected override Rect defaultForegroundRightBounds => new Rect(Width - (0.65 * cliffHeight * config.ForegroundRightRatio), 0, cliffHeight * config.ForegroundRightRatio, cliffHeight);

        protected override Rect defaultCloud1Bounds => new Rect((Width - cloudWidth) / 2, buildingTop - 10, cloudWidth, cloudHeight);

        protected override Rect defaultCloud2Bounds => new Rect((cloudWidth * -0.9) + 10, 30, cloudWidth * 0.9, cloudHeight * 0.9);

        protected override Rect defaultCloud3Bounds => new Rect(Width * 0.7, Height * 0.3, cloudWidth * 0.7, cloudHeight * 0.7);

        protected override Rect outSkySphereBounds => new Rect(defaultSkySphereBounds.X, -defaultSkySphereBounds.Height, defaultSkySphereBounds.Width, defaultSkySphereBounds.Height);

        public PetraIllustration() : base(WonderType.Petra)
        {
        }
    }
}
