using Marvelous.Core;
using Marvelous.Core.Interfaces.Services;
using Marvelous.Core.Interfaces.ViewModels;
using Marvelous.Core.Models;
using Marvelous.Maui.Models;
using Marvelous.Maui.Views.Illustrations;
using Microsoft.Maui.Controls.Shapes;

namespace Marvelous.Maui.Views.Pages;

public partial class WonderMainPage : BaseContentPage
{
    private const string HideMenuButtonAnimationKey = "HideMenuButtonAnimation";
    private const string ShowMenuButtonAnimationKey = "ShowMenuButtonAnimation";
    private const double ScrolledTopHeaderImageOpacity = 0.6d;
    private const double MinimizedHeaderImageOpacity = 0.975d;
    private readonly IDictionary<WonderType, BaseHeaderIllustration> headerIllustrations = new Dictionary<WonderType, BaseHeaderIllustration>();
    private readonly IWonderMainPageViewModel viewModel;
    private FactsHistoryWonderSectionViewModel factsHistoryWonderSectionViewModel;
    private ConstructionWonderSectionViewModel constructionWonderSectionViewModel;

    private double scrollY = 0;
    private double firstHistoryInfoHeight = 0;
    private double factsHistoryHeight = 0;
    private double constructionHeight = 0;
    private bool isHeaderImageContainerMinimized = false;
    private bool onlyOnTopVisible = true;
	private Thickness safeArea;
    private Thickness defaultMenuButtonBorderMargin = Thickness.Zero;
    private WonderViewConfig wonderConfig;

    private double windowImageTop => firstHistoryInfoHeight + (collectionViewHeader.HeightRequest > 0 ? collectionViewHeader.HeightRequest : factsHistoryWonderSectionViewModel?.Margin.VerticalThickness ?? 0);
    private double defaultHeaderImageContainerHeight => Height * 0.5;
	private double minimizedHeaderImageContainerHeight => safeArea.Top + 65;
    private double wonderSectionTitleCutOutHeight => 0.4 * wonderSectionTitleView.Height;


    public WonderMainPage(INavigationService navigationService, IWonderMainPageViewModel viewModel) : base(navigationService)
    {
		BindingContext = this.viewModel = viewModel;

		InitializeComponent();
        
		viewModel.PropertyChanged += ViewModelPropertyChanged;

		collectionViewFooterContainer.HeightRequest = Controls.TabBar.TabBarHeight + 20;
    }


    protected override void OnSafeAreaChanged(Thickness safeArea)
	{
        if (defaultMenuButtonBorderMargin == Thickness.Zero)
            defaultMenuButtonBorderMargin = menuButtonBorder.Margin;

        this.safeArea = safeArea;
        Padding = new Thickness(safeArea.Left, 0, safeArea.Right, safeArea.Bottom);
        menuButtonBorder.Margin = new Thickness(
            defaultMenuButtonBorderMargin.Left + safeArea.Left,
            defaultMenuButtonBorderMargin.Top + safeArea.Top,
            defaultMenuButtonBorderMargin.Right,
            defaultMenuButtonBorderMargin.Bottom);
    }

	protected override void OnNavigatedTo(NavigatedToEventArgs args)
	{
		base.OnNavigatedTo(args);
		//scrollY = 0;
        UpdateAllAfterScroll();
    }

	private void UpdateWonderColors()
	{
		var config = WonderViewConfig.GetWonderViewConfig(viewModel.CurrentWonder.Type);

		headerTitleTopSeparatorContainer
			.Where(v => v is Rectangle)
			.Cast<Rectangle>()
			.ToList()
			.ForEach(r =>
			{
				r.Fill = config.SecondaryColor;
			});

		headerTitleBottomSeparator.LineColor = config.SecondaryColor;
		Background = config.PrimaryColor;
        headerImageOverlay.Fill = config.PrimaryColor;
    }

    private void UpdateAllAfterScroll()
    {
        UpdatePositionsAfterScroll();
        UpdateHeaderImageAfterScroll();
        UpdateOpacityAfterScroll();
        UpdateOnlyOnTopVisibleAfterScroll();
        UpdateWindowImageAfterScroll();
        UpdateUImagesAfterScroll();
    }

    private void UpdatePositionsAfterScroll()
    {
		isHeaderImageContainerMinimized = headerImageContainer.Margin.Top - scrollY < 0;
        var newHeaderSectionTitleContainerTranslationY = Math.Max(-scrollY, minimizedHeaderImageContainerHeight - headerSectionTitleContainer.Margin.Top - wonderSectionTitleCutOutHeight);
        var newHeaderImageContainerTranslationY = isHeaderImageContainerMinimized ? -headerImageContainer.Margin.Top : -scrollY;

        headerTitleContainer.TranslationY = -scrollY * 0.7;

        if (newHeaderSectionTitleContainerTranslationY != headerSectionTitleContainer.TranslationY)
            headerSectionTitleContainer.TranslationY = newHeaderSectionTitleContainerTranslationY;

        if (newHeaderImageContainerTranslationY != headerImageContainer.TranslationY)
            headerImageContainer.TranslationY = newHeaderImageContainerTranslationY;
    }

