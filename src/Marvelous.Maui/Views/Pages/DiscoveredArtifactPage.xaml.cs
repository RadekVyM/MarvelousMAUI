using SimpleToolkit.SimpleShell.Extensions;
using SimpleToolkit.SimpleShell.Transitions;
using Marvelous.Core.Interfaces.Services;
using Marvelous.Core.Interfaces.ViewModels;

namespace Marvelous.Maui.Views.Pages;

public partial class DiscoveredArtifactPage : BaseContentPage
{
    private const string AnimateInAnimationKey = "AnimateIn";
    private const uint AnimationLength = 1000;
    private const double CollectionButtonBorderInitialTranslationY = 150;
    private const double LabelsStackLayoutInitialTranslationY = 50;

    private readonly BackgroundDrawable backgroundDrawable;
    private readonly RibbonDrawable ribbonDrawable;


    public DiscoveredArtifactPage(INavigationService navigationService, IDiscoveredArtifactPageViewModel viewModel) : base(navigationService)
	{
		BindingContext = viewModel;

		InitializeComponent();

        App.Current.Resources.TryGetValue("PrimaryColor", out object primaryColor);

        backgroundGraphicsView.Drawable = backgroundDrawable = new BackgroundDrawable
        {
            Color = primaryColor as Color,
            BackgroundColor = Color.FromArgb("#221d19"),
            AppBarHeight = appBar.HeightRequest
        };
        arifactDiscoveredGraphicsView.Drawable = ribbonDrawable = new RibbonDrawable
        {
            Color = primaryColor as Color
        };

        this.SetTransition(
            static args =>
            {
                if (args.TransitionType == SimpleShellTransitionType.Popping)
                {
                    args.OriginPage.Scale = 0.99 + (0.01 * (1 - args.Progress));
                    args.OriginPage.TranslationX = args.Progress * args.OriginPage.Width;
                }
            },
            starting: static args =>
            {
                if (args.TransitionType == SimpleShellTransitionType.Pushing && args.DestinationPage is DiscoveredArtifactPage page)
                    page.AnimateIn();
            },
            finished: static args =>
            {
                args.DestinationPage.Scale = 1;
                args.DestinationPage.TranslationX = 0;
                args.OriginPage.Scale = 1;
                args.OriginPage.TranslationX = 0;
            },
            duration: static args => args.TransitionType == SimpleShellTransitionType.Pushing ? AnimationLength : 250u,
            destinationPageInFront: static args =>
            {
                return args.TransitionType switch
                {
                    SimpleShellTransitionType.Switching => args.OriginPage is not MainMenuPage,
                    SimpleShellTransitionType.Popping => false,
                    _ => true
                };
            });
	}


    private void AnimateIn()
    {
        this.AbortAnimation(AnimateInAnimationKey);

        collectionButtonBorder.TranslationY = CollectionButtonBorderInitialTranslationY;
        labelsStackLayout.TranslationY = LabelsStackLayoutInitialTranslationY;
        labelsStackLayout.Scale = 0;
        arifactDiscoveredContainer.Scale = 0;
        image.Scale = 0;
        menuButtonBorder.Opacity = 0;
        backgroundDrawable.ImageScale = 0;
        backgroundDrawable.BackgroundOpacity = 0;
        backgroundGraphicsView.Invalidate();

        var animation = new Animation();

        animation.Add(0, 0.7, new Animation(d =>
        {
            backgroundDrawable.BackgroundOpacity = d;
            backgroundGraphicsView.Invalidate();
        }, 0, 1, Easing.CubicIn));

        animation.Add(0.3, 1, new Animation(d =>
        {
            image.Scale = d;
            backgroundDrawable.ImageScale = d;
            backgroundGraphicsView.Invalidate();
        }, 0, 1, Easing.SpringOut));

        animation.Add(0.5, 1, new Animation(d =>
        {
            labelsStackLayout.TranslationY = LabelsStackLayoutInitialTranslationY - (d * LabelsStackLayoutInitialTranslationY);
            labelsStackLayout.Scale = d;
            arifactDiscoveredContainer.Scale = d;
        }, 0, 1, Easing.SpringOut));

        animation.Add(0.7, 1, new Animation(d =>
        {
            menuButtonBorder.Opacity = d;
            collectionButtonBorder.TranslationY = CollectionButtonBorderInitialTranslationY - (d * CollectionButtonBorderInitialTranslationY);
        }, 0, 1, Easing.SpringOut));

        animation.Commit(this, AnimateInAnimationKey, length: AnimationLength);
    }

