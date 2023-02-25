namespace Marvelous.Maui.Views.Controls
{
    public class VerticalPanIndicatorView : GraphicsView
    {
        private readonly VerticalPanIndicatorDrawable drawable;
       
        public static readonly BindableProperty ProgressProperty =
           BindableProperty.Create(nameof(Progress), typeof(double), typeof(VerticalPanIndicatorView), 0d, BindingMode.OneWay, propertyChanged: OnProgressPropertyChanged);

        public static readonly BindableProperty ColorProperty =
            BindableProperty.Create(nameof(Color), typeof(Color), typeof(VerticalPanIndicatorView), null, BindingMode.OneWay, propertyChanged: OnColorPropertyChanged);

        public double Progress
        {
            get => (double)GetValue(ProgressProperty);
            set => SetValue(ProgressProperty, value);
        }

        public Color Color
        {
            get => (Color)GetValue(ColorProperty);
            set => SetValue(ColorProperty, value);
        }


        public VerticalPanIndicatorView()
        {
            Background = Colors.Transparent;
            Drawable = drawable = new VerticalPanIndicatorDrawable();
        }


        public void AnimateIn()
        {
            this.AbortAnimation("AnimateIn");

            var animation = new Animation();

            animation.Add(0, 0.3, new Animation(d =>
            {
                drawable.ArrowOpacity = (float)d;
                Invalidate();
            }, 1, 0));

            animation.Add(0.3, 1, new Animation(d =>
            {
                drawable.ArrowOpacity = (float)d;
                drawable.ArrowRelativeY = 0.6f + (0.4f * (float)d);
                Invalidate();
            }, 0, 1));

            animation.Commit(this, "AnimateIn", length: 500);
        }

        private static void OnProgressPropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var view = bindable as VerticalPanIndicatorView;
            view.drawable.Progress = (double)newValue;
            view.Invalidate();
        }

        private static void OnColorPropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var view = bindable as VerticalPanIndicatorView;
            view.drawable.Color = newValue as Color;
            view.Invalidate();
        }

        private class VerticalPanIndicatorDrawable : IDrawable
        {
            public Color Color { get; set; }
            public double Progress { get; set; }
            public float ArrowOpacity { get; set; } = 1;
            public float ArrowRelativeY { get; set; } = 1;

            public void Draw(ICanvas canvas, RectF dirtyRect)
            {
                canvas.SaveState();

                var backgroundPath = CreateBackgroundPath(dirtyRect);
                var gradient = new LinearGradientBrush(new GradientStopCollection
                {
                    new GradientStop(Color.FromRgba(Color.Red, Color.Green, Color.Blue, 0), 0),
                    new GradientStop(Color.FromRgba(Color.Red, Color.Green, Color.Blue, 0.8), 1),
                }, new Point(0, 0), new Point(0, 1.1));

                canvas.SetFillPaint(gradient, new RectF(dirtyRect.Left, dirtyRect.Top + dirtyRect.Height - (float)(dirtyRect.Height * Math.Min(Progress, 1)), dirtyRect.Width, dirtyRect.Height));
                canvas.FillPath(backgroundPath);

                var arrowWidth = dirtyRect.Width * 0.35f;
                var arrowPath = CreateArrowPath(arrowWidth);
                arrowPath.Move((dirtyRect.Width - arrowWidth) / 2, (dirtyRect.Height - (arrowWidth / 2) - ((dirtyRect.Width - arrowWidth) / 2)) * ArrowRelativeY);

                canvas.StrokeColor = Color.FromRgba(Color.Red, Color.Green, Color.Blue, ArrowOpacity);
                canvas.StrokeSize = 3.2f;

                canvas.DrawPath(arrowPath);

                canvas.RestoreState();
            }

            private PathF CreateArrowPath(float arrowWidth)
            {
                return new PathF()
                    .MoveTo(0, 0)
                    .LineTo(arrowWidth / 2, arrowWidth / 2)
                    .LineTo(arrowWidth, 0);
            }

            private PathF CreateBackgroundPath(RectF dirtyRect)
            {
                var path = new PathF()
                    .MoveTo(0, 0)
                    .LineTo(dirtyRect.Width, 0)
                    .LineTo(dirtyRect.Width, dirtyRect.Height - dirtyRect.Width)
                    .AddArc(0, dirtyRect.Height - dirtyRect.Width, dirtyRect.Width, dirtyRect.Height, 0, 180, true);
                path.Close();

                return path;
            }
        }
    }
}
