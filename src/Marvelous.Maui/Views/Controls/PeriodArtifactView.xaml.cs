using System.ComponentModel;
using Microsoft.Maui.Graphics;
using Marvelous.Core.Interfaces.ViewModels;
using Marvelous.Maui.Extensions;
using Marvelous.Maui.Models;
using Marvelous.Maui.Services;

namespace Marvelous.Maui.Views.Controls;

public partial class PeriodArtifactView : Grid
{
    private const string CollapseAnimationKey = "CollapseAnimation";
    private const string ExpandAnimationKey = "ExpandAnimation";
    private const float ArrowThumbWidth = 18;
    private const float AllowedTapOffset = 5;
    private const double SmallLabelCollapsedOffset = 0;
    private const double SmallLabelCollapsedOpacity = 1;
    private const double SmallLabelExpandedOffset = -8;
    private const double SmallLabelExpandedOpacity = 0.01;
    private const double LargeLabelCollapsedOffset = 10;
    private const double LargeLabelCollapsedOpacity = 0;
    private const double LargeLabelExpandedOffset = 0;
    private const double LargeLabelExpandedOpacity = 1;

    private readonly PeriodArtifactBackroundDrawable backgroundDrawable;
    private readonly TapGestureRecognizer smallTapGestureRecognizer;
    private Thickness safeArea;
    private PointF startingPoint;
    private PointF lastDraggingPoint;
    private int startingFromYear;
    private int startingToYear;
    private bool draggingLeft;
    private bool draggingRight;
    private bool isSmall = false;

    private Thickness TimelineMargin => new Thickness(20 + SafeArea.Left, 50, 20 + SafeArea.Right, 20 + SafeArea.Bottom);
    private Rect TimelineRect =>
        new Rect(TimelineMargin.Left + ArrowThumbWidth, TimelineMargin.Top, Width - TimelineMargin.HorizontalThickness - (2 * ArrowThumbWidth), Height - TimelineMargin.VerticalThickness);
    private Rect CollapsedRect
    {
        get
        {
            var width = smallYearsStackLayout.Width;
            var height = smallYearsStackLayout.Height;
            var left = ((this.Width - width - smallYearsStackLayout.Margin.HorizontalThickness) / 2d) + smallYearsStackLayout.Margin.Left;
            var top = ((this.Height - height - smallYearsStackLayout.Margin.VerticalThickness) / 2d) + smallYearsStackLayout.Margin.Top;

            return new Rect(left, top, width, height);
        }
    }
    private IArtifactsPageViewModel viewModel => BindingContext as IArtifactsPageViewModel;

    public Thickness SafeArea
    {
        get => safeArea;
        set
        {
            safeArea = value;

            if (backgroundDrawable is not null)
                backgroundDrawable.TimelineMargin = TimelineMargin;

            smallYearsStackLayout.Margin = new Thickness(value.Left, 0, value.Right, value.Bottom);

            backgroundGraphicsView?.Invalidate();
        }
    }

    public WonderLayerService WonderLayerService { get; set; }


    public PeriodArtifactView()
    {
        InitializeComponent();

        App.Current.Resources.TryGetValue("DarkSecondaryColor", out object backgroundColor);
        App.Current.Resources.TryGetValue("SuperDarkSecondaryColor", out object timelineColor);
        App.Current.Resources.TryGetValue("PrimaryColor", out object searchColor);

        backgroundGraphicsView.Drawable = backgroundDrawable = new PeriodArtifactBackroundDrawable
        {
            BackgroundColor = backgroundColor as Color,
            TimelineColor = timelineColor as Color,
            SearchColor = searchColor as Color,
        };

        smallTapGestureRecognizer = new TapGestureRecognizer();
        smallTapGestureRecognizer.Tapped += SmallTapped;

        SizeChanged += PeriodArtifactViewSizeChanged;
    }


    public void ToggleCollapse()
    {
        if (isSmall)
            Expand();
        else
            Collapse();
    }

