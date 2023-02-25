using Microsoft.Maui.Controls;
using Microsoft.Maui.Controls.Shapes;
using SimpleToolkit.Core;
using Marvelous.Core.Models;
using Marvelous.Maui.Extensions;
using Marvelous.Maui.Views.Controls;

namespace Marvelous.Maui.Views.Illustrations
{
    public abstract class BaseIllustration : AbsoluteLayout
    {
        private const string AnimatePositionToDefaultKey = "AnimatePositionToDefault";
        private const string AnimateForegroundScaleToDefaultKey = "AnimateForegroundScaleToDefaul";
        private const string AnimateInKey = "AnimateIn";
        private const string AnimateOutKey = "AnimateOut";
        private const string AnimateMainObjectInKey = "AnimateMainObjectInKey";
        private const string AnimateMainObjectOutKey = "AnimateMainObjectOut";
        private const uint AnimatePositionToDefaultLength = 200;
        private const uint AnimateForegroundScaleToDefaultLength = 200;
        public const uint AnimateInLength = 500;
        public const uint AnimateOutLength = 500;
        public const uint AnimateMainObjectInLength = 400;
        public const uint AnimateMainObjectOutLength = 400;

        protected double offset;
        protected double foregroundScale;
        protected Thickness safeArea;

        protected Image mainObject;
        protected Image skySphere;
        protected Image foregroundLeft;
        protected Image foregroundRight;
        protected Image topLeft;
        protected Image topRight;
        protected Image cloud1;
        protected Image cloud2;
        protected Image cloud3;
        protected Rectangle gradientRect;
        protected WonderTitle illustrationTitle;

        protected abstract Rect defaultMainObjectBounds { get; }
        protected abstract Rect defaultSkySphereBounds { get; }
        protected abstract Rect defaultForegroundLeftBounds { get; }
        protected abstract Rect defaultForegroundRightBounds { get; }
        protected virtual Rect defaultTopLeftBounds => new Rect();
        protected virtual Rect defaultTopRightBounds => new Rect();
        protected abstract Rect defaultCloud1Bounds { get; }
        protected abstract Rect defaultCloud2Bounds { get; }
        protected abstract Rect defaultCloud3Bounds { get; }
        protected virtual Rect gradientRectBounds => new Rect(0, Height / 2, Width, Height / 2);
        protected virtual Rect illustrationTitleBounds => new Rect(safeArea.Left, (Height * 0.6d) - safeArea.Bottom, Width - safeArea.HorizontalThickness, Height * 0.4d);

        protected virtual Rect outSkySphereBounds => defaultSkySphereBounds;
        protected virtual Rect outForegroundLeftBounds => new Rect(-defaultForegroundLeftBounds.Width, defaultForegroundLeftBounds.Y, defaultForegroundLeftBounds.Width, defaultForegroundLeftBounds.Height);
        protected virtual Rect outForegroundRightBounds => new Rect(Width, defaultForegroundRightBounds.Y, defaultForegroundRightBounds.Width, defaultForegroundRightBounds.Height);
        protected virtual Rect outTopLeftBounds => new Rect(-defaultTopLeftBounds.Width, defaultTopLeftBounds.Y, defaultTopLeftBounds.Width, defaultTopLeftBounds.Height);
        protected virtual Rect outTopRightBounds => new Rect(Width, defaultTopRightBounds.Y, defaultTopRightBounds.Width, defaultTopRightBounds.Height);
        protected virtual Rect outCloud1Bounds => new Rect(-defaultCloud1Bounds.Width, defaultCloud1Bounds.Y, defaultCloud1Bounds.Width, defaultCloud1Bounds.Height);
        protected virtual Rect outCloud2Bounds => new Rect(Width, defaultCloud2Bounds.Y, defaultCloud2Bounds.Width, defaultCloud2Bounds.Height);
        protected virtual Rect outCloud3Bounds => new Rect(-defaultCloud3Bounds.Width, defaultCloud3Bounds.Y, defaultCloud3Bounds.Width, defaultCloud3Bounds.Height);