    private void UpdateImageSize()
    {
        var ration = image.Height / image.Width;

        if (image.Height > imageContainer.Height || image.Width > imageContainer.Width)
        {
            image.BatchBegin();

            if (ration > 1)
            {
                image.HeightRequest = imageContainer.Height;
                image.WidthRequest = image.HeightRequest / ration;
            }
            else
            {
                image.WidthRequest = imageContainer.Width;
                image.HeightRequest = image.WidthRequest * ration;
            }

            backgroundDrawable.ImageHeight = image.HeightRequest;
            backgroundDrawable.ImageWidth = image.WidthRequest;

            image.BatchCommit();
        }
    }

    protected override void OnSafeAreaChanged(Thickness safeArea)
    {
        innerGrid.Padding = safeArea;
        backgroundDrawable.SafeArea = safeArea;
        backgroundGraphicsView.Invalidate();
    }

    private void MenuBackButtonClicked(object sender, EventArgs e)
    {
		navigationService.GoBack();
    }

    private void ImageSizeChanged(object sender, EventArgs e)
    {
        backgroundDrawable.ImageHeight = image.Height;
        backgroundDrawable.ImageWidth = image.Width;
        backgroundDrawable.ImageMargin = imageContainer.Margin;

        // TODO: Bug on iOS: image has its real size and overlaps its container
#if IOS
        UpdateImageSize();
#endif

        backgroundGraphicsView.Invalidate();
    }

    private void ArifactDiscoveredLabelSizeChanged(object sender, EventArgs e)
    {
        ribbonDrawable.ArtifactDiscoveredLabelHeight = arifactDiscoveredLabel.Height;
        ribbonDrawable.ArtifactDiscoveredLabelWidth = arifactDiscoveredLabel.Width;
        ribbonDrawable.ArtifactDiscoveredLabelMargin = arifactDiscoveredLabel.Margin;

        backgroundGraphicsView.Invalidate();
    }


    private class RibbonDrawable : IDrawable
    {
        private readonly float ribbonFoldWidth = 20f;
        private readonly float ribbonOverlapCutoutWidth = 22f;
        private readonly float ribbonOverlapWidth = 35f;

        public double ArtifactDiscoveredLabelWidth { get; set; }
        public double ArtifactDiscoveredLabelHeight { get; set; }
        public Thickness ArtifactDiscoveredLabelMargin { get; set; }
        public Color Color { get; set; } = Colors.White;
        public Color DarkerColor => Color.WithSaturation(Color.GetSaturation() - 0.2f).AddLuminosity(-0.2f);


        public void Draw(ICanvas canvas, RectF dirtyRect)
        {
            canvas.SaveState();

            var baseRibbonRect = new Rect(
                (dirtyRect.Width - ArtifactDiscoveredLabelWidth) / 2,
                0,
                ArtifactDiscoveredLabelWidth,
                ArtifactDiscoveredLabelHeight);

            DrawRibbonEnds(
                canvas,
                new Rect(
                    baseRibbonRect.X - ribbonOverlapWidth,
                    ArtifactDiscoveredLabelMargin.VerticalThickness,
                    baseRibbonRect.Width + (2 * ribbonOverlapWidth),
                    baseRibbonRect.Height),
                baseRibbonRect);

            canvas.SetFillPaint(new SolidPaint(Color), baseRibbonRect);
            canvas.FillRectangle(baseRibbonRect);

            canvas.RestoreState();
        }