    private void Collapse()
    {
        this.AbortAnimation(CollapseAnimationKey);
        this.AbortAnimation(ExpandAnimationKey);

        isSmall = true;
        SetExpandedStateProperties();
        UpdateInputTransparency();

        var animation = new Animation();

        animation.Add(0, 0.5, new Animation(d =>
        {
            closeIcon.Opacity = largeYearsStackLayout.Opacity = Lerp(LargeLabelExpandedOpacity, LargeLabelCollapsedOpacity, d);
            largeYearsStackLayout.TranslationY = Lerp(LargeLabelExpandedOffset, LargeLabelCollapsedOffset, d);
        }));

        animation.Add(0.5, 1, new Animation(d =>
        {
            smallYearsStackLayout.Opacity = Lerp(SmallLabelExpandedOpacity, SmallLabelCollapsedOpacity, d);
            smallYearsStackLayout.TranslationY = Lerp(SmallLabelExpandedOffset, SmallLabelCollapsedOffset, d);
        }));

        animation.Add(0, 1, new Animation(d =>
        {
            backgroundDrawable.CollapseProgress = d;
            backgroundGraphicsView.Invalidate();
        }, 0, 1));

        animation.Commit(this, CollapseAnimationKey, finished: (d, b) =>
        {
            SetCollapsedStateProperties();
            backgroundGraphicsView.Invalidate();
        });
    }

    private void Expand()
    {
        this.AbortAnimation(CollapseAnimationKey);
        this.AbortAnimation(ExpandAnimationKey);

        isSmall = false;
        SetCollapsedStateProperties();
        UpdateInputTransparency();

        var animation = new Animation();

        animation.Add(0.5, 1, new Animation(d =>
        {
            closeIcon.Opacity = largeYearsStackLayout.Opacity = Lerp(LargeLabelCollapsedOpacity, LargeLabelExpandedOpacity, d);
            largeYearsStackLayout.TranslationY = Lerp(LargeLabelCollapsedOffset, LargeLabelExpandedOffset, d);
        }));

        animation.Add(0, 0.5, new Animation(d =>
        {
            smallYearsStackLayout.Opacity = Lerp(SmallLabelCollapsedOpacity, SmallLabelExpandedOpacity, d);
            smallYearsStackLayout.TranslationY = Lerp(SmallLabelCollapsedOffset, SmallLabelExpandedOffset, d);
        }));

        animation.Add(0, 1, new Animation(d =>
        {
            backgroundDrawable.CollapseProgress = d;
            backgroundGraphicsView.Invalidate();
        }, 1, 0));

        animation.Commit(this, ExpandAnimationKey, finished: (d, b) =>
        {
            SetExpandedStateProperties();
            backgroundGraphicsView.Invalidate();
        });
    }

    private void SetCollapsedStateProperties()
    {
        smallYearsStackLayout.Opacity = SmallLabelCollapsedOpacity;
        smallYearsStackLayout.TranslationY = SmallLabelCollapsedOffset;
        largeYearsStackLayout.Opacity = LargeLabelCollapsedOpacity;
        largeYearsStackLayout.TranslationY = LargeLabelCollapsedOffset;
        closeIcon.Opacity = LargeLabelCollapsedOpacity;
        backgroundDrawable.CollapsedRect = CollapsedRect;
        backgroundDrawable.CollapseProgress = 1;
    }

    private void SetExpandedStateProperties()
    {
        smallYearsStackLayout.Opacity = SmallLabelExpandedOpacity;
        smallYearsStackLayout.TranslationY = SmallLabelExpandedOffset;
        largeYearsStackLayout.Opacity = LargeLabelExpandedOpacity;
        largeYearsStackLayout.TranslationY = LargeLabelExpandedOffset;
        closeIcon.Opacity = LargeLabelExpandedOpacity;
        backgroundDrawable.CollapsedRect = CollapsedRect;
        backgroundDrawable.CollapseProgress = 0;
    }

    private static double Lerp(double from, double to, double position)
    {
        return from + ((to - from) * position);
    }

    private void TappedOutside()
    {
        Collapse();
    }

    private void UpdateInputTransparency()
    {
        largeGrid.InputTransparent = isSmall;
        backgroundGraphicsView.InputTransparent = isSmall;
        smallYearsStackLayout.InputTransparent = !isSmall;
        smallYearsLabel.InputTransparent = !isSmall;
        calendarIcon.InputTransparent = !isSmall;

        smallYearsStackLayout.GestureRecognizers.Clear();

        if (isSmall)
            smallYearsStackLayout.GestureRecognizers.Add(smallTapGestureRecognizer);
    }

    private void WhateverEndInteraction(PointF point)
    {
        var vector = point - startingPoint;
        var length = Math.Sqrt((vector.Width * vector.Width) + (vector.Height * vector.Height));

        if (length < AllowedTapOffset && !draggingLeft && !draggingRight)
            TappedOutside();
        else if (draggingLeft || draggingRight)
            DarggingEnd();
    }

    private void DarggingEnd()
    {
        viewModel.UpdateSearches();
    }

    private int GetYearOffset(SizeF vector)
    {
        var yearWidth = viewModel.MaxYear - viewModel.MinYear;
        return (int)Math.Round(yearWidth * (vector.Width / TimelineRect.Width));
    }