	private void UpdateHeaderImageAfterScroll()
    {
		UpdateImageClip();
		
		var newHeight = Math.Clamp(defaultHeaderImageContainerHeight + headerImageContainer.Margin.Top - scrollY, minimizedHeaderImageContainerHeight, defaultHeaderImageContainerHeight);

        if (headerImageContainer.HeightRequest != newHeight)
            headerImageContainer.HeightRequest = newHeight;
    }

    private void UpdateOpacityAfterScroll()
    {
        var offsetHeightRatio = (headerImageContainer.Margin.Top + headerImageContainer.TranslationY) / headerImageContainer.Margin.Top;
        var illustrationOpacity = Math.Clamp(offsetHeightRatio, 0, 1);
        var headerTitleOpacity = Math.Clamp((illustrationOpacity * 1.2) - 0.2, 0, 1);

        if (headerTitleOpacity != headerTitleContainer.Opacity)
            headerTitleContainer.Opacity = headerTitleOpacity;
        if (illustrationOpacity != headerIllustrationContainer.Opacity)
            headerIllustrationContainer.Opacity = illustrationOpacity;

        if (offsetHeightRatio > 0)
        {
            var newOpacity = Math.Clamp(ScrolledTopHeaderImageOpacity + ((1 - illustrationOpacity) * (1 - ScrolledTopHeaderImageOpacity)), 0, 1);
            
            if (headerImageContainer.Opacity != newOpacity)
                headerImageContainer.Opacity = newOpacity;
            if (headerImageOverlay.Opacity != 0)
                headerImageOverlay.Opacity = 0;
#if IOS
            // TODO: There is a problem with input transparency of an Image control on iOS
            // Because of this, Z order of controls has to be different in different scroll positions
            collectionView.ZIndex = 1;
            headerImageContainer.ZIndex = 0;
#endif
        }
        else
        {
            var newOpacity = Math.Clamp((1 - (headerImageContainer.Height / defaultHeaderImageContainerHeight)) * MinimizedHeaderImageOpacity, 0, 1);

            if (headerImageContainer.Opacity != 1)
                headerImageContainer.Opacity = 1;
            if (headerImageOverlay.Opacity != newOpacity)
                headerImageOverlay.Opacity = newOpacity;
#if IOS
            collectionView.ZIndex = 0;
            headerImageContainer.ZIndex = 1;
#endif
        }
    }

    private void UpdateOnlyOnTopVisibleAfterScroll()
    {
        var newVisible = scrollY <= 5;

        if (newVisible != onlyOnTopVisible)
        {
            headerTitleBottomSeparator.Collapsed = !newVisible;
            UpdateMenuButtonVisibility(newVisible);
        }

        onlyOnTopVisible = scrollY <= 5;
    }

    private void UpdateWindowImageAfterScroll()
    {
        var verticalDelta = (scrollY - windowImageTop + collectionView.Height) / (collectionView.Height / 2d);
        if (factsHistoryWonderSectionViewModel.VerticalDelta != verticalDelta)
            factsHistoryWonderSectionViewModel.VerticalDelta = verticalDelta;
    }

    private void UpdateUImagesAfterScroll()
    {
        var verticalDelta = (scrollY - factsHistoryHeight - (collectionViewHeader.HeightRequest > 0 ? collectionViewHeader.HeightRequest : 0) - constructionHeight + collectionView.Height) / collectionView.Height;
        if (constructionWonderSectionViewModel.VerticalDelta != verticalDelta)
            constructionWonderSectionViewModel.VerticalDelta = verticalDelta;
    }

    private void UpdateSectionsAfterScroll(int centerItemIndex, double verticalDelta)
    {
        if (collectionView.ItemsSource is not IList<WonderSectionViewModel> list)
            return;

#if ANDROID
        // TODO: On Android, the header counts as an item but, not on iOS
        var currentSection = list[Math.Clamp(centerItemIndex - 1, 0, list.Count - 1)];
#else
        var currentSection = list[centerItemIndex];
#endif

        if (wonderSectionTitleView.WonderSection == currentSection)
            return;

        wonderSectionTitleView.TitleSwitchDirection = verticalDelta > 0;
        wonderSectionTitleView.WonderSection = currentSection;

        foreach (var item in collectionView.ItemsSource)
        {
            if (item is not WonderSectionViewModel sectionViewModel || item == currentSection)
                continue;

            sectionViewModel.CollapsedSeparator = true;
        }

        currentSection.CollapsedSeparator = false;
    }