        protected readonly WonderViewConfig config;
        protected readonly double cloudRatio = 5.05455;

        public double Offset 
        {
            get => offset;
            set
            {
                offset = value;
                this.LayoutChildTo(mainObject, new Rect(defaultMainObjectBounds.X + offset, defaultMainObjectBounds.Y, defaultMainObjectBounds.Width, defaultMainObjectBounds.Height));
            }
        }

        public virtual double ForegroundScale
        {
            get => foregroundScale;
            set
            {
                foregroundScale = value;

                if (foregroundLeft is not null)
                    foregroundLeft.Scale = foregroundScale;
                if (foregroundRight is not null)
                    foregroundRight.Scale = foregroundScale;
                if (topLeft is not null)
                    topLeft.Scale = foregroundScale;
                if (topRight is not null)
                    topRight.Scale = foregroundScale;
            }
        }

        public string Title
        {
            get => illustrationTitle?.Title;
            set
            {
                if (illustrationTitle is not null)
                    illustrationTitle.Title = value;
            }
        }


        public BaseIllustration(WonderType wonderType)
        {
            config = WonderViewConfig.GetWonderViewConfig(wonderType);

            CreateViews();
            LayoutViewsToDefaultPosition();
            AddViewsToLayout();

            SizeChanged += BaseIllustrationSizeChanged;
            Loaded += IllustrationsCarouselViewLoaded;
            Unloaded += IllustrationsCarouselViewUnloaded;

            CompressedLayout.SetIsHeadless(this, true);
        }


        public virtual async Task AnimateIn()
        {
            this.AbortAnimation(AnimateInKey);
            this.AbortAnimation(AnimateOutKey);
            this.AbortAnimation(AnimateMainObjectOutKey);

            var animation = new Animation();

            animation.Add(0, 1, CreateOpacityAnimation(0, 1));
            animation.Add(0, 0.8, CreatePositionAnimation(skySphere, outSkySphereBounds, defaultSkySphereBounds));
            animation.Add(0, 0.8, CreatePositionAnimation(foregroundLeft, outForegroundLeftBounds, defaultForegroundLeftBounds));
            animation.Add(0, 0.8, CreatePositionAnimation(foregroundRight, outForegroundRightBounds, defaultForegroundRightBounds));
            if (topLeft is not null)
                animation.Add(0, 0.8, CreatePositionAnimation(topLeft, outTopLeftBounds, defaultTopLeftBounds));
            if (topRight is not null)
                animation.Add(0, 0.8, CreatePositionAnimation(topRight, outTopRightBounds, defaultTopRightBounds));
            animation.Add(0, 1, CreatePositionAnimation(cloud1, outCloud1Bounds, defaultCloud1Bounds));
            animation.Add(0, 1, CreatePositionAnimation(cloud2, outCloud2Bounds, defaultCloud2Bounds));
            animation.Add(0, 1, CreatePositionAnimation(cloud3, outCloud3Bounds, defaultCloud3Bounds));

            animation.Commit(this, AnimateInKey, length: AnimateInLength);

            await Task.Delay((int)AnimateInLength);
        }