    private void MoveSelection(SizeF vector)
    {
        var yearOffset = GetYearOffset(vector);

        var fromYear = startingFromYear + yearOffset;
        var toYear = startingToYear + yearOffset;

        if (fromYear < viewModel.MinYear)
        {
            var offset = viewModel.MinYear - fromYear;
            fromYear = viewModel.MinYear;
            toYear += offset;
        }
        if (toYear > viewModel.MaxYear)
        {
            var offset = toYear - viewModel.MaxYear;
            fromYear -= offset;
            toYear = viewModel.MaxYear;
        }

        viewModel.FromYear = fromYear;
        viewModel.ToYear = toYear;
    }

    private void MoveSelectionLeftEdge(SizeF vector)
    {
        var yearOffset = GetYearOffset(vector);

        var fromYear = Math.Clamp(startingFromYear + yearOffset, viewModel.MinYear, startingToYear - IArtifactsPageViewModel.MinYearsRange);
        viewModel.FromYear = fromYear;
    }

    private void MoveSelectionRightEdge(SizeF vector)
    {
        var yearOffset = GetYearOffset(vector);

        var toYear = Math.Clamp(startingToYear + yearOffset, startingFromYear + IArtifactsPageViewModel.MinYearsRange, viewModel.MaxYear);
        viewModel.ToYear = toYear;
    }

    private void StartInteraction(object sender, TouchEventArgs e)
    {
        startingPoint = lastDraggingPoint = e.Touches.FirstOrDefault();
        startingToYear = viewModel.ToYear;
        startingFromYear = viewModel.FromYear;

        var yearWidth = viewModel.MaxYear - viewModel.MinYear;
        var relativeLeft = (viewModel.FromYear - viewModel.MinYear) / (float)yearWidth;
        var relativeRight = (viewModel.ToYear - viewModel.MinYear) / (float)yearWidth;
        var left = (TimelineRect.Width * relativeLeft) + TimelineRect.Left;
        var right = (TimelineRect.Width * relativeRight) + TimelineRect.Left;
        var rect = new Rect(left, TimelineRect.Top, right - left, TimelineRect.Height);
        var leftThumbRect = new Rect(left - ArrowThumbWidth, TimelineRect.Top, ArrowThumbWidth, TimelineRect.Height);
        var rightThumbRect = new Rect(right, TimelineRect.Top, ArrowThumbWidth, TimelineRect.Height);

        if (rect.Contains(startingPoint))
        {
            draggingLeft = true;
            draggingRight = true;
        }
        else if (leftThumbRect.Contains(startingPoint))
        {
            draggingLeft = true;
            draggingRight = false;
        }
        else if (rightThumbRect.Contains(startingPoint))
        {
            draggingLeft = false;
            draggingRight = true;
        }
        else
        {
            draggingLeft = false;
            draggingRight = false;
        }
    }

    private void DragInteraction(object sender, TouchEventArgs e)
    {
        lastDraggingPoint = e.Touches.FirstOrDefault();
        var vector = lastDraggingPoint - startingPoint;

        if (draggingRight && draggingLeft)
            MoveSelection(vector);
        else if (draggingLeft)
            MoveSelectionLeftEdge(vector);
        else if (draggingRight)
            MoveSelectionRightEdge(vector);
    }

    private void EndInteraction(object sender, TouchEventArgs e)
    {
        var point = e.Touches.FirstOrDefault();
        WhateverEndInteraction(point);
    }

    private void CancelInteraction(object sender, EventArgs e)
    {
        if (draggingLeft || draggingRight)
            DarggingEnd();
    }

    protected override void OnBindingContextChanged()
    {
        base.OnBindingContextChanged();

        if (viewModel is not null)
        {
            viewModel.PropertyChanged -= ViewModelPropertyChanged;
            viewModel.PropertyChanged += ViewModelPropertyChanged;
        }
    }

    private void PeriodArtifactViewSizeChanged(object sender, EventArgs e)
    {
        backgroundDrawable.TimelineMargin = TimelineMargin;
        backgroundDrawable.TimelineRect = TimelineRect;
        WonderLayerService?.UpdateWondersPosition(backgroundDrawable.WonderLayers, backgroundDrawable.MinYear, backgroundDrawable.MaxYear, TimelineRect.Width, 0, backgroundDrawable.MinWonderWidth);

        backgroundGraphicsView.Invalidate();

        smallYearsStackLayout.Opacity = isSmall ? SmallLabelCollapsedOpacity : SmallLabelExpandedOpacity;
    }