    private void UpdateImageClip()
	{
        if (isHeaderImageContainerMinimized)
        {
            if (headerImageContainer.Clip is not null)
                headerImageContainer.Clip = null;
			return;
        }

        if (viewModel?.CurrentWonder is null)
			return;

        var clip = wonderConfig?.GetPhoto1ClipGeometry?.Invoke(headerImageContainer.Width, headerImageContainer.Height);

        if (clip != headerImageContainer?.Clip)
            headerImageContainer.Clip = clip;
    }

	private void UpdateItemsSource()
	{
        var wonder = viewModel.CurrentWonder;
        wonderConfig = WonderViewConfig.GetWonderViewConfig(viewModel.CurrentWonder.Type);

        if (wonder is null)
            return;

        factsHistoryWonderSectionViewModel = new FactsHistoryWonderSectionViewModel
        {
            Icon = "common_history.png",
            Title = Localization.appBarTitleFactsHistory,
            HistoryInfo1 = wonder.HistoryInfo1,
            HistoryInfo2 = wonder.HistoryInfo2,
            Callout1 = wonder.Callout1,
            PullQuote1Top = wonder.PullQuote1Top,
            PullQuote1Bottom = wonder.PullQuote1Bottom,
            WonderType = viewModel.CurrentWonder.Type,
            CollapsedSeparator = true,
            VisibleCollectiblePosition = wonderConfig.MainPageCollectiblePosition,
        };

        constructionWonderSectionViewModel = new ConstructionWonderSectionViewModel
        {
            Icon = "common_construction.png",
            Title = Localization.appBarTitleConstruction,
            ConstructionInfo1 = wonder.ConstructionInfo1,
            ConstructionInfo2 = wonder.ConstructionInfo2,
            Callout2 = wonder.Callout2,
            VideoCaption = wonder.VideoCaption,
            VideoId = wonder.VideoId,
            WonderType = viewModel.CurrentWonder.Type,
            CollapsedSeparator = true,
            VisibleCollectiblePosition = wonderConfig.MainPageCollectiblePosition,
        };

        var list = new List<WonderSectionViewModel>
        {
            factsHistoryWonderSectionViewModel,
            constructionWonderSectionViewModel,
            new LocationWonderSectionViewModel
            {
                Icon = "common_geography.png",
                Title = Localization.appBarTitleLocation,
                LocationInfo1 = wonder.LocationInfo1,
                LocationInfo2 = wonder.LocationInfo2,
                MapCaption = wonder.MapCaption,
                MapImage = wonderConfig.Map,
                PullQuote2 = wonder.PullQuote2,
                PullQuote2Author = wonder.PullQuote2Author,
                Lat = wonder.Lat,
                Lng = wonder.Lng,
                WonderType = viewModel.CurrentWonder.Type,
                CollapsedSeparator = true,
                VisibleCollectiblePosition = wonderConfig.MainPageCollectiblePosition,
            }
        };

        UpdateCollectionViewContentPadding();

        collectionView.ItemsSource = list;
    }

    private void UpdateCollectionViewContentPadding()
    {
        if (headerIllustrationContainer.HeightRequest < 0)
            return;

        var offset = headerIllustrationContainer.HeightRequest + headerIllustrationContainer.Margin.VerticalThickness + headerTitleContainer.Height + headerImageContainer.HeightRequest + headerSectionTitleContainer.HeightRequest - wonderSectionTitleCutOutHeight;
#if ANDROID
        // TODO: On Android, margin is ignored on first load of the page (after 7.0.58 or 7.0.59 service release)
        collectionViewHeader.HeightRequest = offset;
#elif IOS
        factsHistoryWonderSectionViewModel.Margin = new Thickness(0, offset, 0, 0);
#endif
    }

    private void UpdateHeaderIllustration()
    {
        if (viewModel?.CurrentWonder is null)
            return;

        if (!headerIllustrations.ContainsKey(viewModel.CurrentWonder.Type))
            headerIllustrations.Add(viewModel.CurrentWonder.Type, CreateHeaderIllustration(viewModel.CurrentWonder.Type));

        var illustration = headerIllustrations[viewModel.CurrentWonder.Type];

        headerIllustrationContainer.Clear();
        headerIllustrationContainer.Add(illustration);
    }