        public virtual async Task AnimateOut()
        {
            this.AbortAnimation(AnimateInKey);
            this.AbortAnimation(AnimateOutKey);

            var animation = new Animation();

            animation.Add(0, 1, CreateOpacityAnimation(1, 0));
            animation.Add(0, 1, CreatePositionAnimation(skySphere, defaultSkySphereBounds, outSkySphereBounds));
            animation.Add(0, 1, CreatePositionAnimation(foregroundLeft, defaultForegroundLeftBounds, outForegroundLeftBounds));
            animation.Add(0, 1, CreatePositionAnimation(foregroundRight, defaultForegroundRightBounds, outForegroundRightBounds));
            if (topLeft is not null)
                animation.Add(0, 1, CreatePositionAnimation(topLeft, defaultTopLeftBounds, outTopLeftBounds));
            if (topRight is not null)
                animation.Add(0, 1, CreatePositionAnimation(topRight, defaultTopRightBounds, outTopRightBounds));
            animation.Add(0, 1, CreatePositionAnimation(cloud1, defaultCloud1Bounds, outCloud1Bounds));
            animation.Add(0, 1, CreatePositionAnimation(cloud2, defaultCloud2Bounds, outCloud2Bounds));
            animation.Add(0, 1, CreatePositionAnimation(cloud3, defaultCloud3Bounds, outCloud3Bounds));

            animation.Commit(this, AnimateOutKey, length: AnimateOutLength);

            await Task.Delay((int)AnimateOutLength);
        }

        public virtual void AnimateMainObjectIn(double totalPanX)
        {
            this.AbortAnimation(AnimateMainObjectInKey);
            this.AbortAnimation(AnimateMainObjectOutKey);

            var startOffset = totalPanX < 0 ? this.Width : -this.Width;
            var endOffset = 0;

            var animation = new Animation(d => Offset = d, startOffset, endOffset);

            animation.Commit(this, AnimateMainObjectInKey, length: AnimateMainObjectInLength, finished: (d, b) =>
            {
                if (b)
                    Offset = 0;
            });
        }

        public virtual void AnimateMainObjectOut(double totalPanX)
        {
            this.AbortAnimation(AnimateMainObjectInKey);
            this.AbortAnimation(AnimateMainObjectOutKey);

            var startOffset = Offset;
            var endOffset = totalPanX < 0 ? -this.Width : this.Width;

            var animation = new Animation(d => Offset = d, startOffset, endOffset);

            animation.Commit(this, AnimateMainObjectOutKey, length: AnimateMainObjectOutLength, finished: (d, b) =>
            {
                if (b)
                    Offset = 0;
            });
        }

        public virtual async Task AnimatePositionToDefault()
        {
            mainObject.AbortAnimation(AnimatePositionToDefaultKey);

            var currentOffset = mainObject.X - defaultMainObjectBounds.X;

            var animation = new Animation(d =>
            {
                this.LayoutChildTo(mainObject, new Rect(defaultMainObjectBounds.X + (currentOffset * d), defaultMainObjectBounds.Y, defaultMainObjectBounds.Width, defaultMainObjectBounds.Height));
            }, 1, 0);

            animation.Commit(mainObject, AnimatePositionToDefaultKey, length: AnimatePositionToDefaultLength);

            await Task.Delay((int)AnimatePositionToDefaultLength);
        }

        public virtual async Task AnimateForegroundScaleToDefault()
        {
            this.AbortAnimation(AnimateForegroundScaleToDefaultKey);

            var animation = new Animation(d =>
            {
                ForegroundScale = d;
            }, ForegroundScale, 1);

            animation.Commit(this, AnimateForegroundScaleToDefaultKey, length: AnimateForegroundScaleToDefaultLength);

            await Task.Delay((int)AnimateForegroundScaleToDefaultLength);
        }

        public void LayoutViewsToDefaultPosition()
        {
            this.LayoutChildTo(mainObject, defaultMainObjectBounds);
            this.LayoutChildTo(skySphere, defaultSkySphereBounds);
            this.LayoutChildTo(foregroundLeft, defaultForegroundLeftBounds);
            this.LayoutChildTo(foregroundRight, defaultForegroundRightBounds);
            this.LayoutChildTo(topLeft, defaultTopLeftBounds);
            this.LayoutChildTo(topRight, defaultTopRightBounds);
            this.LayoutChildTo(cloud1, defaultCloud1Bounds);
            this.LayoutChildTo(cloud2, defaultCloud2Bounds);
            this.LayoutChildTo(cloud3, defaultCloud3Bounds);
            this.LayoutChildTo(gradientRect, gradientRectBounds);
            this.LayoutChildTo(illustrationTitle, illustrationTitleBounds);
        }