    private void ViewModelPropertyChanged(object sender, PropertyChangedEventArgs e)
    {
        if (e.PropertyName == nameof(IArtifactsPageViewModel.FromYear) ||
            e.PropertyName == nameof(IArtifactsPageViewModel.ToYear))
        {
            backgroundDrawable.FromYear = viewModel.FromYear;
            backgroundDrawable.ToYear = viewModel.ToYear;
        }
        else
        {
            backgroundDrawable.SearchesYears = viewModel.AllSearches?.Select(s => s.Year);
            backgroundDrawable.MinYear = viewModel.MinYear;
            backgroundDrawable.MaxYear = viewModel.MaxYear;
            if (viewModel.CurrentWonder is not null)
                backgroundDrawable.SelectedWonder = viewModel.CurrentWonder.Type;

            WonderLayerService?.UpdateWonders(viewModel.Wonders, backgroundDrawable.WonderLayers);
            WonderLayerService?.UpdateWondersPosition(backgroundDrawable.WonderLayers, viewModel.MinYear, viewModel.MaxYear, TimelineRect.Width, 0, backgroundDrawable.MinWonderWidth);
        }

        backgroundGraphicsView.Invalidate();
    }

    private void SmallTapped(object sender, TappedEventArgs e)
    {
        Expand();
    }


    class PeriodArtifactBackroundDrawable : IDrawable
    {
        private const float CornerRadius = 8f;
        private const float TimelineCornerRadius = 6f;
        private const float BackgroundOpacity = 0.95f;
        private const float SearchOpacity = 0.25f;
        private const float SearchWidth = 2f;

        private double ExpandProgress => 1 - CollapseProgress;

        public float MinWonderWidth => (TimelineRect.Height / WonderLayers.Count) * 0.6f;
        public Thickness TimelineMargin { get; set; }
        public RectF TimelineRect { get; set; }
        public RectF CollapsedRect { get; set; }
        public double CollapseProgress { get; set; } = 0;
        public Color BackgroundColor { get; set; }
        public Color TimelineColor { get; set; }
        public Color SearchColor { get; set; }
        public int MinYear { get; set; }
        public int MaxYear { get; set; }
        public int FromYear { get; set; }
        public int ToYear { get; set; }
        public IEnumerable<int> SearchesYears { get; set; }
        public Marvelous.Core.Models.WonderType SelectedWonder { get; set; }
        public Dictionary<int, List<LayerWonder>> WonderLayers { get; private set; } = new Dictionary<int, List<LayerWonder>>();


        public void Draw(ICanvas canvas, RectF dirtyRect)
        {
            canvas.SaveState();

            var backgroundBottomCornerRadius = Lerp(0, CornerRadius, Math.Min(1, CollapseProgress * 4));
            var backgroundRect = GetBackgroundRect(dirtyRect);

            canvas.ClipRectangle(backgroundRect);

            canvas.SetFillPaint(new SolidPaint(ColorWithOpacity(BackgroundColor, BackgroundOpacity)), dirtyRect);
            canvas.FillRoundedRectangle(backgroundRect, CornerRadius, CornerRadius, backgroundBottomCornerRadius, backgroundBottomCornerRadius);

            RectF timelineRect = new Rect(TimelineMargin.Left, TimelineMargin.Top, dirtyRect.Width - TimelineMargin.HorizontalThickness, dirtyRect.Height - TimelineMargin.VerticalThickness);
            canvas.SetFillPaint(new SolidPaint(ColorWithOpacity(TimelineColor, ExpandProgress)), timelineRect);
            canvas.FillRoundedRectangle(timelineRect, TimelineCornerRadius);

            DrawSearches(canvas);

            canvas.DrawGlobalTimeline(
                TimelineRect,
                WonderLayers,
                MinWonderWidth,
                2f,
                SelectedWonder,
                0,
                Colors.Transparent,
                new SolidPaint(ColorWithOpacity(Colors.White, 0.8 * ExpandProgress)),
                new SolidPaint(ColorWithOpacity(Colors.White, 0.45 * ExpandProgress)));

            DrawSelectionBox(canvas, dirtyRect);

            canvas.RestoreState();
        }

