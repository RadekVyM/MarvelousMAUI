using Marvelous.Core.Interfaces.Services;
using Marvelous.Core.Interfaces.ViewModels;
using Marvelous.Maui.Models;

namespace Marvelous.Maui.Views.Pages;

public partial class WonderArtifactsPage : BaseContentPage
{
    private double lastCenterItemIndex;
    private IWonderArtifactsPageViewModel viewModel => BindingContext as IWonderArtifactsPageViewModel;


    public WonderArtifactsPage(INavigationService navigationService, IWonderArtifactsPageViewModel viewModel) : base(navigationService)
	{
		BindingContext = viewModel;
		
		InitializeComponent();

        SizeChanged += WonderArtifactsPageSizeChanged;
    }


    private void UpdateImageScales(CarouselView carouselView, int centerItemIndex, bool animate = false)
    {
        foreach (var item in carouselView.ItemsSource)
        {
            if (item is ArtifactCarouselItemViewModel viewModel)
            {
                var newScale = viewModel.Position == centerItemIndex ? 1 : 0;

                if (viewModel.IsImageScaleAnimated != animate)
                    viewModel.IsImageScaleAnimated = animate;

                if (viewModel.ImageScale != newScale)
                    viewModel.ImageScale = newScale;
            }
        }
    }

    protected override void OnSafeAreaChanged(Thickness safeArea)
    {
        titleLabel.Margin = new Thickness(0, safeArea.Top, 0, 0);
        rootGrid.Margin = new Thickness(safeArea.Left, 0, safeArea.Right, safeArea.Bottom + Controls.TabBar.TabBarHeight - 1);
    }

    private void WonderArtifactsPageSizeChanged(object sender, EventArgs e)
    {
        var radius = Width / 2;
        roundRectangle.CornerRadius = new CornerRadius(radius, radius, 0, 0);
    }

    private void CarouselViewScrolled(object sender, ItemsViewScrolledEventArgs e)
    {
        if (lastCenterItemIndex != e.CenterItemIndex)
            UpdateImageScales(sender as CarouselView, e.CenterItemIndex, false); // TODO: I turned the animation off because it is choppy on Android
        lastCenterItemIndex = e.CenterItemIndex;
    }

    private void CarouselViewPropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
    {
        var carouselView = sender as CarouselView;

        if (e.PropertyName == CarouselView.ItemsSourceProperty.PropertyName)
        {
            UpdateImageScales(carouselView, viewModel.CurrentArtifactIndex);
        }
    }
}