using Microsoft.Maui.Controls.Shapes;

namespace Marvelous.Maui.Views.Controls;

public partial class WindowImageView : ContentView
{
    public static readonly BindableProperty VerticalDeltaProperty =
        BindableProperty.Create(nameof(VerticalDelta), typeof(double), typeof(WindowImageView), defaultValue: 0d, propertyChanged: OnVerticalDeltaChanged);
    public static readonly BindableProperty ImageSourceProperty =
        BindableProperty.Create(nameof(ImageSource), typeof(ImageSource), typeof(WindowImageView), propertyChanged: OnImageSourceChanged);
    public static readonly BindableProperty TopTextProperty =
        BindableProperty.Create(nameof(TopText), typeof(string), typeof(WindowImageView), propertyChanged: OnTopTextChanged);
    public static readonly BindableProperty BottomTextProperty =
        BindableProperty.Create(nameof(BottomText), typeof(string), typeof(WindowImageView), propertyChanged: OnBottomTextChanged);

    public virtual double VerticalDelta
    {
        get => (double)GetValue(VerticalDeltaProperty);
        set => SetValue(VerticalDeltaProperty, value);
    }

    public virtual ImageSource ImageSource
    {
        get => (ImageSource)GetValue(ImageSourceProperty);
        set => SetValue(ImageSourceProperty, value);
    }

    public virtual string TopText
    {
        get => (string)GetValue(TopTextProperty);
        set => SetValue(TopTextProperty, value);
    }

    public virtual string BottomText
    {
        get => (string)GetValue(BottomTextProperty);
        set => SetValue(BottomTextProperty, value);
    }


    public WindowImageView()
	{
		InitializeComponent();
	}


    private void OnVerticalDeltaChanged()
    {
        var minScale = 1;
        var maxScale = 1.6;
        var maxScaleVerticalDelta = 2;

        var scale = minScale + (((maxScaleVerticalDelta - Math.Clamp(VerticalDelta, 0, maxScaleVerticalDelta)) / maxScaleVerticalDelta) * (maxScale - minScale));
        
        image.Scale = scale;

        var maxLabelVerticalDelta = 1.5;

        var labelRelativeOffset = (maxLabelVerticalDelta - Math.Clamp(VerticalDelta, 0, maxLabelVerticalDelta)) / maxLabelVerticalDelta;

        topLabel.TranslationY = labelRelativeOffset * ((Height / 2) - topLabel.Height) * -1;
        bottomLabel.TranslationY = labelRelativeOffset * ((Height / 2) - bottomLabel.Height);
    }

    private static void OnVerticalDeltaChanged(BindableObject bindable, object oldValue, object newValue)
    {
        var view = bindable as WindowImageView;

        view.OnVerticalDeltaChanged();
    }

    private static void OnImageSourceChanged(BindableObject bindable, object oldValue, object newValue)
    {
        var view = bindable as WindowImageView;

        view.image.Source = newValue as ImageSource;
    }

    private static void OnTopTextChanged(BindableObject bindable, object oldValue, object newValue)
    {
        var view = bindable as WindowImageView;

        view.topLabel.Text = newValue?.ToString() ?? "";
    }

    private static void OnBottomTextChanged(BindableObject bindable, object oldValue, object newValue)
    {
        var view = bindable as WindowImageView;

        view.bottomLabel.Text = newValue?.ToString() ?? "";
    }

    private void ImageBorderSizeChanged(object sender, EventArgs e)
    {
        var border = sender as Border;

        border.StrokeShape = new RoundRectangle
        {
            CornerRadius = new CornerRadius(border.Width / 2d, border.Width / 2d, 0, 0)
        };

        image.Clip = new RoundRectangleGeometry
        {
            Rect = new Rect(0, 0, image.Width, image.Height),
            CornerRadius = new CornerRadius(image.Width / 2d, image.Width / 2d, 0, 0)
        };
    }

    private void ImageSizeChanged(object sender, EventArgs e)
    {
        var image = sender as Image;

        image.Clip = new RoundRectangleGeometry
        {
            Rect = new Rect(0, 0, image.Width, image.Height),
            CornerRadius = new CornerRadius(image.Width / 2d, image.Width / 2d, 0, 0)
        };
    }

    private void ImageContainerSizeChanged(object sender, EventArgs e)
    {
        var imageContainer = sender as Grid;

        imageContainer.Clip = new RoundRectangleGeometry
        {
            Rect = new Rect(0, 0, imageContainer.Width, imageContainer.Height),
            CornerRadius = new CornerRadius(imageContainer.Width / 2d, imageContainer.Width / 2d, 0, 0)
        };
    }
}