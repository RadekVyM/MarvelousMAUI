using System.ComponentModel;
using CommunityToolkit.Maui.Views;
using Marvelous.Core.Interfaces.Services;
using Marvelous.Core.Interfaces.ViewModels;
using Marvelous.Maui.Views.Controls;
using Marvelous.Maui.Models;

namespace Marvelous.Maui.Views.Pages;

public partial class CollectionPage : BaseContentPage
{
    private PercentageBarDrawable percentageBarDrawable;
    private ICollectionPageViewModel viewModel => BindingContext as ICollectionPageViewModel;


    public CollectionPage(INavigationService navigationService, ICollectionPageViewModel viewModel) : base(navigationService)
	{
		BindingContext = viewModel;

        InitializeComponent();

        App.Current.Resources.TryGetValue("PrimaryColor", out object foregroundColor);

        percentageBarGraphicsView.Drawable = percentageBarDrawable = new PercentageBarDrawable
        {
            BackgroundColor = Colors.Gray,
            ForegroundColor = foregroundColor as Color,
            Value = viewModel.DiscoveredCollectiblesPercentage / 100d
        };

        viewModel.PropertyChanged += ViewModelPropertyChanged;

        UpdateShadowOffset(0);
    }


    private void UpdateShadowOffset(double scrollY)
    {
        var offset = (float)Math.Min(0.1 + (scrollY / (darkShadow.HeightRequest * 2)), 1);
        darkShadow.Offset = offset;
    }

    protected override void OnSafeAreaChanged(Thickness safeArea)
    {
        Padding = new Thickness(safeArea.Left, safeArea.Top, safeArea.Right, 0);
        countGrid.Padding = new Thickness(0, 0, 0, safeArea.Bottom);
    }

    private void ViewModelPropertyChanged(object sender, PropertyChangedEventArgs e)
    {
        if (e.PropertyName == nameof(ICollectionPageViewModel.DiscoveredCollectiblesPercentage))
        {
            percentageBarDrawable.Value = viewModel.DiscoveredCollectiblesPercentage / 100d;
            percentageBarGraphicsView.Invalidate();
        }
    }

    private void MenuBackButtonClicked(object sender, EventArgs e)
    {
        Shell.Current.Navigation.PopToRootAsync();
    }

    private async void ResetClicked(object sender, EventArgs e)
    {
        var bindable = sender as BindableObject;

#if IOS
        var width = this.Width - 40;
#else
        var width = this.Width;
#endif

        var dialog = new ConfirmCollectionResetPopup(width);
        var confirmed = await this.ShowPopupAsync(dialog);

        if (confirmed is bool c && c)
            viewModel?.ResetCollectionCommand?.Execute(null);
    }

    private void CollectionViewScrolled(object sender, ItemsViewScrolledEventArgs e)
    {
        UpdateShadowOffset(e.VerticalOffset);
    }

    private void NewlyDiscoveredItemsTapped(object sender, TappedEventArgs e)
    {
        var lastDiscoveredCollectible = viewModel.LastDiscoveredCollectible;

        if (lastDiscoveredCollectible is null)
            return;

        var scrollToIndex = 0;

        foreach (var item in collectionView.ItemsSource)
        {
            if (item is CollectibleGroupViewModel group && group.Any(c => c.ArtifactId == lastDiscoveredCollectible.ArtifactId))
                break;

            scrollToIndex++;
        }

        collectionView.ScrollTo(scrollToIndex, position: ScrollToPosition.Center);
    }


    private class PercentageBarDrawable : IDrawable
    {
        public Color BackgroundColor { get; set; }
        public Color ForegroundColor { get; set; }
        public double Value { get; set; }

        public void Draw(ICanvas canvas, RectF dirtyRect)
        {
            canvas.SaveState();

            var radius = dirtyRect.Height / 2f;
            RectF selectedRect = new Rect(dirtyRect.X, dirtyRect.Y, dirtyRect.Width * Value, dirtyRect.Height);

            canvas.SetFillPaint(new SolidPaint(BackgroundColor), dirtyRect);
            canvas.FillRoundedRectangle(dirtyRect, radius);

            canvas.SetFillPaint(new SolidPaint(ForegroundColor), selectedRect);
            canvas.FillRoundedRectangle(selectedRect, radius);

            canvas.RestoreState();
        }
    }
}