    private BaseHeaderIllustration CreateHeaderIllustration(WonderType wonderType)
    {
        return wonderType switch
        {
            WonderType.ChichenItza => new ChichenItzaHeaderIllustration(),
            WonderType.Colosseum => new ColosseumHeaderIllustration(),
            WonderType.ChristRedeemer => new ChristRedeemerHeaderIllustration(),
            WonderType.GreatWall => new GreatWallHeaderIllustration(),
            WonderType.MachuPicchu => new MachuPicchuHeaderIllustration(),
            WonderType.Petra => new PetraHeaderIllustration(),
            WonderType.PyramidsGiza => new PyramidsGizaHeaderIllustration(),
            WonderType.TajMahal => new TajMahalHeaderIllustration(),
            _ => null
        };
    }

    private void UpdateMenuButtonVisibility(bool show)
    {
        var end = show ? 1 : 0;

        if (menuButtonBorder.Opacity == end)
            return;

        menuButtonBorder.AbortAnimation(HideMenuButtonAnimationKey);
        menuButtonBorder.AbortAnimation(ShowMenuButtonAnimationKey);

        var start = menuButtonBorder.Opacity;

        var animation = new Animation(d =>
        {
            menuButtonBorder.Opacity = d;
        }, start, show ? 1 : 0);

        if (show)
            menuButtonBorder.IsVisible = true;

        animation.Commit(menuButtonBorder, show ? ShowMenuButtonAnimationKey : HideMenuButtonAnimationKey, finished: (d, canceled) =>
        {
            if (!canceled)
                menuButtonBorder.IsVisible = show;
        });
    }

    private void ViewModelPropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
	{
		if (e.PropertyName == nameof(IWonderMainPageViewModel.CurrentWonder))
		{
            if (scrollY > 0)
            {
                collectionView.ScrollTo(0, position: ScrollToPosition.Start, animate: false);
                scrollY = 0;

                UpdateAllAfterScroll();
            }

            UpdateHeaderIllustration();
            UpdateWonderColors();
            UpdateImageClip();
            UpdateItemsSource();
        }
        else if (e.PropertyName == nameof(IWonderMainPageViewModel.Collectible) ||
            e.PropertyName == nameof(IWonderMainPageViewModel.Collectible.CollectibleState))
        {
            if (collectionView.ItemsSource is null)
                return;

            foreach (var item in collectionView.ItemsSource)
            {
                if (item is IBaseViewModel viewModel)
                {
                    viewModel.OnPropertyChanged(e.PropertyName);
                }
            }
        }
	}

	private void HeaderTitleContainerSizeChanged(object sender, EventArgs e)
    {
        headerIllustrationContainer.HeightRequest = Height * 0.37;
        headerImageContainer.HeightRequest = defaultHeaderImageContainerHeight;
        headerTitleContainer.Margin = new Thickness(0, headerIllustrationContainer.HeightRequest, 0, 0);
        headerImageContainer.Margin = new Thickness(0, headerTitleContainer.Margin.Top + headerTitleContainer.Height, 0, 0);
        headerSectionTitleContainer.Margin = new Thickness(0, headerImageContainer.Margin.Top + headerImageContainer.HeightRequest - wonderSectionTitleCutOutHeight, 0, 0);

        UpdateCollectionViewContentPadding();
    }

    private void HeaderImageContainerSizeChanged(object sender, EventArgs e)
    {
        UpdateImageClip();
    }

    private void FirstHistoryInfoStackLayoutSizeChanged(object sender, EventArgs e)
    {
        var view = sender as Layout;
        firstHistoryInfoHeight = view.Height + view.Margin.VerticalThickness + view.Padding.VerticalThickness;
    }

    private void ConstructionVerticalStackLayoutSizeChanged(object sender, EventArgs e)
    {
        var view = sender as Layout;
        factsHistoryHeight = view.Height + view.Margin.VerticalThickness + view.Padding.VerticalThickness;
    }

    private void FactsHistoryVerticalStackLayoutSizeChanged(object sender, EventArgs e)
    {
        var view = sender as Layout;
        constructionHeight = view.Height + view.Margin.VerticalThickness + view.Padding.VerticalThickness;
    }

    private void CollectionViewScrolled(object sender, ItemsViewScrolledEventArgs e)
	{
        scrollY = e.VerticalOffset;

        UpdateAllAfterScroll();

        UpdateSectionsAfterScroll(e.CenterItemIndex, e.VerticalDelta);
    }

    private void MenuButtonClicked(object sender, EventArgs e)
    {
        navigationService.GoTo(PageType.MainMenuPage);
    }

    private static async void MapTapped(object sender, TappedEventArgs e)
    {
        var bindable = sender as BindableObject;
        var viewModel = bindable.BindingContext as LocationWonderSectionViewModel;

        await Map.Default.OpenAsync(new Location(viewModel.Lat, viewModel.Lng));
    }
}