        private void DrawRibbonEnds(ICanvas canvas, RectF rect, RectF baseRect)
        {
            var overlap = 0.5f;
            var left = baseRect.Left - overlap;
            var right = baseRect.Left + ribbonFoldWidth;

            var leftDarkPath = new PathF()
                .MoveTo(left, rect.Top)
                .LineTo(right, rect.Top)
                .LineTo(right, rect.Bottom)
                .LineTo(left, baseRect.Bottom);
            leftDarkPath.Close();

            var leftLightPath = new PathF()
                .MoveTo(rect.Left, rect.Top)
                .LineTo(right, rect.Top)
                .LineTo(right, rect.Bottom)
                .LineTo(rect.Left, rect.Bottom)
                .LineTo(rect.Left + ribbonOverlapCutoutWidth, rect.Top + (rect.Height / 2));
            leftLightPath.Close();

            left = baseRect.Right - ribbonFoldWidth;
            right = baseRect.Right + overlap;

            var rightDarkPath = new PathF()
                .MoveTo(left, rect.Top)
                .LineTo(right, rect.Top)
                .LineTo(right, baseRect.Bottom)
                .LineTo(left, rect.Bottom);
            rightDarkPath.Close();

            var rightLightPath = new PathF()
                .MoveTo(left, rect.Top)
                .LineTo(rect.Right, rect.Top)
                .LineTo(rect.Right - ribbonOverlapCutoutWidth, rect.Top + (rect.Height / 2))
                .LineTo(rect.Right, rect.Bottom)
                .LineTo(left, rect.Bottom);
            rightLightPath.Close();

            canvas.SetFillPaint(new SolidPaint(Color), rect);
            canvas.FillPath(leftLightPath);
            canvas.FillPath(rightLightPath);

            canvas.SetFillPaint(new SolidPaint(DarkerColor), rect);
            canvas.FillPath(leftDarkPath);
            canvas.FillPath(rightDarkPath);
        }
    }

    private class BackgroundDrawable : IDrawable
    {
        private readonly Thickness imageBorderThickness = new Thickness(5);

        public double ImageScale { get; set; } = 1;
        public double BackgroundOpacity { get; set; } = 1;
        public double AppBarHeight { get; set; }
        public Thickness SafeArea { get; set; }
        public double ImageWidth { get; set; }
        public double ImageHeight { get; set; }
        public Thickness ImageMargin { get; set; }
        public Color Color { get; set; } = Colors.White;
        public Color DarkerColor => Color.WithSaturation(Color.GetSaturation() - 0.2f).AddLuminosity(-0.2f);
        public Color BackgroundColor { get; set; }

        public void Draw(ICanvas canvas, RectF dirtyRect)
        {
            canvas.SaveState();

            canvas.SetFillPaint(new SolidPaint(WithOpacity(BackgroundColor, BackgroundOpacity)), dirtyRect);
            canvas.FillRectangle(dirtyRect);

            var relativeAreaHeight = dirtyRect.Height - 150d - AppBarHeight - SafeArea.VerticalThickness;
            var imageAreaHeight = relativeAreaHeight * (2d / 3d);
            var ribbonTopOffset = imageAreaHeight + AppBarHeight + SafeArea.Top;
            Rect imageRect = GetImageRect(dirtyRect, imageAreaHeight);

            canvas.SetFillPaint(new SolidPaint(Colors.White), imageRect);
            canvas.SetShadow(new SizeF(), 120f, DarkerColor);
            canvas.FillRectangle(imageRect);

            canvas.RestoreState();
        }

        private Rect GetImageRect(RectF dirtyRect, double imageAreaHeight)
        {
            var scale = 1 - ImageScale;
            var width = ImageWidth + imageBorderThickness.HorizontalThickness;
            var height = ImageHeight + imageBorderThickness.VerticalThickness;
            var left = ((dirtyRect.Width - ImageMargin.HorizontalThickness - ImageWidth) / 2) + ImageMargin.Left - imageBorderThickness.Left;
            var top = ((imageAreaHeight - ImageMargin.VerticalThickness - ImageHeight) / 2) + ImageMargin.Top + AppBarHeight + SafeArea.Top - imageBorderThickness.Top;

            return new Rect(
                left + ((width / 2) * scale),
                top + ((height / 2) * scale),
                width - (width * scale),
                height - (height * scale));
        }

        private static Color WithOpacity(Color color, double opacity) =>
            Color.FromRgba(color.Red, color.Green, color.Blue, opacity);
    }
}
