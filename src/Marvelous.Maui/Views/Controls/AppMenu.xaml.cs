using Microsoft.Maui.Controls.Shapes;
using SimpleToolkit.Core;
using Marvelous.Core.Models;
using Marvelous.Maui.Extensions;
using CommunityToolkit.Maui.Views;

namespace Marvelous.Maui.Views.Controls;

public partial class AppMenu : ContentView
{
    private const string ShowAnimationKey = "ShowAnimation";
    private const string HideAnimationKey = "HideAnimation";
    private const uint ShowAnimationLength = 900;

    private const double WonderButtonSpacing = 15;
    private const double WonderButtonSpan = 3;
    private const double WonderButtonCornerRadius = 8;

    public static readonly BindableProperty CurrentWonderProperty = BindableProperty.Create(nameof(CurrentWonder), typeof(Wonder), typeof(AppMenu), defaultBindingMode: BindingMode.TwoWay, propertyChanged: OnCurrentWonderChanged);
    public static readonly BindableProperty WondersProperty = BindableProperty.Create(nameof(Wonders), typeof(IList<Wonder>), typeof(AppMenu), propertyChanged: OnWondersChanged);

    public virtual Wonder CurrentWonder
    {
        get => (Wonder)GetValue(CurrentWonderProperty);
        set => SetValue(CurrentWonderProperty, value);
    }

    public virtual IList<Wonder> Wonders
    {
        get => (IList<Wonder>)GetValue(WondersProperty);
        set => SetValue(WondersProperty, value);
    }

    public event EventHandler Hidden;
    public event EventHandler Presenting;
    public event EventHandler TimelineClicked { add => timelineButton.Clicked += value; remove => timelineButton.Clicked -= value; }
    public event EventHandler CollectionClicked { add => collectionButton.Clicked += value; remove => collectionButton.Clicked -= value; }
    public event EventHandler AboutThisAppClicked { add => aboutThisAppButton.Clicked += value; remove => aboutThisAppButton.Clicked -= value; }


    public AppMenu()
	{
		Opacity = 0;
		IsVisible = false;
		
        InitializeComponent();
    }


    public void Show()
	{
        Presenting?.Invoke(this, EventArgs.Empty);
        IsVisible = true;
        App.Current.MainPage.Window.SubscribeToSafeAreaChanges(OnSafeAreaChanged);

        this.AbortAnimation(ShowAnimationKey);

        var animation = new Animation();

        var buttons = buttonsStackLayout.Where(v => v is ContentButton).Cast<ContentButton>().Select(b => b.Content).ToList();
        var lines = buttonsStackLayout.Where(v => v is not ContentButton).Cast<View>().ToList();
        buttons.ForEach(b => b.Opacity = 0);
        lines.ForEach(l => l.Opacity = 0);

        this.Opacity = 0;

        var fadeAnimation = new Animation(d => this.Opacity = d);
        var scaleAnimation = new Animation(d => wondersAbsoluteLayout.Scale = d, 0.75, 1);
        var buttonsAnimation = new Animation(d =>
        {
            foreach (var button in buttons)
            {
                button.TranslationY = button.Height * 0.15 * (1 - d);
                button.Opacity = d;
            }
        });
        var linesAnimation = new Animation(d =>
        {
            foreach (var line in lines)
            {
                line.ScaleX = d;
                line.Opacity = Math.Min(d * 3, 1);
            }
        }, easing: Easing.SpringOut);

        animation.Add(0, 0.4, fadeAnimation);
        animation.Add(0, 0.4, scaleAnimation);
        animation.Add(0.35, 0.55, buttonsAnimation);
        animation.Add(0.5, 1, linesAnimation);

        animation.Commit(this, ShowAnimationKey, length: ShowAnimationLength, finished: (d, b) =>
        {
            Opacity = 1;
            wondersAbsoluteLayout.Scale = 1;

            foreach (var button in buttons)
            {
                button.TranslationY = 0;
                button.Opacity = 1;
            }

            foreach (var line in lines)
            {
                line.ScaleX = 1;
                line.Opacity = 1;
            }
        });
	}

	public void Hide()
    {
        App.Current.MainPage.Window.UnsubscribeFromSafeAreaChanges(OnSafeAreaChanged);

        this.AbortAnimation(HideAnimationKey);

        var animation = new Animation(d => Opacity = d, 1, 0);

        animation.Commit(this, HideAnimationKey, finished: (d, b) => 
        {
            if (!b)
            {
                IsVisible = false;
                Opacity = 1;
                Hidden?.Invoke(this, EventArgs.Empty);
            }
        });
    }

