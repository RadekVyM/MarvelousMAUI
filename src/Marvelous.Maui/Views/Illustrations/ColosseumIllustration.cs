using Marvelous.Core.Models;

namespace Marvelous.Maui.Views.Illustrations
{
    public class ColosseumIllustration : BaseIllustration
    {
        private double buildingHeight => Height * 0.55;
        private double buildingWidth => buildingHeight * config.MainObjectRatio;
        private double buildingTop => (Height - buildingHeight) / 2;
        private double sphereSize => buildingWidth * 0.3;
        private double flowersHeight => Height * 0.5;
        private double centerCloudWidth => sphereSize * 1.3;
        private double centerCloudTop => buildingTop - 40;

        protected override Rect defaultMainObjectBounds => new Rect((Width - buildingWidth) / 2, buildingTop, buildingWidth, buildingHeight);

        protected override Rect defaultSkySphereBounds => new Rect(Width / 9, buildingTop - 70, sphereSize, sphereSize);

        protected override Rect defaultForegroundLeftBounds => new Rect(flowersHeight * config.ForegroundLeftRatio * -0.15, Height * 0.5, flowersHeight * config.ForegroundLeftRatio, flowersHeight);

        protected override Rect defaultForegroundRightBounds => new Rect(Width - (flowersHeight * config.ForegroundRightRatio * 0.85), Height * 0.6, flowersHeight * config.ForegroundRightRatio, flowersHeight);

        protected override Rect defaultCloud1Bounds => new Rect((Width - centerCloudWidth) / 2, centerCloudTop, centerCloudWidth, centerCloudWidth / cloudRatio);

        protected override Rect defaultCloud2Bounds => new Rect(-20, centerCloudTop + (centerCloudWidth / cloudRatio) + 30, centerCloudWidth * 0.7, (centerCloudWidth * 0.7) / cloudRatio);

        protected override Rect defaultCloud3Bounds => new Rect(Width / 8, centerCloudTop + ((centerCloudWidth / cloudRatio) * 2) + 80, centerCloudWidth * 0.8, (centerCloudWidth * 0.8) / cloudRatio);

        protected override Rect outSkySphereBounds => new Rect(defaultSkySphereBounds.X, defaultSkySphereBounds.Y + (defaultSkySphereBounds.Height * 0.7), defaultSkySphereBounds.Width, defaultSkySphereBounds.Height);

        public ColosseumIllustration() : base(WonderType.Colosseum)
        {
        }
    }
}
