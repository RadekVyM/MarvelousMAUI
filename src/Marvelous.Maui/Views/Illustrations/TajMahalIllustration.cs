using Marvelous.Core.Models;

namespace Marvelous.Maui.Views.Illustrations
{
    public class TajMahalIllustration : BaseIllustration
    {
        private double buildingHeight => buildingWidth / config.MainObjectRatio;
        private double buildingWidth => Math.Max(Height * 0.5 * config.MainObjectRatio, Width);
        private double buildingTop => (Height - buildingHeight) / 2;
        private double sphereSize => buildingWidth * 0.28;
        private double fruitHeight => Height * 0.55;
        private double poolHeight => Math.Max(((Height - buildingHeight) / 2) + (buildingHeight * 0.05), 2);
        private double poolWidth => poolHeight * config.TopLeftRatio;
        private double cloudWidth => Width * 0.6;
        private double cloudHeight => cloudWidth / cloudRatio;

        protected override Rect defaultMainObjectBounds => new Rect((Width - buildingWidth) / 2, buildingTop, buildingWidth, buildingHeight);

        protected override Rect defaultSkySphereBounds => new Rect(-20, -10, sphereSize, sphereSize);

        protected override Rect defaultTopLeftBounds => new Rect((Width - poolWidth) / 2, defaultMainObjectBounds.Top + (defaultMainObjectBounds.Height * 0.99) - (poolHeight * 0.06), poolWidth, poolHeight);

        protected override Rect defaultForegroundLeftBounds => new Rect((fruitHeight * config.ForegroundLeftRatio) * -0.425, Height * 0.41, fruitHeight * config.ForegroundLeftRatio, fruitHeight);

        protected override Rect defaultForegroundRightBounds => new Rect(Width - (fruitHeight * config.ForegroundRightRatio * 0.575), Height * 0.40, fruitHeight * config.ForegroundRightRatio, fruitHeight);
        
        protected override Rect defaultCloud1Bounds => new Rect((Width / 2) - cloudWidth + 20, buildingTop + 30, cloudWidth, cloudHeight);

        protected override Rect defaultCloud2Bounds => new Rect(-cloudWidth + 10, sphereSize * 0.4, cloudWidth, cloudHeight);

        protected override Rect defaultCloud3Bounds => new Rect(Width / 2, sphereSize * 0.6, cloudWidth * 0.8, cloudHeight * 0.8);

        protected override Rect outTopLeftBounds => new Rect(defaultTopLeftBounds.X, defaultTopLeftBounds.Y + 100, defaultTopLeftBounds.Width, defaultTopLeftBounds.Height);

        protected override Rect outSkySphereBounds => new Rect(defaultSkySphereBounds.X - (defaultSkySphereBounds.Width / 3), defaultSkySphereBounds.Y + (defaultSkySphereBounds.Height / 3), defaultSkySphereBounds.Width, defaultSkySphereBounds.Height);

        public override double ForegroundScale
        {
            get => foregroundScale;
            set
            {
                foregroundScale = value;

                if (foregroundLeft is not null)
                    foregroundLeft.Scale = foregroundScale;
                if (foregroundRight is not null)
                    foregroundRight.Scale = foregroundScale;
            }
        }


        public TajMahalIllustration() : base(WonderType.TajMahal)
        {
        }
    }
}
