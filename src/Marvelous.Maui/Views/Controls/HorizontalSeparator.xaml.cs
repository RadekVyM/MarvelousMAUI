namespace Marvelous.Maui.Views.Controls;

public partial class HorizontalSeparator : ContentView
{
    private const string ExpandAnimationKey = "ExpandAnimation";
    private const string CollapseAnimationKey = "CollapseAnimation";
    private const uint AnimationLength = 2000;

    public static readonly BindableProperty IconColorProperty = BindableProperty.Create(nameof(IconColor), typeof(Color), typeof(HorizontalSeparator), defaultValue: null, propertyChanged: OnIconColorChanged);
    public static readonly BindableProperty LineColorProperty = BindableProperty.Create(nameof(LineColor), typeof(Color), typeof(HorizontalSeparator), defaultValue: null, propertyChanged: OnLineColorChanged);
    public static readonly BindableProperty CollapsedProperty = BindableProperty.Create(nameof(Collapsed), typeof(bool), typeof(HorizontalSeparator), defaultValue: false, propertyChanged: OnCollapsedChanged);
    public static readonly BindableProperty IsCollapsedAnimatedProperty = BindableProperty.Create(nameof(IsCollapsedAnimated), typeof(bool), typeof(HorizontalSeparator), defaultValue: true);

    public virtual Color IconColor
    {
        get => (Color)GetValue(IconColorProperty);
        set => SetValue(IconColorProperty, value);
    }

    public virtual Color LineColor
    {
        get => (Color)GetValue(LineColorProperty);
        set => SetValue(LineColorProperty, value);
    }

    public virtual bool Collapsed
    {
        get => (bool)GetValue(CollapsedProperty);
        set => SetValue(CollapsedProperty, value);
    }

    public virtual bool IsCollapsedAnimated
    {
        get => (bool)GetValue(IsCollapsedAnimatedProperty);
        set => SetValue(IsCollapsedAnimatedProperty, value);
    }


    public HorizontalSeparator()
	{
		InitializeComponent();
	}


    public void Expand()
    {
        this.AbortAnimation(ExpandAnimationKey);

        if (!IsCollapsedAnimated)
        {
            icon.Rotation = 0;
            leftRect.ScaleX = 1;
            rightRect.ScaleX = 1;
            return;
        }

        var animation = new Animation();

        animation.Add(0, 1, new Animation(d =>
        {
            icon.Rotation = d;
        }, 0, 270, easing: Easing.SpringOut, finished: () =>
        {
            icon.Rotation = 0;
        }));

        animation.Add(0, 1, new Animation(d =>
        {
            leftRect.ScaleX = d;
            rightRect.ScaleX = d;
        }, 0, 1, easing: Easing.CubicInOut));

        animation.Commit(this, ExpandAnimationKey, length: AnimationLength);
    }

    public void Collapse()
    {
        this.AbortAnimation(CollapseAnimationKey);

        if (!IsCollapsedAnimated)
        {
            icon.Rotation = 0;
            leftRect.ScaleX = 0;
            rightRect.ScaleX = 0;
            return;
        }

        var animation = new Animation();

        animation.Add(0, 1, new Animation(d =>
        {
            icon.Rotation = d;
        }, 270, 0, easing: Easing.SpringOut, finished: () =>
        {
            icon.Rotation = 0;
        }));

        animation.Add(0, 1, new Animation(d =>
        {
            leftRect.ScaleX = d;
            rightRect.ScaleX = d;
        }, 1, 0, easing: Easing.CubicInOut));

        animation.Commit(this, CollapseAnimationKey, length: AnimationLength);
    }

    private static void OnIconColorChanged(BindableObject bindable, object oldValue, object newValue)
    {
        var separator = bindable as HorizontalSeparator;

        separator.icon.TintColor = newValue as Color;
    }

    private static void OnLineColorChanged(BindableObject bindable, object oldValue, object newValue)
    {
        var separator = bindable as HorizontalSeparator;

        var color = newValue as Color;

        separator.leftRect.Fill = color;
        separator.rightRect.Fill = color;
    }

    private static void OnCollapsedChanged(BindableObject bindable, object oldValue, object newValue)
    {
        var separator = bindable as HorizontalSeparator;
        var collapsed = (bool)newValue;

        if (oldValue == newValue)
            return;

        if (collapsed)
            separator.Collapse();
        else
            separator.Expand();
    }
}