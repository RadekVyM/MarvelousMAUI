using Marvelous.Core;

namespace Marvelous.Maui.Views.Controls;

public partial class WonderTitle : ContentView
{
    public static readonly BindableProperty TitleProperty =
        BindableProperty.Create(nameof(Title), typeof(string), typeof(WonderTitle), propertyChanged: OnTitleChanged);

    public virtual string Title
    {
        get => (string)GetValue(TitleProperty);
        set => SetValue(TitleProperty, value);
    }


    public WonderTitle()
	{
		InitializeComponent();
	}


    private void UpdateTitle(string titleKey)
    {
        var title = Localization.ResourceManager.GetString(titleKey);
        illustrationTitle.Text = title;
    }

    private static void OnTitleChanged(BindableObject bindable, object oldValue, object newValue)
    {
        var wonderTitle = bindable as  WonderTitle;
        wonderTitle.UpdateTitle(newValue.ToString());
    }
}