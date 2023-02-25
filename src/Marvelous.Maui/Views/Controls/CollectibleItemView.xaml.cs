using System.ComponentModel;
using System.Windows.Input;
using Marvelous.Core.Interfaces.ViewModels;
using Marvelous.Core.Models;
using Marvelous.Maui.Converters;

namespace Marvelous.Maui.Views.Controls;

public partial class CollectibleItemView : ContentView
{
    private const string ShakyAnimationKey = "ShakyAnimation";
    private const string HideAnimationKey = "HideAnimation";

    private readonly CollectibleIconTypeToIconConverter collectibleIconTypeToIconConverter;
    private object oldBindingContext = null;

    public static readonly BindableProperty PositionProperty =
        BindableProperty.Create(nameof(Position), typeof(int), typeof(CollectibleItemView), 0, BindingMode.OneWay, propertyChanged: OnPositionPropertyChanged);

    public static readonly BindableProperty VisiblePositionProperty =
        BindableProperty.Create(nameof(VisiblePosition), typeof(int), typeof(CollectibleItemView), 0, BindingMode.OneWay, propertyChanged: OnPositionPropertyChanged);

    public static readonly BindableProperty CollectibleProperty =
        BindableProperty.Create(nameof(Collectible), typeof(Collectible), typeof(CollectibleItemView), null, BindingMode.OneWay, propertyChanged: OnCollectiblePropertyChanged);

    public static readonly BindableProperty CommandProperty =
        BindableProperty.Create(nameof(Command), typeof(ICommand), typeof(CollectibleItemView), null, BindingMode.OneWay);

    public int Position
    {
        get => (int)GetValue(PositionProperty);
        set => SetValue(PositionProperty, value);
    }

    public int VisiblePosition
    {
        get => (int)GetValue(VisiblePositionProperty);
        set => SetValue(VisiblePositionProperty, value);
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


    public CollectibleItemView()
	{
        collectibleIconTypeToIconConverter = new CollectibleIconTypeToIconConverter();

        InitializeComponent();
	}


    private void StartShakyAnimation()
    {
        this.AbortAnimation(ShakyAnimationKey);

        image.Scale = 1;

        var animation = new Animation();

        animation.Add(0.55, 0.65, new Animation(d =>
        {
            image.Scale = d;
        }, 1, 1.1, Easing.CubicInOut));

        animation.Add(0.5, 0.6, new Animation(d =>
        {
            image.Rotation = d;
        }, 0, 15, Easing.CubicInOut));

        animation.Add(0.6, 0.65, new Animation(d =>
        {
            image.Rotation = d;
        }, 15, -10, Easing.CubicInOut));

        animation.Add(0.65, 0.7, new Animation(d =>
        {
            image.Rotation = d;
        }, -10, 10, Easing.CubicInOut));

        animation.Add(0.7, 0.75, new Animation(d =>
        {
            image.Rotation = d;
        }, 10, -10, Easing.CubicInOut));

        animation.Add(0.75, 0.8, new Animation(d =>
        {
            image.Rotation = d;
        }, -10, 10, Easing.CubicInOut));

        animation.Add(0.8, 0.85, new Animation(d =>
        {
            image.Rotation = d;
        }, 10, -15, Easing.CubicInOut));

        animation.Add(0.85, 1, new Animation(d =>
        {
            image.Rotation = d;
        }, -15, 0, Easing.CubicInOut));

        animation.Add(0.85, 1, new Animation(d =>
        {
            image.Scale = d;
        }, 1.1, 1, Easing.CubicInOut));

        animation.Commit(this, ShakyAnimationKey, length: 5000, repeat: () => true);
    }

    private void Hide()
    {
        this.AbortAnimation(ShakyAnimationKey);

        var animation = new Animation();

        animation.Add(0, 0.9, new Animation(d =>
        {
            image.Scale = d;
        }, image.Scale, 0, Easing.SpringIn));

        animation.Commit(this, HideAnimationKey, length: 500, finished: (d, b) =>
        {
            UpdateVisibility();
        });
    }

    private void UpdateVisibility()
    {
        IsVisible = Position == VisiblePosition && Collectible?.CollectibleState == CollectibleState.Lost;

        if (IsVisible)
            StartShakyAnimation();
        else
            this.AbortAnimation(ShakyAnimationKey);
    }

    protected override void OnBindingContextChanged()
    {
        base.OnBindingContextChanged();

        if (oldBindingContext != BindingContext)
        {
            if (oldBindingContext is IBaseViewModel oldViewModel)
                oldViewModel.PropertyChanged -= OnViewModelPropertyChanged;

            if (BindingContext is IBaseViewModel newViewModel)
                newViewModel.PropertyChanged += OnViewModelPropertyChanged;

            oldBindingContext = BindingContext;
        }
    }

    private void OnViewModelPropertyChanged(object sender, PropertyChangedEventArgs e)
    {
        if (e.PropertyName == nameof(IBaseWonderCollectiblePageViewModel.Collectible.CollectibleState))
        {
            if (Collectible.CollectibleState != CollectibleState.Lost)
                Hide();
            else
                UpdateVisibility();
        }
    }

    private static void OnCollectiblePropertyChanged(BindableObject bindable, object oldValue, object newValue)
    {
        var view = bindable as CollectibleItemView;

        if (newValue is not Collectible collectible)
            return;

        view.image.Source = view.collectibleIconTypeToIconConverter
            .Convert(collectible.IconType, typeof(CollectibleItemView), null, System.Globalization.CultureInfo.CurrentCulture)
            .ToString();

        view.UpdateVisibility();
    }

    private static void OnPositionPropertyChanged(BindableObject bindable, object oldValue, object newValue)
    {
        var view = bindable as CollectibleItemView;

        view.UpdateVisibility();
    }

    private void ButtonClicked(object sender, EventArgs e)
    {
        Command?.Execute(null);
    }
}