        private Animation CreateOpacityAnimation(double from, double to)
        {
            return new Animation(d =>
            {
                this.Opacity = d;
                skySphere.Opacity = d;
            }, from, to, finished: () =>
            {
                this.Opacity = to;
                skySphere.Opacity = to;
            });
        }

        private Animation CreatePositionAnimation(View view, Rect fromRect, Rect toRect)
        {
            var currentOffsetX = fromRect.X - toRect.X;
            var currentOffsetY = fromRect.Y - toRect.Y;

            return new Animation(d =>
            {
                this.LayoutChildTo(view, new Rect(toRect.X + (currentOffsetX * d), toRect.Y + (currentOffsetY * d), toRect.Width, toRect.Height));
            }, 1, 0, finished: () =>
            {
                this.LayoutChildTo(view, new Rect(toRect.X, toRect.Y, toRect.Width, toRect.Height));
            });
        }     

        protected virtual void AddViewsToLayout()
        {
            Children.Add(skySphere);
            Children.Add(cloud1);
            Children.Add(cloud2);
            Children.Add(cloud3);
            Children.Add(mainObject);
            if (topLeft is not null)
                Children.Add(topLeft);
            if (topRight is not null)
                Children.Add(topRight);
            Children.Add(foregroundLeft);
            Children.Add(foregroundRight);
            Children.Add(gradientRect);
            Children.Add(illustrationTitle);
        }

        protected virtual void CreateViews()
        {
            mainObject = CreateImage(config.MainObject);
            skySphere = CreateImage(config.SkySphere);
            foregroundLeft = CreateImage(config.ForegroundLeft);
            foregroundRight = CreateImage(config.ForegroundRight);
            topLeft = CreateImage(config.TopLeft);
            topRight = CreateImage(config.TopRight);
            cloud1 = CreateImage("common_cloud_white.png");
            cloud2 = CreateImage("common_cloud_white.png");
            cloud3 = CreateImage("common_cloud_white.png");

            var cloudOpacity = 0.4;

            cloud1.Opacity = cloudOpacity;
            cloud2.Opacity = cloudOpacity;
            cloud3.Opacity = cloudOpacity;

            var gradient = new LinearGradientBrush(new GradientStopCollection
            {
                new GradientStop(Color.FromRgba(config.PrimaryColor.Red, config.PrimaryColor.Green, config.PrimaryColor.Blue, 0), 0),
                new GradientStop(config.PrimaryColor, 1),
            }, new Point(0, 0), new Point(0, 1));

            gradientRect = new Rectangle
            {
                Fill = gradient,
                Margin = -1
            };

            illustrationTitle = new WonderTitle();
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

        private void IllustrationsCarouselViewUnloaded(object sender, EventArgs e)
        {
            this.Window.UnsubscribeFromSafeAreaChanges(OnSafeAreaChanged);
        }

        private async void IllustrationsCarouselViewLoaded(object sender, EventArgs e)
        {
            this.Window.SubscribeToSafeAreaChanges(OnSafeAreaChanged);

#if ANDROID
            // TODO: Workaround of a bug with wrong initial sizing of the label
            await Task.Delay(100);

            this.LayoutChildTo(illustrationTitle, new Rect(1, 1, 1, 1));
            this.LayoutChildTo(illustrationTitle, illustrationTitleBounds);
#endif
        }

        private void BaseIllustrationSizeChanged(object sender, EventArgs e)
        {
            LayoutViewsToDefaultPosition();
        }

        private void OnSafeAreaChanged(Thickness safeArea)
        {
            this.safeArea = safeArea;
            this.LayoutChildTo(illustrationTitle, illustrationTitleBounds);
        }
    }
}
