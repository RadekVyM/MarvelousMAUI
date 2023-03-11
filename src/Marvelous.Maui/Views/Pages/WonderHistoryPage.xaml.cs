using Microsoft.Maui.Controls;
using Microsoft.Maui.Controls.Shapes;
using Marvelous.Core.Extensions;
using Marvelous.Core.Interfaces.Services;
using Marvelous.Core.Interfaces.ViewModels;
using Marvelous.Core.Models;
using Marvelous.Maui.Extensions;
using Marvelous.Maui.Models;
using Marvelous.Maui.Services;
using TabBar = Marvelous.Maui.Views.Controls.TabBar;

namespace Marvelous.Maui.Views.Pages;

public partial class WonderHistoryPage : BaseContentPage
{
    private readonly IWonderHistoryPageViewModel viewModel;
    private readonly TimelineDrawable drawable;
    private readonly Dictionary<int, List<LayerWonder>> wonderLayers = new Dictionary<int, List<LayerWonder>>();
    private int minYear;
    private int maxYear;

    private double wonderSpacing => 3;
    private double minWonderWidth => (headerGraphicsView.Height - ((wonderLayers.Count - 1) * wonderSpacing)) / wonderLayers.Count;
    private double headerHeight => Width * 1.2d;
    private double scrollY = 0;


    public WonderHistoryPage(
        INavigationService navigationService,
        IWonderHistoryPageViewModel viewModel) : base(navigationService)
    {
        BindingContext = this.viewModel = viewModel;

        InitializeComponent();

        headerGraphicsView.Drawable = drawable = new TimelineDrawable
        {
            Color = Colors.White
        };

        if (viewModel.CurrentWonder is not null)
            headerImage.Source = WonderViewConfig.GetWonderViewConfig(viewModel.CurrentWonder.Type).Flattened;

        viewModel.PropertyChanged += ViewModelPropertyChanged;

        SizeChanged += WonderHistoryPageSizeChanged;
        headerGraphicsView.SizeChanged += HeaderGraphicsViewSizeChanged;
    }


    private void ViewModelPropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
    {
        if (e.PropertyName == nameof(IWonderHistoryPageViewModel.CurrentWonder))
        {
            headerImage.Source = WonderViewConfig.GetWonderViewConfig(viewModel.CurrentWonder.Type).Flattened;

            drawable.SelectedWonder = viewModel.CurrentWonder.Type;

            headerGraphicsView.Invalidate();
        }
        else if (e.PropertyName == nameof(IWonderHistoryPageViewModel.Wonders))
        {
            minYear = viewModel.Wonders.MinWonderYear();
            maxYear = viewModel.Wonders.MaxWonderYear();

            WonderLayerService.UpdateWonders(viewModel.Wonders, wonderLayers);
            WonderLayerService.UpdateWondersPosition(wonderLayers, minYear, maxYear, headerGraphicsView.Width, 0, minWonderWidth);

            drawable.WonderLayers = wonderLayers;

            headerGraphicsView.Invalidate();
        }
    }

    private void WonderHistoryPageSizeChanged(object sender, EventArgs e)
    {
        collectionHeaderGrid.HeightRequest = headerHeight;
        headerGrid.HeightRequest = headerHeight;

        headerImageBorder.WidthRequest = Width / 2;
        headerImageBorder.StrokeShape = new RoundRectangle
        {
            CornerRadius = new CornerRadius(Width / 2, Width / 2, 0, 0)
        };
    }

    private void HeaderGraphicsViewSizeChanged(object sender, EventArgs e)
    {
        WonderLayerService.UpdateWondersPosition(wonderLayers, minYear, maxYear, headerGraphicsView.Width, 0, minWonderWidth);

        drawable.MinWonderHeight = minWonderWidth;
        drawable.WonderSpacing = wonderSpacing;

        headerGraphicsView.Invalidate();
    }

    private void CollectionViewScrolled(object sender, ItemsViewScrolledEventArgs e)
    {
        scrollY = e.VerticalOffset;

        var opacity = 1 - Math.Clamp(scrollY / (headerHeight * 0.75d), 0.05, 1);

        headerGrid.Opacity = opacity;
    }


    private class TimelineDrawable : IDrawable
    {
        public Dictionary<int, List<LayerWonder>> WonderLayers { get; set; } = new Dictionary<int, List<LayerWonder>>();
        public double MinWonderHeight { get; set; }
        public double WonderSpacing { get; set; }
        public Color Color { get; set; }
        public WonderType SelectedWonder { get; set; }

        public void Draw(ICanvas canvas, RectF dirtyRect)
        {
            canvas.SaveState();

            var config = WonderViewConfig.GetWonderViewConfig(SelectedWonder);

            canvas.DrawGlobalTimeline(
                dirtyRect,
                WonderLayers,
                MinWonderHeight,
                WonderSpacing,
                SelectedWonder,
                1,
                Color,
                new SolidPaint(config.SecondaryColor));

            canvas.RestoreState();
        }
    }
}