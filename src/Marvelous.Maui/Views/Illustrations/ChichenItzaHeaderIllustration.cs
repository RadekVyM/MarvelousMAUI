using Marvelous.Core.Models;

namespace Marvelous.Maui.Views.Illustrations
{
    public class ChichenItzaHeaderIllustration : BaseHeaderIllustration
    {
        private double mainObjectWidth => Width * 1.4;
        private double mainObjectTop => Height - (mainObjectWidth / config.MainObjectRatio);
        private double skySphereWidth => Width * 0.38;
        private double cloudWidth => Width * 0.6;
        private double cloudHeight => cloudWidth / CloudRatio;

        protected override Rect defaultMainObjectBounds => new Rect((Width - mainObjectWidth) / 2, mainObjectTop, mainObjectWidth, mainObjectWidth / config.MainObjectRatio);

        protected override Rect defaultSkySphereBounds => new Rect((Width / 2) - (skySphereWidth / 4), backgroundBounds.Bottom - (skySphereWidth / config.SkySphereRatio), skySphereWidth, skySphereWidth / config.SkySphereRatio);

        protected override Rect defaultCloud1Bounds => new Rect(-20, mainObjectTop - 10, cloudWidth, cloudHeight);

        protected override Rect defaultCloud2Bounds => new Rect(Width * 0.6, Height * 0.25, cloudWidth * 0.8, cloudHeight * 0.8);

        protected override Rect defaultCloud3Bounds => new Rect((Width / 2) - (cloudWidth / 2), 20, cloudWidth * 0.6, cloudHeight * 0.6);


        public ChichenItzaHeaderIllustration() : base(WonderType.ChichenItza)
        {
        }
    }
}
