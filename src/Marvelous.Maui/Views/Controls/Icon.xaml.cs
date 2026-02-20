namespace Marvelous.Maui.Views.Controls;

public partial class Icon : Image
{
	public static readonly BindableProperty TintColorProperty =
		BindableProperty.Create(nameof(TintColor), typeof(Color), typeof(Icon), propertyChanged: OnTintColorChanged);

	public virtual Color TintColor
	{
		get => (Color)GetValue(TintColorProperty);
		set => SetValue(TintColorProperty, value);
	}

	public Icon()
	{
		InitializeComponent();
	}

	private static void OnTintColorChanged(BindableObject bindable, object oldValue, object newValue)
	{
		if (bindable is not Icon icon)
			return;

		icon.tintBehavior.TintColor = newValue as Color;
	}
}