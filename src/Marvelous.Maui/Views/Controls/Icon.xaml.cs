using CommunityToolkit.Maui.Behaviors;

namespace Marvelous.Maui.Views.Controls;

public class Icon : Image
{
	private readonly IconTintColorBehavior tintBehavior;

	public static readonly BindableProperty TintColorProperty =
		BindableProperty.Create(nameof(TintColor), typeof(Color), typeof(Icon), propertyChanged: OnTintColorChanged);

	public virtual Color TintColor
	{
		get => (Color)GetValue(TintColorProperty);
		set => SetValue(TintColorProperty, value);
	}

	public Icon()
	{
		tintBehavior = new IconTintColorBehavior();
		Behaviors.Add(tintBehavior);
	}

	private static void OnTintColorChanged(BindableObject bindable, object oldValue, object newValue)
	{
		if (bindable is not Icon icon)
			return;

		icon.tintBehavior.TintColor = newValue as Color;
	}
}