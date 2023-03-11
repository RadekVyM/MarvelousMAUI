using Microsoft.Maui.Controls.Shapes;
using SimpleToolkit.Core;
using Marvelous.Core;
using Marvelous.Core.Extensions;
using Marvelous.Core.Interfaces.Services;
using Marvelous.Core.Interfaces.ViewModels;
using Marvelous.Core.Models;
using Marvelous.Maui.Extensions;
using Marvelous.Maui.Models;
using Marvelous.Maui.Services;

namespace Marvelous.Maui.Views.Pages;

public partial class GlobalTimelinePage : BaseContentPage
{
	private const string TimelineEraLabelAnimationKey = "TimelineEraLabelAnimation";
    private const double YearLabelHeight = 60;
	private const double YearLabelWidth = 50;
    private const double YearLabelDifference = 100;
	private const double LeftSpacing = YearLabelWidth + 20 + 30;
	private const double RightSpacing = 20;
    private const double WondersSpacing = 15;
    private const double ImageMargin = 8;

    private int minYear;
	private int maxYear;
    private double scrollY = 0;
	private int labelsCount = 0;

	private double timelineHeight => (labelsCount * YearLabelHeight) - YearLabelHeight;
    private double totalTimelineHeight => timelineHeight + (headerSpacer?.Height ?? 0) + (footerSpacer?.Height ?? 0) + YearLabelHeight;
	private double wonderWidth => (collectionViewGrid.Width - ((wonderLayers.Count - 1) * WondersSpacing) - LeftSpacing - RightSpacing) / (wonderLayers.Count == 0 ? 1 : wonderLayers.Count);
	private double minWonderHeight => wonderWidth * 1.5d;
	private double maxWonderImageHeight => (minWonderHeight * 1.5d) - (2 * ImageMargin);
	private double realSpacing => collectionViewGrid.Height / 2;

	private readonly Dictionary<int, List<LayerWonder>> wonderLayers = new Dictionary<int, List<LayerWonder>>();
	private readonly GlobalTimelineDrawable timelineDrawable;
	private readonly IGlobalTimelinePageViewModel viewModel;


	public GlobalTimelinePage(INavigationService navigationService, IGlobalTimelinePageViewModel viewModel) : base(navigationService)
    {
		BindingContext = this.viewModel = viewModel;

        InitializeComponent();

        App.Current.Resources.TryGetValue("PrimaryColor", out object color);

		graphicsView.Drawable = timelineDrawable = new GlobalTimelineDrawable
		{
			LeftSpacing = LeftSpacing,
            DotColor = color as Color
        };

		viewModel.PropertyChanged += ViewModelPropertyChanged;
        collectionViewGrid.SizeChanged += CollectionViewSizeChanged;
		globalTimelineSlider.Scrolled += GlobalTimelineSliderScrolled;

		Loaded += GlobalTimelinePageLoaded;
		Unloaded += GlobalTimelinePageUnloaded;
	}


	protected override void OnNavigatedTo(NavigatedToEventArgs args)
	{
		base.OnNavigatedTo(args);

		var currentWonder = viewModel.Wonders.FirstOrDefault(w => w.Type == viewModel.CurrentWonderType);
        var labelIndex = GetLabelIndex(currentWonder.StartYr);

		// TODO: Do I want this? Sometimes, it is buggy and scrolls completely to the end even if the labelIndex is always correct
		ScrollTo(labelIndex, true);
    }

	private void UpdateAfterScroll()
    {
        UpdateYearLabels();

        var currentEvent = GetCurrentTimelineEvent();

        if (currentEvent is not null)
            eventCard.Show(currentEvent);
        else
            eventCard.Hide();

        timelineDrawable.CurrentEventYear = currentEvent?.Year ?? int.MinValue;
        timelineDrawable.ScrollY = scrollY;
        graphicsView.Invalidate();

        UpdateImages();
    }

