using SimpleToolkit.Core;
using Marvelous.Core.Models;
using Marvelous.Maui.Extensions;

namespace Marvelous.Maui.Views.Controls;

public enum TabBarStyle
{
    Light, Transparent
}

public partial class TabBar : ContentView
{
    private const string ShowAnimationKey = "ShowAnimation";
    private const string HideAnimationKey = "HideAnimation";
    private const string AnimateInAnimationKey = "AnimateInAnimation";
    private const string AnimateOutAnimationKey = "AnimateOutAnimation";
    private const string UpdateScaleAnimationKey = "UpdateScaleAnimation";
    public const double TabBarHeight = 70;

    private readonly int buttonsStartIndex = 3;
    private double darkWonderButtonScale => (wonderButton.Width + ((wonderBoxView.Width - wonderButton.Width) * 0.9)) / wonderButton.Width;
    private readonly double lightWonderButtonScale = 1;
    private readonly Color defaultDarkBackgroundColor;
    private readonly Color defaultLightBackgroundColor;
    private readonly Color defaultIconDarkColor;
    private readonly Color defaultIconLightColor;
    private readonly Color activeIconColor;
    private readonly LineDrawable drawable;
    private readonly uint animationLength = 200;

    public event EventHandler ItemSelected;
    public event EventHandler WonderClicked;

    public static readonly BindableProperty CurrentWonderProperty =
        BindableProperty.Create(nameof(CurrentWonder), typeof(Wonder), typeof(TabBar), defaultValue: null, propertyChanged: OnCurrentWonderChanged);
    public static readonly BindableProperty ShellItemsProperty =
        BindableProperty.Create(nameof(ShellItems), typeof(IEnumerable<BaseShellItem>), typeof(TabBar), defaultValue: null, propertyChanged: OnShellItemsChanged);
    public static readonly BindableProperty CurrentShellItemProperty =
        BindableProperty.Create(nameof(CurrentShellItem), typeof(BaseShellItem), typeof(TabBar), defaultValue: null, propertyChanged: OnCurrentShellItemChanged);
    public static readonly BindableProperty IsShownProperty =
        BindableProperty.Create(nameof(IsShown), typeof(bool), typeof(TabBar), defaultValue: true, propertyChanged: OnIsShownChanged);
    public static readonly BindableProperty TabBarStyleProperty =
        BindableProperty.Create(nameof(TabBarStyle), typeof(TabBarStyle), typeof(TabBar), defaultValue: TabBarStyle.Light, propertyChanged: OnTabBarStyleChanged);

    public virtual Wonder CurrentWonder
    {
        get => (Wonder)GetValue(CurrentWonderProperty);
        set => SetValue(CurrentWonderProperty, value);
    }

    public virtual IEnumerable<BaseShellItem> ShellItems
    {
        get => (IEnumerable<BaseShellItem>)GetValue(ShellItemsProperty);
        set => SetValue(ShellItemsProperty, value);
    }

    public virtual BaseShellItem CurrentShellItem
    {
        get => (BaseShellItem)GetValue(CurrentShellItemProperty);
        set => SetValue(CurrentShellItemProperty, value);
    }

    public virtual bool IsShown
    {
        get => (bool)GetValue(IsShownProperty);
        set => SetValue(IsShownProperty, value);
    }

    public virtual TabBarStyle TabBarStyle
    {
        get => (TabBarStyle)GetValue(TabBarStyleProperty);
        set => SetValue(TabBarStyleProperty, value);
    }


    public TabBar()
	{
        App.Current.Resources.TryGetValue("PrimaryColor", out object primaryColor);
        App.Current.Resources.TryGetValue("SecondaryColor", out object secondaryColor);
        App.Current.Resources.TryGetValue("TertiaryColor", out object tertiaryColor);
        activeIconColor = primaryColor as Color;
        defaultIconDarkColor = secondaryColor as Color;
        defaultIconLightColor = Colors.White;
        defaultDarkBackgroundColor = Colors.Transparent;
        defaultLightBackgroundColor = tertiaryColor as Color;

        InitializeComponent();

        graphicsView.Drawable = drawable = new LineDrawable
        {
            Color = activeIconColor
        };

        UpdateStyle(TabBarStyle.Light);
    }


