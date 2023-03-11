using SimpleToolkit.Core;
using Marvelous.Core.Models;
using Marvelous.Maui.Views.Illustrations;

namespace Marvelous.Maui.Views.Controls;

public partial class IllustrationsCarouselView : ContentView
{
    private const int HorizontalSwipeDuration = 250;
    private double totalPanX = 0;
    private double totalPanY = 0;
    private bool? isHorizontalPan;
    private bool animateOnHorizontalPanCompleted;
    private bool isHidden;
    private Wonder currentWonderBeforePan;
    private DateTime panStart;

    private readonly IDictionary<WonderType, BaseIllustration> illustrations = new Dictionary<WonderType, BaseIllustration>();
    private BaseIllustration currentIllustration;
    private BaseIllustration previousIllustration;

    public static readonly BindableProperty CurrentWonderProperty = BindableProperty.Create(nameof(CurrentWonder), typeof(Wonder), typeof(IllustrationsCarouselView), defaultBindingMode: BindingMode.TwoWay, propertyChanged: OnCurrentWonderChanged);
    public static readonly BindableProperty WondersProperty = BindableProperty.Create(nameof(Wonders), typeof(IList<Wonder>), typeof(IllustrationsCarouselView), propertyChanged: OnWondersChanged);

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

    public event EventHandler Closing;
    public event EventHandler Hiding;
    public event EventHandler Presenting;


    public IllustrationsCarouselView()
	{
		InitializeComponent();

        Loaded += IllustrationsCarouselViewLoaded;
        Unloaded += IllustrationsCarouselViewUnloaded;
    }

    private void IllustrationsCarouselViewUnloaded(object sender, EventArgs e)
    {
        this.Window.UnsubscribeFromSafeAreaChanges(OnSafeAreaChanged);
    }

    private void IllustrationsCarouselViewLoaded(object sender, EventArgs e)
    {
        this.Window.SubscribeToSafeAreaChanges(OnSafeAreaChanged);
    }

    private void OnSafeAreaChanged(Thickness safeArea)
    {
        indicatorContainer.Margin = safeArea;
    }

    public async Task Show()
    {
        IsVisible = true;
        isHidden = false;

        Presenting?.Invoke(this, EventArgs.Empty);

        verticalPanIndicatorView.AnimateIn(); // This is not in the original design but I like it
        await Task.WhenAll(
            currentIllustration.AnimateIn(),
            backgroundContainer.FadeTo(1, 200));
    }

    public async Task Hide()
    {
        isHidden = true;
        Hiding?.Invoke(this, EventArgs.Empty);
        verticalPanIndicatorView.Progress = 0;

        await Task.WhenAll(
            currentIllustration.AnimateOut(),
            currentIllustration.AnimateForegroundScaleToDefault(),
            backgroundContainer.FadeTo(0, 200));
        
        currentIllustration.ForegroundScale = 1;

        IsVisible = false;
    }

    private BaseIllustration CreateIllustration(WonderType wonder, string title)
    {
        return wonder switch
        {
            WonderType.ChichenItza => new ChichenItzaIllustration { Title = title },
            WonderType.ChristRedeemer => new ChristRedeemerIllustration { Title = title },
            WonderType.Colosseum => new ColosseumIllustration { Title = title },
            WonderType.GreatWall => new GreatWallIllustration { Title = title },
            WonderType.MachuPicchu => new MachuPicchuIllustration { Title = title },
            WonderType.Petra => new PetraIllustration { Title = title },
            WonderType.PyramidsGiza => new PyramidsGizaIllustration { Title = title },
            WonderType.TajMahal => new TajMahalIllustration { Title = title },
            _ => null // TODO: Default illustration
        };
    }

    private async void SwapIllustrations(BaseIllustration newCurrent)
    {
        if (!illustrationsContainer.Contains(newCurrent))
            illustrationsContainer.Add(newCurrent);
        
        if (previousIllustration is not null && previousIllustration != newCurrent)
        {
            previousIllustration.Offset = 0;
            previousIllustration.LayoutViewsToDefaultPosition();
        }

        previousIllustration = currentIllustration;
        currentIllustration = newCurrent;

        var backgroundColor = WonderViewConfig.GetWonderViewConfig(CurrentWonder.Type).SecondaryColor;
        backgroundContainer.Background = backgroundColor;

        if (animateOnHorizontalPanCompleted)
        {
            previousIllustration.AnimateMainObjectOut(totalPanX);

            animateOnHorizontalPanCompleted = false;
            //_ = currentIllustration.AnimatePositionToDefault();
            currentIllustration.AnimateMainObjectIn(totalPanX);
        }

        verticalPanIndicatorView.AnimateIn();
        await Task.WhenAll(
            previousIllustration?.AnimateOut() ?? Task.CompletedTask,
            currentIllustration?.AnimateIn() ?? Task.CompletedTask);

        if (illustrationsContainer.Count > 1)
        {
            var previousIllustrations = new List<BaseIllustration>();

            foreach (var child in illustrationsContainer.Children)
            {
                if (child is BaseIllustration illustration && illustration != currentIllustration)
                {
                    previousIllustrations.Add(illustration);
                    illustration.Offset = 0;
                    illustration.LayoutViewsToDefaultPosition();
                }
            }

            foreach (var illustration in previousIllustrations)
                illustrationsContainer.Remove(illustration);
        }
    }