    private void UpdateYears()
	{
        minYear = viewModel.TimelineEvents.MinEventYear();
        maxYear = viewModel.TimelineEvents.MaxEventYear();

		var list = new List<string>();
		
		labelsCount = 0;

        for (double i = minYear; i <= maxYear; i += YearLabelDifference)
		{
            list.Add(Math.Abs(i).ToString());
			labelsCount++;
        }

		collectionView.ItemsSource = list;
		UpdateYearLabels();

		timelineDrawable.TimelineHeight = timelineHeight;
        timelineDrawable.MinYear = minYear;
		timelineDrawable.MaxYear = maxYear;
		timelineDrawable.EventYears = viewModel.TimelineEvents.Select(te => te.Year).ToList();
		graphicsView.Invalidate();
    }

	private void UpdateYearLabels()
	{
		var totalYears = Math.Abs(minYear) + Math.Abs(maxYear);
        var yearsOffset = Math.Round((totalYears * (scrollY / timelineHeight)) / 10d) * 10;
		var year = minYear + yearsOffset;

		var text = Math.Abs(year).ToString();
		var secondText = year < 0 ? Localization.yearBCE : Localization.yearCE;

		if (yearLabel.Text != text)
            yearLabel.Text = text;
        if (secondYearLabel.Text != secondText)
			secondYearLabel.Text = secondText;

		var era = viewModel.GetTimelineEra((int)year);
		var eraTitle = Localization.ResourceManager.GetString(era.TitleKey);

		if (eraTitle != timelineEraLabel.Text)
		{
			timelineEraLabel.AbortAnimation(TimelineEraLabelAnimationKey);

            var animation = new Animation(d =>
			{
				timelineEraLabel.TranslationY = d;
            }, timelineEraLabel.Height * 0.2, 0);

			animation.Commit(timelineEraLabel, TimelineEraLabelAnimationKey);
        }

        timelineEraLabel.Text = eraTitle;
    }

	private void UpdateWonders()
    {
        imagesAbsoluteLayout.Clear();

        WonderLayerService.UpdateWonders(viewModel.Wonders, wonderLayers);

		UpdateWondersPosition();

        UpdateImages();
    }

	private void UpdateWondersPosition()
	{
        WonderLayerService.UpdateWondersPosition(wonderLayers, minYear, maxYear, timelineHeight, realSpacing, minWonderHeight);

		timelineDrawable.WonderLayers = wonderLayers;
		timelineDrawable.WonderWidth = wonderWidth;
		graphicsView.Invalidate();
    }

	private void UpdateImages()
	{
		if (imagesAbsoluteLayout.Height < 0)
			return;

        foreach (var layer in wonderLayers)
        {
            var left = LeftSpacing + (layer.Key * (wonderWidth + WondersSpacing)) + ImageMargin;

            foreach (var wonder in layer.Value)
            {
				var containerHeight = wonder.End - wonder.Start - (2 * ImageMargin);
                var height = Math.Min(containerHeight, maxWonderImageHeight);
				var top = wonder.Start + ImageMargin - scrollY;
				var bottom = wonder.Start + ImageMargin - scrollY + containerHeight;
				var visibleHeight = Math.Min(wonder.Start + ImageMargin - scrollY + containerHeight, imagesAbsoluteLayout.Height) - Math.Max((wonder.Start + ImageMargin - scrollY), 0);
				var offset = Math.Abs(visibleHeight - height) / 2;

				if (top + height < imagesAbsoluteLayout.Height)
					top = Math.Min(Math.Max(top, 0) + offset, bottom - height);

                var rect = new Rect(left, top, wonderWidth - (2 * ImageMargin), height);

				if ((rect.Top < 0 && rect.Bottom < 0) || (rect.Top > imagesAbsoluteLayout.Height && rect.Bottom > imagesAbsoluteLayout.Height))
				{
					if (imagesAbsoluteLayout.Contains(wonder.Image))
						imagesAbsoluteLayout.Remove(wonder.Image);
                    continue;
                }

				wonder.Image ??= CreateImage(wonder.ImagePath);

                if (!imagesAbsoluteLayout.Contains(wonder.Image))
                    imagesAbsoluteLayout.Add(wonder.Image);

                imagesAbsoluteLayout.LayoutChildTo(wonder.Image, rect);
            }
        }
    }