    public void OnSafeAreaChanged(Thickness safeArea)
    {
        rootGrid.HeightRequest = 80 + safeArea.Bottom;
        backgroundRect.HeightRequest = 70 + safeArea.Bottom;
    }

    public void Show()
    {
        var animation = new Animation(d =>
        {
            TranslationY = d;
        }, Height, 0);

        IsVisible = true;

        animation.Commit(this, ShowAnimationKey, finished: (d, b) =>
        {
            TranslationY = 0;
        });
    }

    public void Hide()
    {
        var animation = new Animation(d =>
        {
            TranslationY = d;
        }, 0, Height);

        animation.Commit(this, ShowAnimationKey, finished: (d, b) =>
        {
            TranslationY = Height;
            IsVisible = false;
        });
    }

    private void UpdateButtons()
    {
        for (int i = buttonsStartIndex; i < rootGrid.Count; i++)
        {
            var button = rootGrid[i] as ContentButton;
            var icon = button.Content as Icon;
            var shellItem = button.BindingContext as BaseShellItem;

            if (shellItem is null)
                continue;

            icon.TintColor = shellItem == CurrentShellItem ? activeIconColor : GetDefaultButtonIconColor();
            icon.Source = shellItem == CurrentShellItem ? ShellProperties.GetActiveIcon(shellItem) : shellItem.Icon;

            SemanticProperties.SetDescription(button, shellItem.Title);
        }

        UpdateWonderButtonScale(TabBarStyle is TabBarStyle.Light ? lightWonderButtonScale : darkWonderButtonScale);
    }

    private async Task AnimateIn()
    {
        graphicsView.AbortAnimation(AnimateInAnimationKey);
        graphicsView.AbortAnimation(AnimateOutAnimationKey);

        var animation = new Animation(d =>
        {
            drawable.AnimationProgress = d;
            graphicsView.Invalidate();
        });

        animation.Commit(graphicsView, AnimateInAnimationKey, length: animationLength);

        await Task.Delay((int)animationLength);
    }

    private async Task AnimateOut()
    {
        graphicsView.AbortAnimation(AnimateInAnimationKey);
        graphicsView.AbortAnimation(AnimateOutAnimationKey);

        var animation = new Animation(d =>
        {
            drawable.AnimationProgress = d;
            graphicsView.Invalidate();
        }, 1, 0);

        animation.Commit(graphicsView, AnimateOutAnimationKey, length: animationLength);

        await Task.Delay((int)animationLength);
    }

    private void UpdateWonderButtonScale(double newScale)
    {
        wonderButton.AbortAnimation(UpdateScaleAnimationKey);

        var currentScale = wonderButton.Scale;

        var animation = new Animation(d =>
        {
            wonderButton.Scale = d;
        }, currentScale, newScale);

        animation.Commit(wonderButton, UpdateScaleAnimationKey);
    }

    private void UpdateStyle(TabBarStyle style)
    {
        switch (style)
        {
            case TabBarStyle.Light:
                backgroundRect.Fill = defaultLightBackgroundColor;
                wonderBoxView.Color = defaultLightBackgroundColor;
                UpdateWonderButtonScale(lightWonderButtonScale);
                break;
            case TabBarStyle.Transparent:
                backgroundRect.Fill = defaultDarkBackgroundColor;
                wonderBoxView.Color = Colors.White;
                UpdateWonderButtonScale(darkWonderButtonScale);
                break;
        }

        rootGrid
            .Where(v => v is ContentButton cb && cb.BindingContext != CurrentShellItem)
            .Cast<ContentButton>()
            .Select(cb => cb.Content)
            .Cast<Icon>()
            .ToList()
            .ForEach(i => i.TintColor = GetDefaultButtonIconColor());

        if (GetButtonOfBindingContext(CurrentShellItem)?.Content is Icon icon)
            icon.TintColor = activeIconColor;
    }

    private ContentButton GetButtonOfBindingContext(object bindingContext)
    {
        return rootGrid.Children
            .Where(v => v is ContentButton)
            .FirstOrDefault(v => (v as ContentButton).BindingContext == bindingContext) as ContentButton;
    }

    private Color GetDefaultButtonIconColor()
    {
        return TabBarStyle switch
        {
            TabBarStyle.Light => defaultIconDarkColor,
            TabBarStyle.Transparent => defaultIconLightColor,
            _ => defaultIconDarkColor
        };
    }

