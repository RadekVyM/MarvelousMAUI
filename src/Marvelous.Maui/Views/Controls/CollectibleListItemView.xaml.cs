using System.Globalization;
using System.Windows.Input;
using Marvelous.Core.Models;
using Marvelous.Maui.Converters;

namespace Marvelous.Maui.Views.Controls;

public partial class CollectibleListItemView : ContentView
{
    private readonly CollectibleIconTypeToIconConverter iconConverter = new CollectibleIconTypeToIconConverter();

    public static readonly BindableProperty CollectibleStateProperty =
        BindableProperty.Create(nameof(CollectibleState), typeof(CollectibleState), typeof(CollectibleListItemView), CollectibleState.Lost, BindingMode.OneWay, propertyChanged: OnCollectibleStatePropertyChanged);

    public static readonly BindableProperty CollectibleProperty =
        BindableProperty.Create(nameof(Collectible), typeof(Collectible), typeof(CollectibleListItemView), null, BindingMode.OneWay, propertyChanged: OnCollectiblePropertyChanged);

    public static readonly BindableProperty CommandProperty =
        BindableProperty.Create(nameof(Command), typeof(ICommand), typeof(CollectibleListItemView), null, BindingMode.OneWay);

    public CollectibleState CollectibleState
    {
        get => (CollectibleState)GetValue(CollectibleStateProperty);
        set => SetValue(CollectibleStateProperty, value);
    }

    public Collectible Collectible
    {
        get => (Collectible)GetValue(CollectibleProperty);
        set => SetValue(CollectibleProperty, value);
    }

    public ICommand Command
    {
        get => (ICommand)GetValue(CommandProperty);
        set => SetValue(CommandProperty, value);
    }


    public CollectibleListItemView()
	{
		InitializeComponent();
        UpdateVisibility();
    }


    private void UpdateVisibility()
    {
        switch (CollectibleState)
        {
            case CollectibleState.Lost:
                icon.IsVisible = true;
                button.IsVisible = false;
                rectangle.IsVisible = false;
                break;
            case CollectibleState.Discovered:
                icon.IsVisible = false;
                button.IsVisible = true;
                rectangle.IsVisible = true;
                break;
            case CollectibleState.Explored:
                icon.IsVisible = false;
                button.IsVisible = true;
                rectangle.IsVisible = false;
                break;
        }
    }

    private void ButtonClicked(object sender, EventArgs e)
    {
        Command?.Execute(Collectible);
    }

    private static void OnCollectibleStatePropertyChanged(BindableObject bindable, object oldValue, object newValue)
    {
        var view = bindable as CollectibleListItemView;
        view.UpdateVisibility();
    }

    private static void OnCollectiblePropertyChanged(BindableObject bindable, object oldValue, object newValue)
    {
        var view = bindable as CollectibleListItemView;
        var collectible = newValue as Collectible;

        view.icon.Source = view.iconConverter
            .Convert(collectible.IconType, null, null, CultureInfo.CurrentCulture)
            ?.ToString();
        view.image.Source = collectible.ImageUrlSmall;
    }
}