    private Border CreateWonderButton(WonderType wonder)
    {
        var config = WonderViewConfig.GetWonderViewConfig(wonder);

        var border = new Border
        {
            Background = Colors.Transparent,
            StrokeThickness = 0,
            StrokeShape = new RoundRectangle
            {
                CornerRadius = WonderButtonCornerRadius
            },
            BindingContext = wonder
        };

        var innerBorder = new Border
        {
            Stroke = Colors.Transparent,
            StrokeThickness = 0,
            StrokeShape = new RoundRectangle
            {
                CornerRadius = WonderButtonCornerRadius / 2
            },
            BindingContext = wonder
        };

        var contentButton = new ContentButton
        {
            BindingContext = wonder
        };

        var image = new Image
        {
            Background = config.SecondaryColor,
            Source = config.WonderButton,
            Aspect = Aspect.AspectFill,
            BindingContext = wonder
        };

        contentButton.Content = image;
        innerBorder.Content = contentButton;
        border.Content = innerBorder;

        contentButton.Clicked += ContentButtonClicked;

        return border;
    }

    private void UpdateWonderButtons()
    {
        var compass = compassImage;

        wondersAbsoluteLayout.Clear();
        wondersAbsoluteLayout.Add(compass);

        foreach (var wonder in Wonders)
        {
            var button = CreateWonderButton(wonder.Type);
            wondersAbsoluteLayout.Add(button);
        }

        UpdateButtonSelection();
    }

    private void UpdateButtonSelection()
    {
        foreach (var view in wondersAbsoluteLayout)
        {
            if (view is not Border button)
                continue;

            if ((WonderType)button.BindingContext == CurrentWonder?.Type)
            {
                button.Padding = 5;
                button.Background = Colors.White;
            }
            else
            {
                button.Padding = 0;
                button.Background = Colors.Transparent;
            }
        }
    }

    private void OnSafeAreaChanged(Thickness safeArea)
    {
        rootContentGrid.Margin = safeArea;
    }

	private void CloseButtonClicked(object sender, EventArgs e)
	{
		Hide();
	}

    private void ContentButtonClicked(object sender, EventArgs e)
    {
        var button = sender as ContentButton;

        var wonder = Wonders.FirstOrDefault(w => w.Type == (WonderType)button.BindingContext);
        CurrentWonder = wonder;

        Hide();
    }

    private static void OnCurrentWonderChanged(BindableObject bindable, object oldValue, object newValue)
    {
        var menu = bindable as AppMenu;
        var wonder = newValue as Wonder;
        menu.UpdateButtonSelection();
    }

    private static void OnWondersChanged(BindableObject bindable, object oldValue, object newValue)
    {
        var menu = bindable as AppMenu;
        menu.UpdateWonderButtons();
    }

    private void WondersGridContainerSizeChanged(object sender, EventArgs e)
    {
        var size = Math.Min(wondersAbsoluteLayout.Width, wondersAbsoluteLayout.Height);
        var buttonSize = (size - ((WonderButtonSpan - 1) * WonderButtonSpacing)) / WonderButtonSpan;
        var defatulTop = (wondersAbsoluteLayout.Height - size) / 2;
        var defaultLeft = (wondersAbsoluteLayout.Width - size) / 2;
        var top = defatulTop;
        var left = defaultLeft;
        var index = 0;

        wondersAbsoluteLayout.LayoutChildTo(compassImage, new Rect(defaultLeft + buttonSize + WonderButtonSpacing, defatulTop + buttonSize + WonderButtonSpacing, buttonSize, buttonSize));

        for (int i = 0; i < Wonders.Count; i++)
        {
            var button = wondersAbsoluteLayout[i + 1] as View;

            wondersAbsoluteLayout.LayoutChildTo(button, new Rect(left, top, buttonSize, buttonSize));

            if (i == 3)
            {
                left += buttonSize + WonderButtonSpacing;
                index++;
            }

            left = ((index % WonderButtonSpan) < WonderButtonSpan - 1) ? left + buttonSize + WonderButtonSpacing : defaultLeft;
            top = ((index + 1) % WonderButtonSpan == 0) ? top + buttonSize + WonderButtonSpacing : top;

            index++;
        }
    }
}