    private void TabButtonClicked(object sender, EventArgs e)
    {
        ItemSelected?.Invoke(sender, e);
    }

    private void WonderButtonClicked(object sender, EventArgs e)
    {
        WonderClicked?.Invoke(sender, e);
    }

    private static void OnCurrentWonderChanged(BindableObject bindable, object oldValue, object newValue)
    {
        var tabBar = bindable as TabBar;
        var wonder = newValue as Wonder;

        tabBar.wonderImage.Source = WonderViewConfig.GetWonderViewConfig(wonder.Type).WonderButton;
        tabBar.wonderImage.BackgroundColor = WonderViewConfig.GetWonderViewConfig(wonder.Type).SecondaryColor;
    }

    private static void OnShellItemsChanged(BindableObject bindable, object oldValue, object newValue)
    {
        if (newValue is null)
            return;

        var tabBar = bindable as TabBar;
        var shellItems = (newValue as IEnumerable<BaseShellItem>).ToList();
        var start = tabBar.buttonsStartIndex;

        for (int i = start; i < tabBar.rootGrid.Count; i++)
        {
            var button = tabBar.rootGrid[i] as ContentButton;
            var icon = button.Content as Icon;
            var shellItem = shellItems.Count <= i - start ? null : shellItems[i - start];

            icon.Source = shellItem?.Icon;

            button.BindingContext = shellItem;
        }

        var currentButton = tabBar.GetButtonOfBindingContext(tabBar.CurrentShellItem);

        if (currentButton is null)
            return;

        var currentIndex = Grid.GetColumn(currentButton);

        tabBar.drawable.CurrentIndex = currentIndex;
        tabBar.drawable.AnimationProgress = 1;
        tabBar.UpdateButtons();
        tabBar.graphicsView.Invalidate();
    }

    private static async void OnCurrentShellItemChanged(BindableObject bindable, object oldValue, object newValue)
    {
        var tabBar = bindable as TabBar;
        var newShellItem = newValue as BaseShellItem;
        var oldShellItem = oldValue as BaseShellItem;

        if (tabBar.ShellItems is null)
            return;

        var newButton = tabBar.GetButtonOfBindingContext(newShellItem);
        var oldButton = tabBar.GetButtonOfBindingContext(oldShellItem);

        var newIndex = newButton is not null ? Grid.GetColumn(newButton) : 0;
        var oldIndex = oldButton is not null ? Grid.GetColumn(oldButton) : 0;

        tabBar.drawable.CurrentIndex = oldIndex;
        await tabBar.AnimateOut();
        
        tabBar.UpdateButtons();

        tabBar.drawable.CurrentIndex = newIndex;
        await tabBar.AnimateIn();
    }

    private static void OnIsShownChanged(BindableObject bindable, object oldValue, object newValue)
    {
        var tabBar = bindable as TabBar;

        var oldBool = (bool)oldValue;
        var newBool = (bool)newValue;

        if (oldBool == newBool)
            return;

        if (newBool)
            tabBar.Show();
        else
            tabBar.Hide();
    }

    private static void OnTabBarStyleChanged(BindableObject bindable, object oldValue, object newValue)
    {
        var tabBar = bindable as TabBar;
        var style = (TabBarStyle)newValue;

        tabBar.UpdateStyle(style);
    }

    private class LineDrawable : IDrawable
    {
        private const float topOffset = 16;
        private const float lineHeight = 3.6f;
        private const float lineWidth = 22;

        public int CurrentIndex { get; set; }
        public double AnimationProgress { get; set; } = 1;
        public Color Color { get; set; } = Colors.Black;

        public void Draw(ICanvas canvas, RectF dirtyRect)
        {
            if (CurrentIndex == 0)
                return;

            canvas.SaveState();

            var width = lineWidth * (float)AnimationProgress;
            var top = (dirtyRect.Height / 2f) + topOffset;
            var left = ((dirtyRect.Width / 5f) * CurrentIndex) + (((dirtyRect.Width / 5f) - width) / 2f);

            canvas.SetFillPaint(new SolidPaint(Color), dirtyRect);
            canvas.FillRectangle(left, top, width, lineHeight);

            canvas.RestoreState();
        }
    }
}