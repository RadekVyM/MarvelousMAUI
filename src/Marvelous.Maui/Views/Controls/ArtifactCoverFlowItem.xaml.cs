using Microsoft.Maui.Controls.Shapes;
using Marvelous.Maui.Extensions;

namespace Marvelous.Maui.Views.Controls;

public partial class ArtifactCoverFlowItem : ContentView
{
    private const string ImageScaleAnimationKey = "ImageScaleAnimation";
    private const double MinImageHeightScale = 0.5d;
    private const double MaxImageHeightScale = 1d;
    private const double MinImageWidthScale = 0.7d;
    private const double MaxImageWidthScale = 1d;
    private const double MinImageBottomOffset = 0d;
    private const double MaxImageBottomOffset = 0.05d;
    private const double ImageBorderPadding = 5d;

    private BorderDrawable borderDrawable;
    private double realImageScale;

    public static readonly BindableProperty ImageScaleProperty =
        BindableProperty.Create(nameof(ImageScale), typeof(double), typeof(ArtifactCoverFlowItem), 1d, BindingMode.OneWay, propertyChanged: OnImageScalePropertyChanged);

    public static readonly BindableProperty IsImageScaleAnimatedProperty =
        BindableProperty.Create(nameof(IsImageScaleAnimated), typeof(bool), typeof(ArtifactCoverFlowItem), false, BindingMode.OneWay);

    public double ImageScale
    {
        get => (double)GetValue(ImageScaleProperty);
        set => SetValue(ImageScaleProperty, value);
    }

    public bool IsImageScaleAnimated
    {
        get => (bool)GetValue(IsImageScaleAnimatedProperty);
        set => SetValue(IsImageScaleAnimatedProperty, value);
    }


    public ArtifactCoverFlowItem()
    {
        InitializeComponent();

        borderGraphicsView.Drawable = borderDrawable = new BorderDrawable();

        absoluteLayout.SizeChanged += ArtifactCoverFlowItemSizeChanged;
    }


    public void AnimateImageScale(double toImageScale)
    {
        System.Diagnostics.Debug.WriteLine(realImageScale);

        this.AbortAnimation(ImageScaleAnimationKey);
        var animation = new Animation(d =>
        {
            UpdateImage(d);
            realImageScale = d;
        }, realImageScale, toImageScale);
        animation.Commit(this, ImageScaleAnimationKey);
    }

    private void UpdateImage(double scale)
    {
        UpdateImageSize(scale);
        UpdateImageBorder(scale);
        borderGraphicsView.Invalidate();
    }

    private void UpdateImageSize(double scale)
    {
        var rect = GetBorderRect(scale);
        var doublePadding = 2 * ImageBorderPadding;
        var width = rect.Width - doublePadding;
        var height = rect.Height - doublePadding;

        absoluteLayout.LayoutChildTo(imageBorder, new Rect(rect.Left + ImageBorderPadding, rect.Top + ImageBorderPadding, width, height));
    }

    private void UpdateImageBorder(double scale)
    {
        var rect = GetBorderRect(scale);
        var outerCornerRadius = Math.Min(rect.Width, rect.Height) / 2;

        borderDrawable.Rect = rect;
        borderDrawable.Radius = outerCornerRadius;
        imageBorder.StrokeShape = new RoundRectangle
        {
            CornerRadius = outerCornerRadius - ImageBorderPadding
        };
    }

    private Rect GetBorderRect(double scale)
    {
        var widthScale = ((MaxImageWidthScale - MinImageWidthScale) * scale) + MinImageWidthScale;
        var heigthScale = ((MaxImageHeightScale - MinImageHeightScale) * scale) + MinImageHeightScale;
        var bottomOffset = ((MaxImageBottomOffset - MinImageBottomOffset) * scale) + MinImageBottomOffset;
        bottomOffset = absoluteLayout.Height * bottomOffset;

        var width = absoluteLayout.Width * widthScale;
        var height = absoluteLayout.Height * heigthScale - bottomOffset;
        var left = (absoluteLayout.Width - width) / 2;
        var top = absoluteLayout.Height - height - bottomOffset;

        return new Rect(left, top, width, height);
    }

    private void ArtifactCoverFlowItemSizeChanged(object sender, EventArgs e)
    {
        absoluteLayout.LayoutChildTo(borderGraphicsView, new Rect(0, 0, absoluteLayout.Width, absoluteLayout.Height));
        UpdateImage(ImageScale);
    }


    private static void OnImageScalePropertyChanged(BindableObject bindable, object oldValue, object newValue)
    {
        var item = bindable as ArtifactCoverFlowItem;

        if (oldValue == newValue)
            return;

        if (item.IsImageScaleAnimated)
        {
            item.AnimateImageScale((double)newValue);
        }
        else
        {
            item.realImageScale = (double)newValue;
            item.UpdateImage(item.realImageScale);
        }
    }

    private class BorderDrawable : IDrawable
    {
        public Rect Rect { get; set; }
        public double Radius { get; set; }

        public void Draw(ICanvas canvas, RectF dirtyRect)
        {
            canvas.SaveState();

            canvas.StrokeColor = Colors.White;
            canvas.StrokeSize = 1.5f;

            RectF rect = new Rect(Rect.Left + 1, Rect.Top + 1, Rect.Width - 2, Rect.Height - 2);

            canvas.DrawRoundedRectangle(rect, (float)Radius);

            canvas.RestoreState();
        }
    }
}