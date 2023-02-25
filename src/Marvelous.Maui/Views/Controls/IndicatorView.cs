namespace Marvelous.Maui.Views.Controls
{
    public class IndicatorView : GraphicsView
    {
        private const int AnimationLength = 200;

        private readonly IndicatorDrawable drawable;

        public static readonly BindableProperty CountProperty =
            BindableProperty.Create(nameof(Count), typeof(int), typeof(IndicatorView), 0, BindingMode.OneWay, propertyChanged: OnCountPropertyChanged);

        public static readonly BindableProperty CurrentIndexProperty =
            BindableProperty.Create(nameof(CurrentIndex), typeof(int), typeof(IndicatorView), 0, BindingMode.OneWay, propertyChanged: OnCurrentIndexPropertyChanged);

        public static readonly BindableProperty ColorProperty =
            BindableProperty.Create(nameof(Color), typeof(Color), typeof(IndicatorView), Colors.Black, BindingMode.OneWay, propertyChanged: OnColorPropertyChanged);

        public int Count
        {
            get => (int)GetValue(CountProperty);
            set => SetValue(CountProperty, value);
        }

        public int CurrentIndex
        {
            get => (int)GetValue(CurrentIndexProperty);
            set => SetValue(CurrentIndexProperty, value);
        }

        public Color Color
        {
            get => (Color)GetValue(ColorProperty);
            set => SetValue(ColorProperty, value);
        }


        public IndicatorView()
        {
            Background = Colors.Transparent;
            Drawable = drawable = new IndicatorDrawable();
        }

        private void MoveTo(int newIndex)
        {
            if (drawable.SelectedIndex == newIndex)
                return;

            drawable.OldSelectedIndex = drawable.SelectedIndex;
            drawable.SelectedIndex = newIndex;

            var animation = new Animation(v =>
            {
                drawable.AnimationProgress = v;
                Invalidate();
            });

            animation.Commit(this, "Animation", length: AnimationLength, finished: (d, b) =>
            {
                drawable.AnimationProgress = 1;
                Invalidate();
            });
        }

        private static void OnCountPropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var indicatorView = bindable as IndicatorView;
            indicatorView.drawable.Count = (int)newValue;
            indicatorView.Invalidate();
        }

        private static void OnCurrentIndexPropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var indicatorView = bindable as IndicatorView;
            indicatorView.MoveTo((int)newValue);
            indicatorView.Invalidate();
        }

        private static void OnColorPropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var indicatorView = bindable as IndicatorView;
            indicatorView.drawable.Color = newValue as Color;
            indicatorView.Invalidate();
        }

        private class IndicatorDrawable : IDrawable
        {
            public Color Color { get; set; }
            public int Count { get; set; }
            public int SelectedIndex { get; set; } = 0;
            public int OldSelectedIndex { get; set; } = 0;
            public double AnimationProgress { get; set; } = 1;


            public void Draw(ICanvas canvas, RectF dirtyRect)
            {
                canvas.SaveState();

                float defaultWidth = dirtyRect.Height;
                float selectedWidth = defaultWidth * 2;
                float spacing = defaultWidth;
                float left = (dirtyRect.Width - ((defaultWidth * (Count - 1)) + selectedWidth + ((Count - 1) * spacing))) / 2;

                for (int i = 0; i < Count; i++)
                {
                    float width;

                    if (i == SelectedIndex)
                        width = defaultWidth + ((selectedWidth - defaultWidth) * (float)AnimationProgress);
                    else if (i == OldSelectedIndex)
                        width = selectedWidth - ((selectedWidth - defaultWidth) * (float)AnimationProgress);
                    else
                        width = defaultWidth;

                    var rect = new RectF(left, 0, width, dirtyRect.Height);

                    canvas.SetFillPaint(new SolidPaint(Color), rect);

                    canvas.FillRoundedRectangle(rect, dirtyRect.Height / 2);

                    left += width + spacing;
                }

                canvas.RestoreState();
            }
        }
    }
}