    private void ScrollTo(int labelIndex, bool animate = false)
    {
#if ANDROID
        // TODO: On Android, the header counts as an item but, not on iOS
        var scrollToIndex = labelIndex + 1;
#else
        var scrollToIndex = labelIndex;
#endif

        collectionView.ScrollTo(scrollToIndex, position: ScrollToPosition.Center, animate: animate);
    }

    private Image CreateImage(string imagePath)
	{
		var image = new Image
		{
			Source = imagePath,
			Aspect = Aspect.AspectFill,
            Style = new Style(typeof(Image))
        };

		image.SizeChanged += ImageSizeChanged;

		return image;
	}

    private TimelineEvent GetCurrentTimelineEvent()
    {
        var currentYear = GetCurrentYear();
        var yearsOffset = 10;
        TimelineEvent currentEvent = null;

        foreach (var e in viewModel.TimelineEvents)
        {
            if (e.Year <= (currentYear + yearsOffset) && e.Year >= (currentYear - yearsOffset))
            {
                currentEvent = e;
                break;
            }
        }

        return currentEvent;
    }

    private int GetLabelIndex(int year)
    {
        var yearLabelDifference = (int)YearLabelDifference;
        return (((year / yearLabelDifference) * yearLabelDifference) + Math.Abs(minYear)) / yearLabelDifference;
    }

    private int GetCurrentYear()
    {
        var totalYears = Math.Abs(minYear) + Math.Abs(maxYear);
        var yearsOffset = (int)Math.Round(totalYears * (scrollY / timelineHeight));

        return minYear + yearsOffset;
    }

    private void GlobalTimelinePageUnloaded(object sender, EventArgs e)
    {
    }

    private void GlobalTimelinePageLoaded(object sender, EventArgs e)
    {
        eventCard.Opacity = 0;
    }

    private void CollectionViewSizeChanged(object sender, EventArgs e)
    {
        var labelSpacing = realSpacing - (YearLabelHeight / 2);

		headerSpacer.HeightRequest = labelSpacing;
        footerSpacer.HeightRequest = labelSpacing;

		UpdateWondersPosition();

        horizontalLine.X2 = collectionViewGrid.Width;

		timelineDrawable.TimelineHeight = timelineHeight;
		timelineDrawable.TopSpacing = realSpacing;
		timelineDrawable.BottomSpacing = realSpacing;
        graphicsView.Invalidate();

		UpdateImages();

        globalTimelineSlider.RelativeThumbWidth = collectionViewGrid.Height / totalTimelineHeight;
    }

    private static void ImageSizeChanged(object sender, EventArgs e)
    {
        var image = sender as Image;

        var previousClip = image.Clip as RoundRectangleGeometry;

        if (previousClip?.Rect.Width == image.Width && previousClip?.Rect.Height == image.Height)
            return;

        var clip = new RoundRectangleGeometry(Math.Min(image.Width, image.Height) / 2, new Rect(0, 0, image.Width, image.Height));
        image.Clip = clip;
    }

    private void ViewModelPropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
    {
        if (e.PropertyName == nameof(IGlobalTimelinePageViewModel.TimelineEvents))
        {
            globalTimelineSlider.UpdateWonderLayers(viewModel.Wonders, viewModel.TimelineEvents);
            UpdateYears();
        }
		else if (e.PropertyName == nameof(IGlobalTimelinePageViewModel.Wonders))
        {
            globalTimelineSlider.UpdateWonderLayers(viewModel.Wonders, viewModel.TimelineEvents);
            UpdateWonders();
		}
		else if (e.PropertyName == nameof(IGlobalTimelinePageViewModel.CurrentWonderType))
        {
            globalTimelineSlider.CurrentWonderType = viewModel.CurrentWonderType;
            UpdateWonders();
        }
    }