        private void DrawSelectionBox(ICanvas canvas, RectF dirtyRect)
        {
            var color = ColorWithOpacity(Colors.White, 0.85 * ExpandProgress);
            var arrowWidth = ArrowThumbWidth * 0.22f;
            var arrowLeftOffset = (ArrowThumbWidth - arrowWidth) / 2f;
            var arrowTopOffset = (TimelineRect.Height / 2f) - arrowWidth;
            var strokeWidth = 2f;
            var halfStrokeWidth = strokeWidth / 2f;
            var yearWidth = MaxYear - MinYear;
            var relativeLeft = (FromYear - MinYear) / (float)yearWidth;
            var relativeRight = (ToYear - MinYear) / (float)yearWidth;
            var left = (TimelineRect.Width * relativeLeft) + TimelineRect.Left;
            var right = (TimelineRect.Width * relativeRight) + TimelineRect.Left;
            var rect = new RectF(left + halfStrokeWidth, TimelineRect.Top + halfStrokeWidth, right - left - strokeWidth, TimelineRect.Height - strokeWidth);
            var leftThumbRect = new RectF(left - ArrowThumbWidth, TimelineRect.Top, ArrowThumbWidth, TimelineRect.Height);
            var rightThumbRect = new RectF(right, TimelineRect.Top, ArrowThumbWidth, TimelineRect.Height);

            canvas.StrokeColor = color;
            canvas.StrokeSize = strokeWidth;
            canvas.SetFillPaint(new SolidPaint(color), dirtyRect);
            canvas.DrawRectangle(rect);
            canvas.FillRoundedRectangle(leftThumbRect, TimelineCornerRadius, 0, TimelineCornerRadius, 0);
            canvas.FillRoundedRectangle(rightThumbRect, 0, TimelineCornerRadius, 0, TimelineCornerRadius);

            DrawArrows(canvas, arrowWidth, arrowLeftOffset, arrowTopOffset, leftThumbRect, rightThumbRect);
        }

        private void DrawArrows(ICanvas canvas, float arrowWidth, float arrowLeftOffset, float arrowTopOffset, RectF leftThumbRect, RectF rightThumbRect)
        {
            canvas.StrokeSize = 1.5f;
            canvas.StrokeColor = ColorWithOpacity(TimelineColor, ExpandProgress);
            canvas.StrokeLineCap = LineCap.Round;
            canvas.StrokeLineJoin = LineJoin.Round;

            var leftPath = new PathF();
            var rightPath = new PathF();

            leftPath
                .MoveTo(arrowLeftOffset + arrowWidth + leftThumbRect.Left, arrowTopOffset + leftThumbRect.Top)
                .LineTo(arrowLeftOffset + leftThumbRect.Left, arrowTopOffset + arrowWidth + leftThumbRect.Top)
                .LineTo(arrowLeftOffset + arrowWidth + leftThumbRect.Left, arrowTopOffset + (arrowWidth * 2) + leftThumbRect.Top);

            rightPath
                .MoveTo(arrowLeftOffset + rightThumbRect.Left, arrowTopOffset + rightThumbRect.Top)
                .LineTo(arrowLeftOffset + arrowWidth + rightThumbRect.Left, arrowTopOffset + arrowWidth + rightThumbRect.Top)
                .LineTo(arrowLeftOffset + rightThumbRect.Left, arrowTopOffset + (arrowWidth * 2) + rightThumbRect.Top);

            canvas.DrawPath(leftPath);
            canvas.DrawPath(rightPath);

            return;
        }

        private void DrawSearches(ICanvas canvas)
        {
            if (SearchesYears is null)
                return;

            var paint = new SolidPaint(ColorWithOpacity(SearchColor, SearchOpacity * ExpandProgress));
            var yearWidth = MaxYear - MinYear;

            foreach (var year in SearchesYears)
            {
                var relativePosition = (year - MinYear) / (float)yearWidth;
                var left = (TimelineRect.Width * relativePosition) - (SearchWidth / 2f) + TimelineRect.Left;
                var rect = new RectF(left, TimelineRect.Top, SearchWidth, TimelineRect.Height);

                canvas.SetFillPaint(paint, rect);
                canvas.FillRectangle(rect);
            }
        }

        private RectF GetBackgroundRect(RectF dirtyRect)
        {
            Rect backgroundRect = new Rect();

            backgroundRect.Left = Lerp(dirtyRect.Left, CollapsedRect.Left, CollapseProgress);
            backgroundRect.Right = Lerp(dirtyRect.Right, CollapsedRect.Right, CollapseProgress);
            backgroundRect.Top = Lerp(dirtyRect.Top, CollapsedRect.Top, CollapseProgress);
            backgroundRect.Bottom = Lerp(dirtyRect.Bottom, CollapsedRect.Bottom, CollapseProgress);

            return backgroundRect;
        }

        private Color ColorWithOpacity(Color color, double opacity)
        {
            return Color.FromRgba(color.Red, color.Green, color.Blue, opacity);
        }
    }
}