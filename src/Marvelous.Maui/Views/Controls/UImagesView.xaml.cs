using Microsoft.Maui.Controls.Shapes;
using Microsoft.Maui.Graphics;

namespace Marvelous.Maui.Views.Controls;

public partial class UImagesView : ContentView
{
    public static readonly BindableProperty VerticalDeltaProperty =
        BindableProperty.Create(nameof(VerticalDelta), typeof(double), typeof(UImagesView), defaultValue: 0d, propertyChanged: OnVerticalDeltaChanged);
    public static readonly BindableProperty LeftImageSourceProperty =
        BindableProperty.Create(nameof(LeftImageSource), typeof(ImageSource), typeof(UImagesView), propertyChanged: OnLeftImageSourceChanged);
    public static readonly BindableProperty RightImageSourceProperty =
        BindableProperty.Create(nameof(RightImageSource), typeof(ImageSource), typeof(UImagesView), propertyChanged: OnRightImageSourceChanged);

    public virtual double VerticalDelta
    {
        get => (double)GetValue(VerticalDeltaProperty);
        set => SetValue(VerticalDeltaProperty, value);
    }

    public virtual ImageSource LeftImageSource
    {
        get => (ImageSource)GetValue(LeftImageSourceProperty);
        set => SetValue(LeftImageSourceProperty, value);
    }

    public virtual ImageSource RightImageSource
    {
        get => (ImageSource)GetValue(RightImageSourceProperty);
        set => SetValue(RightImageSourceProperty, value);
    }


    public UImagesView()
	{
        App.Current.Resources.TryGetValue("CreamyGray", out object color);

		InitializeComponent();

        graphicsView.Drawable = new PillDrawable
        {
            Color = color as Color
        };  
    }


    private void UpdateClipping()
    {
        var leftHeight = Height * 0.5;
        var rightHeight = Height * 0.8;
        var minDelta = 0.15d;
        var maxDelta = 1.2d;
        var delta = (Math.Clamp(VerticalDelta, minDelta, maxDelta) - minDelta) / (maxDelta - minDelta);
        var leftMinTop = (leftImage.Height - leftHeight) * 0.7d;

        var leftGeometry = new RoundRectangleGeometry
        {
            CornerRadius = new CornerRadius(0, 0, leftImage.Width / 2, leftImage.Width / 2),
            Rect = new Rect(0, leftMinTop + ((1 - delta) * (leftImage.Height - leftHeight - leftMinTop)), leftImage.Width, leftHeight)
        };
        var rightGeometry = new RoundRectangleGeometry
        {
            CornerRadius = new CornerRadius(rightImage.Width / 2, rightImage.Width / 2, 0, 0),
            Rect = new Rect(0, delta * (rightImage.Height - rightHeight), rightImage.Width, rightHeight)
        };

        leftImage.Clip = leftGeometry;
        rightImage.Clip = rightGeometry;
    }

    private void ImageSizeChanged(object sender, EventArgs e)
    {
        UpdateClipping();
    }

    private static void OnVerticalDeltaChanged(BindableObject bindable, object oldValue, object newValue)
    {
        var view = bindable as UImagesView;

        view.UpdateClipping();
    }

    private static void OnLeftImageSourceChanged(BindableObject bindable, object oldValue, object newValue)
    {
        var view = bindable as UImagesView;

        view.leftImage.Source = newValue as ImageSource;
    }

    private static void OnRightImageSourceChanged(BindableObject bindable, object oldValue, object newValue)
    {
        var view = bindable as UImagesView;

        view.rightImage.Source = newValue as ImageSource;
    }

    private class PillDrawable : IDrawable
    {
        private const float MaxRadius = 100f;
        private const float VerticalOffset = 20f;

        public Color Color { get; set; }

        public void Draw(ICanvas canvas, RectF dirtyRect)
        {
            canvas.SaveState();

            var radius = Math.Min(dirtyRect.Height / 2, Math.Min(MaxRadius, dirtyRect.Width / 2));
            var leftCenter = new PointF(radius, dirtyRect.Height - radius - VerticalOffset);
            var rightCenter = new PointF(dirtyRect.Width - radius, radius + VerticalOffset);

            var vec = rightCenter - leftCenter;
            var leftVector = GetRightAngleVector(vec, true, radius);
            var rigthVector = GetRightAngleVector(vec, false, radius);

            var angle = (float)(Math.Acos(Math.Abs(leftVector.Width) / radius) / Math.PI) * 180f;

            var path = new PathF()
                .MoveTo(leftCenter + leftVector)
                .LineTo(rightCenter + leftVector)
                .AddArc(rightCenter.X - radius, rightCenter.Y - radius, rightCenter.X + radius, rightCenter.Y + radius, 180f - angle, 360f - angle, true)
                .LineTo(leftCenter + rigthVector)
                .AddArc(leftCenter.X - radius, leftCenter.Y - radius, leftCenter.X + radius, leftCenter.Y + radius, 360f - angle, 180f - angle, true);
            path.Close();

            canvas.StrokeColor = Color;
            canvas.StrokeSize = 1.5f;

            canvas.DrawPath(path);

            canvas.RestoreState();
        }

        private SizeF GetRightAngleVector(SizeF vec, bool left, float length)
        {
            var rightVec = new SizeF(left ? vec.Height : -vec.Height, left ? -vec.Width : vec.Width);
            var currentLength = Math.Sqrt(Math.Pow(rightVec.Width, 2) + Math.Pow(rightVec.Height, 2));
            var scale = currentLength / length;

            return new Size(rightVec.Width / scale, rightVec.Height / scale);
        }
    }
}