    private void MenuBackButtonClicked(object sender, EventArgs e)
	{
		navigationService.GoBack();
	}

    private void GlobalTimelineSliderScrolled(double relativePosition)
	{
		var labelIndex = Math.Min((int)Math.Round(labelsCount * relativePosition), labelsCount - 1);

        scrollY = labelIndex * YearLabelHeight;
        ScrollTo(labelIndex);

        UpdateAfterScroll();
    }

	private void CollectionViewScrolled(object sender, ItemsViewScrolledEventArgs e)
	{
		if (globalTimelineSlider.IsPanning)
			return;

		scrollY += e.VerticalDelta;
		globalTimelineSlider.RelativeThumbPosition = scrollY / timelineHeight;

		UpdateAfterScroll();
	}

	private class GlobalTimelineDrawable : IDrawable
	{
		private const float DefaultDotRadius = 1.2f;
		private const float CurrentDotRadius = 3.5f;

        public double ScrollY { get; set; }
		public double TimelineHeight { get; set; }
		public double WonderWidth { get; set; }
		public double LeftSpacing { get; set; }
		public double TopSpacing { get; set; }
        public double BottomSpacing { get; set; }
		public int MinYear { get; set; }
		public int MaxYear { get; set; }
		public int CurrentEventYear { get; set; }
		public IList<int> EventYears { get; set; }
        public Color DotColor { get; set; }
		public Dictionary<int, List<LayerWonder>> WonderLayers = new Dictionary<int, List<LayerWonder>>();

        public void Draw(ICanvas canvas, RectF dirtyRect)
		{
			canvas.SaveState();

			canvas.SetFillPaint(new SolidPaint(DotColor), dirtyRect);
			DrawDots(canvas, dirtyRect);

			canvas.SetShadow(SizeF.Zero, 0, null);
            DrawWonders(canvas, dirtyRect);

            canvas.RestoreState();
		}

		private void DrawWonders(ICanvas canvas, RectF dirtyRect)
		{
			foreach (var layer in WonderLayers)
			{
				var left = LeftSpacing + (layer.Key * (WonderWidth + WondersSpacing));

				foreach (var wonder in layer.Value)
				{
					var rect = new Rect(left, wonder.Start - ScrollY, WonderWidth, wonder.End - wonder.Start);

                    if ((rect.Top < 0 && rect.Bottom < 0) || (rect.Top > dirtyRect.Height && rect.Bottom > dirtyRect.Height))
                        continue;

                    canvas.SetFillPaint(new SolidPaint(wonder.Color), rect);
                    canvas.FillRoundedRectangle(rect, WonderWidth / 2d);
				}
			}
		}

		private void DrawDots(ICanvas canvas, RectF dirtyRect)
		{
			if (EventYears is null || !EventYears.Any())
				return;

            var totalYears = Math.Abs(MinYear) + Math.Abs(MaxYear);
			var yearOffset = TimelineHeight / totalYears;

			foreach (var year in EventYears)
			{
				var offsetYear = year + Math.Abs(MinYear);
				var top = TopSpacing - ScrollY + (offsetYear * yearOffset);

				if (top < 0 || top > dirtyRect.Height)
					continue;

				canvas.SetShadow(new SizeF(0, 0), (year == CurrentEventYear ? CurrentDotRadius : DefaultDotRadius) * 3f, DotColor);
				canvas.FillCircle((float)LeftSpacing - 20, (float)top, year == CurrentEventYear ? CurrentDotRadius : DefaultDotRadius);
			}
        }
	}
}