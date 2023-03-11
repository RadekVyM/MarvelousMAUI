using Marvelous.Core.Extensions;
using Marvelous.Core.Models;
using Marvelous.Maui.Extensions;
using Marvelous.Maui.Models;
using Marvelous.Maui.Services;

namespace Marvelous.Maui.Views.Controls
{
    public class GlobalTimelineSlider : GraphicsView
    {
        private readonly Dictionary<int, List<LayerWonder>> wonderLayers = new Dictionary<int, List<LayerWonder>>();
        private readonly GlobalTimelineSliderDrawable drawable;

        private double startRelativePosition = 0;
        private int minYear = 0;
        private int maxYear = 0;

        private double wonderSpacing => 3;
        private double wondersHeight => Height * 0.55;
        private double minWonderWidth => (wondersHeight - ((wonderLayers.Count - 1) * wonderSpacing)) / wonderLayers.Count;

        public static readonly BindableProperty CurrentWonderTypeProperty =
            BindableProperty.Create(nameof(CurrentWonderType), typeof(WonderType), typeof(GlobalTimelineSlider), propertyChanged: OnCurrentWonderChanged);
        public static readonly BindableProperty RelativeThumbWidthProperty =
            BindableProperty.Create(nameof(RelativeThumbWidth), typeof(double), typeof(GlobalTimelineSlider), propertyChanged: OnRelativeThumbPropertyChanged);
        public static readonly BindableProperty RelativeThumbPositionProperty =
            BindableProperty.Create(nameof(RelativeThumbPosition), typeof(double), typeof(GlobalTimelineSlider), propertyChanged: OnRelativeThumbPropertyChanged);

        public virtual WonderType CurrentWonderType
        {
            get => (WonderType)GetValue(CurrentWonderTypeProperty);
            set => SetValue(CurrentWonderTypeProperty, value);
        }

        public virtual double RelativeThumbWidth
        {
            get => (double)GetValue(RelativeThumbWidthProperty);
            set => SetValue(RelativeThumbWidthProperty, value);
        }

        public virtual double RelativeThumbPosition
        {
            get => (double)GetValue(RelativeThumbPositionProperty);
            set => SetValue(RelativeThumbPositionProperty, value);
        }

        public bool IsPanning { get; private set; } = false;

        public event Action<double> Scrolled;


        public GlobalTimelineSlider()
        {
            App.Current.Resources.TryGetValue("TertiaryColor", out object color);

            Background = Colors.Transparent;
            Drawable = drawable = new GlobalTimelineSliderDrawable
            {
                WonderLayers = wonderLayers,
                Color = color as Color
            };

            var panRecognizer = new PanGestureRecognizer();
            panRecognizer.PanUpdated += PanRecognizerPanUpdated;
            GestureRecognizers.Add(panRecognizer);

            SizeChanged += GlobalTimelineSliderSizeChanged;
        }


        public void UpdateWonderLayers(IList<Wonder> wonders, IList<TimelineEvent> timelineEvents)
        {
            if (wonders is null)
                return;

            UpdateMinMaxYears(wonders, timelineEvents);

            WonderLayerService.UpdateWonders(wonders, wonderLayers);

            WonderLayerService.UpdateWondersPosition(wonderLayers, minYear, maxYear, Width, 0, minWonderWidth);

            Invalidate();
        }

        private void UpdateMinMaxYears(IList<Wonder> wonders, IList<TimelineEvent> timelineEvents)
        {
            if (timelineEvents is null || !timelineEvents.Any())
            {
                minYear = wonders.MinWonderYear();
                maxYear = wonders.MaxWonderYear();
            }
            else
            {
                minYear = timelineEvents.MinEventYear();
                maxYear = timelineEvents.MaxEventYear();
            }
        }

        private void GlobalTimelineSliderSizeChanged(object sender, EventArgs e)
        {
            WonderLayerService.UpdateWondersPosition(wonderLayers, minYear, maxYear, Width, 0, minWonderWidth);

            drawable.MinWonderHeight = minWonderWidth;
            drawable.WonderSpacing = wonderSpacing;

            Invalidate();
        }

        private async void PanRecognizerPanUpdated(object sender, PanUpdatedEventArgs e)
        {
            switch (e.StatusType)
            {
                case GestureStatus.Started:
                    startRelativePosition = RelativeThumbPosition;
                    IsPanning = true;
                    break;
                case GestureStatus.Running:
                    RelativeThumbPosition = Math.Clamp(startRelativePosition + (e.TotalX / Width), 0, 1);
                    Scrolled?.Invoke(RelativeThumbPosition);
                    break;
                case GestureStatus.Completed:
                case GestureStatus.Canceled:
                    RelativeThumbPosition = Math.Clamp(RelativeThumbPosition, 0, 1);
                    Scrolled?.Invoke(RelativeThumbPosition);

                    // Not good? 🤔
                    await Task.Delay(75);

                    IsPanning = false;
                    break;
            }
        }

        private static void OnCurrentWonderChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var slider = bindable as GlobalTimelineSlider;

            slider.drawable.SelectedWonder = slider.CurrentWonderType;

            slider.Invalidate();
        }

        private static void OnRelativeThumbPropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var slider = bindable as GlobalTimelineSlider;

            slider.drawable.RelativeThumbWidth = slider.RelativeThumbWidth;
            slider.drawable.RelativeThumbPosition = slider.RelativeThumbPosition;

            slider.Invalidate();
        }

        private class GlobalTimelineSliderDrawable : IDrawable
        {
            public Dictionary<int, List<LayerWonder>> WonderLayers { get; set; } = new Dictionary<int, List<LayerWonder>>();
            public double MinWonderHeight { get; set; }
            public double WonderSpacing { get; set; }
            public Color Color { get; set; }
            public WonderType SelectedWonder { get; set; }
            public double RelativeThumbPosition { get; set; }
            public double RelativeThumbWidth { get; set; }

            public void Draw(ICanvas canvas, RectF dirtyRect)
            {
                canvas.SaveState();

                canvas.DrawGlobalTimeline(
                    dirtyRect,
                    WonderLayers,
                    MinWonderHeight,
                    WonderSpacing,
                    SelectedWonder,
                    1,
                    Color,
                    new SolidPaint(Color));

                DrawThumb(canvas, dirtyRect);

                canvas.RestoreState();
            }

            private void DrawThumb(ICanvas canvas, RectF dirtyRect)
            {
                canvas.StrokeColor = Color;
                canvas.StrokeSize = 2;

                var thumbWidth = dirtyRect.Width * RelativeThumbWidth;
                //thumbWidth = (thumbWidth * 0.5f) + (thumbWidth * 0.5f * (RelativeThumbPosition < 0.5 ? RelativeThumbPosition * 2 : (1 - RelativeThumbPosition) * 2));
                var left = Math.Min((dirtyRect.Width * RelativeThumbPosition) - (thumbWidth / 2f), dirtyRect.Width - (thumbWidth / 2f) - 1);
                
                thumbWidth = left < 0 ? thumbWidth + left : Math.Min(thumbWidth, dirtyRect.Width - left);

                RectF rect = new Rect(Math.Max(left, 1), 0, thumbWidth, dirtyRect.Height);

                canvas.StrokeColor = Colors.White;
                canvas.DrawRectangle(rect);

                var lineLeft = dirtyRect.Width * (float)RelativeThumbPosition;

                canvas.StrokeSize = 1;
                canvas.StrokeDashPattern = new float[] { 2f, 2f };

                canvas.StrokeColor = Color;
                canvas.DrawLine(lineLeft, 0, lineLeft, dirtyRect.Height);
            }
        }
    }
}
