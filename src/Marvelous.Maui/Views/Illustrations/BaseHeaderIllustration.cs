using Microsoft.Maui.Controls.Shapes;
using Marvelous.Core.Models;
using Marvelous.Maui.Extensions;

namespace Marvelous.Maui.Views.Illustrations
{
    public abstract class BaseHeaderIllustration : AbsoluteLayout
    {
        protected readonly WonderViewConfig config;
        protected readonly double cloudRatio = 5.05455;

        protected Image mainObject;
        protected Image skySphere;
        protected Image cloud1;
        protected Image cloud2;
        protected Image cloud3;
        protected Rectangle background;

        protected abstract Rect defaultMainObjectBounds { get; }
        protected abstract Rect defaultSkySphereBounds { get; }
        protected abstract Rect defaultCloud1Bounds { get; }
        protected abstract Rect defaultCloud2Bounds { get; }
        protected abstract Rect defaultCloud3Bounds { get; }
        protected virtual Rect backgroundBounds => new Rect(0, 0, Width, Height * 0.9);


        public BaseHeaderIllustration(WonderType wonderType)
        {
            IsClippedToBounds = true;

            config = WonderViewConfig.GetWonderViewConfig(wonderType);

            CreateViews();
            LayoutViewsToDefaultPosition();
            AddViewsToLayout();

            SizeChanged += BaseIllustrationSizeChanged;

            CompressedLayout.SetIsHeadless(this, true);
        }


        public void LayoutViewsToDefaultPosition()
        {
            this.LayoutChildTo(mainObject, defaultMainObjectBounds);
            this.LayoutChildTo(skySphere, defaultSkySphereBounds);
            this.LayoutChildTo(cloud1, defaultCloud1Bounds);
            this.LayoutChildTo(cloud2, defaultCloud2Bounds);
            this.LayoutChildTo(cloud3, defaultCloud3Bounds);
            this.LayoutChildTo(background, backgroundBounds);
        }

        protected virtual void AddViewsToLayout()
        {
            Children.Add(background);
            Children.Add(skySphere);
            Children.Add(cloud1);
            Children.Add(cloud2);
            Children.Add(cloud3);
            Children.Add(mainObject);
        }

        protected virtual void CreateViews()
        {
            mainObject = CreateImage(config.MainObject);
            skySphere = CreateImage(config.SkySphere);
            cloud1 = CreateImage("common_cloud_white.png");
            cloud2 = CreateImage("common_cloud_white.png");
            cloud3 = CreateImage("common_cloud_white.png");

            var cloudOpacity = 0.45;

            cloud1.Opacity = cloudOpacity;
            cloud2.Opacity = cloudOpacity;
            cloud3.Opacity = cloudOpacity;

            background = new Rectangle
            {
                Fill = config.SecondaryColor,
                Margin = -1
            };
        }

        protected Image CreateImage(string path)
        {
            if (string.IsNullOrWhiteSpace(path))
                return null;

            return new Image
            {
                Source = path,
                Aspect = Aspect.AspectFit,
            };
        }

        private void BaseIllustrationSizeChanged(object sender, EventArgs e)
        {
            LayoutViewsToDefaultPosition();
        }
    }
}