    private Wonder GetNextWonder(Wonder wonder)
    {
        for (int i = 0; i < Wonders.Count; i++)
        {
            if (wonder.Type == Wonders[i].Type)
            {
                var index = Wonders.Count == i + 1 ? 0 : i + 1;
                return Wonders[index];
            }
        }

        return null;
    }

    private Wonder GetPreviousWonder(Wonder wonder)
    {
        for (int i = 0; i < Wonders.Count; i++)
        {
            if (wonder.Type == Wonders[i].Type)
            {
                var index = i - 1 < 0 ? Wonders.Count - 1 : i - 1;
                return Wonders[index];
            }
        }

        return null;
    }

    private void HandleHorizontalRunningPan()
    {
        Wonder wonder = null;

        if (Math.Abs(totalPanX) > Width / 2)
        {
            if (totalPanX < 0)
            {
                var next = GetNextWonder(currentWonderBeforePan);
                if (next.Type != CurrentWonder.Type)
                    wonder = next;
            }
            else
            {
                var previous = GetPreviousWonder(currentWonderBeforePan);
                if (previous.Type != CurrentWonder.Type)
                    wonder = previous;
            }
        }
        else if (currentWonderBeforePan.Type != CurrentWonder.Type)
        {
            wonder = totalPanX > 0 ? GetNextWonder(CurrentWonder) : GetPreviousWonder(CurrentWonder);
        }

        if (wonder is not null && CurrentWonder.Type != wonder.Type)
            CurrentWonder = wonder;

        if (currentIllustration is not null)
            currentIllustration.Offset = currentWonderBeforePan == CurrentWonder ? totalPanX : totalPanX < 0 ? (Width + totalPanX) % Width : (-Width + totalPanX) % Width;
        if (previousIllustration is not null)
            previousIllustration.Offset = currentWonderBeforePan != CurrentWonder ? totalPanX : totalPanX < 0 ? (Width + totalPanX) % Width : (-Width + totalPanX) % Width;
    }

    private async Task HandleVerticalRunningPan()
    {
        var progress = totalPanY < 0 ? Math.Abs(totalPanY) / verticalPanIndicatorView.Height : 0;
        verticalPanIndicatorView.Progress = progress;
        currentIllustration.ForegroundScale = 1 + (progress * 0.1);

        if (progress > 1 && !isHidden)
        {
            Closing?.Invoke(this, EventArgs.Empty);
            await Hide();
        }
    }

    private async void PanGestureRecognizerPanUpdated(object sender, PanUpdatedEventArgs e)
    {
        switch (e.StatusType)
        {
            case GestureStatus.Started:
                animateOnHorizontalPanCompleted = false;
                totalPanX = 0;
                totalPanY = 0;
                currentWonderBeforePan = CurrentWonder;
                panStart = DateTime.Now;
                break;
            case GestureStatus.Running:
                totalPanX = e.TotalX;
                totalPanY = e.TotalY;

                if (isHorizontalPan is null)
                    isHorizontalPan = !((e.TotalY < 0) && (Math.Abs(e.TotalX) <= Math.Abs(e.TotalY)));

                if (isHorizontalPan is true)
                    HandleHorizontalRunningPan();
                else if (isHorizontalPan is false)
                    await HandleVerticalRunningPan();
                break;
            case GestureStatus.Completed:
                verticalPanIndicatorView.Progress = 0;
                
                if (isHorizontalPan is true)
                {
                    isHorizontalPan = null;
                    var totalMilliseconds = (DateTime.Now - panStart).TotalMilliseconds;

                    if (totalMilliseconds < HorizontalSwipeDuration && currentWonderBeforePan == CurrentWonder)
                    {
                        animateOnHorizontalPanCompleted = true;
                        CurrentWonder = totalPanX < 0 ? GetNextWonder(currentWonderBeforePan) : GetPreviousWonder(currentWonderBeforePan);
                    }
                    else
                    {
                        await currentIllustration.AnimatePositionToDefault();
                    }
                }
                else if (isHorizontalPan is false)
                {
                    if (!isHidden)
                        _ = currentIllustration.AnimateForegroundScaleToDefault();
                }

                isHorizontalPan = null;
                break;
            case GestureStatus.Canceled:
                isHorizontalPan = null;
                verticalPanIndicatorView.Progress = 0;

                if (isHorizontalPan is true)
                {
                    await currentIllustration.AnimatePositionToDefault();
                }
                else if (isHorizontalPan is false)
                {
                    if (!isHidden)
                        _ = currentIllustration.AnimateForegroundScaleToDefault();
                }
                break;
        }
    }

    private static void OnCurrentWonderChanged(BindableObject bindable, object oldValue, object newValue)
    {
        var carousel = bindable as IllustrationsCarouselView;
        var wonder = newValue as Wonder;

        if (!carousel.illustrations.ContainsKey(wonder.Type))
            carousel.illustrations[wonder.Type] = carousel.CreateIllustration(wonder.Type, wonder.Title);

        carousel.SwapIllustrations(carousel.illustrations[wonder.Type]);
        if (carousel.Wonders is not null)
            carousel.indicatorView.CurrentIndex = carousel.Wonders.IndexOf(carousel.Wonders.FirstOrDefault(w => w.Type == wonder.Type));
    }

    private static void OnWondersChanged(BindableObject bindable, object oldValue, object newValue)
    {
        var carousel = bindable as IllustrationsCarouselView;

        carousel.indicatorView.Count = carousel.Wonders.Count;
    }
}