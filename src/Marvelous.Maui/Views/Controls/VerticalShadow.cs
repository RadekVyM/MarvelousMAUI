namespace Marvelous.Maui.Views.Controls
{
    public class VerticalShadow : GraphicsView
    {
        private VerticalShadowDrawable drawable => Drawable as VerticalShadowDrawable;

        public static readonly BindableProperty ColorProperty =
            BindableProperty.Create(nameof(Color), typeof(Color), typeof(VerticalShadow), defaultValue: null, propertyChanged: OnPropertyChanged);
        public static readonly BindableProperty TransparentColorProperty =
            BindableProperty.Create(nameof(TransparentColor), typeof(Color), typeof(VerticalShadow), defaultValue: null, propertyChanged: OnPropertyChanged);
        public static readonly BindableProperty OffsetProperty =
            BindableProperty.Create(nameof(Offset), typeof(float), typeof(VerticalShadow), defaultValue: 1f, propertyChanged: OnPropertyChanged);
        public static readonly BindableProperty IsFromTopProperty =
            BindableProperty.Create(nameof(IsFromTop), typeof(bool), typeof(VerticalShadow), defaultValue: true, propertyChanged: OnPropertyChanged);

        public virtual Color Color
        {
            get => (Color)GetValue(ColorProperty);
            set => SetValue(ColorProperty, value);
        }

        public virtual Color TransparentColor
        {
            get => (Color)GetValue(TransparentColorProperty);
            set => SetValue(TransparentColorProperty, value);
        }

        public virtual float Offset
        {
            get => (float)GetValue(OffsetProperty);
            set => SetValue(OffsetProperty, value);
        }

        public virtual bool IsFromTop
        {
            get => (bool)GetValue(IsFromTopProperty);
            set => SetValue(IsFromTopProperty, value);
        }


        public VerticalShadow()
        {
            InputTransparent = true;
            Drawable = new VerticalShadowDrawable();
        }


        private static void OnPropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var shadow = bindable as VerticalShadow;

            shadow.drawable.Color = shadow.Color;
            shadow.drawable.TransparentColor = shadow.TransparentColor;
            shadow.drawable.Offset = shadow.Offset;
            shadow.drawable.IsFromTop = shadow.IsFromTop;

            shadow.Invalidate();
        }


        private class VerticalShadowDrawable : IDrawable
        {
            public Color Color { get; set; }
            public Color TransparentColor { get; set; }
            public float Offset { get; set; }
            public bool IsFromTop { get; set; }


            public void Draw(ICanvas canvas, RectF dirtyRect)
            {
                canvas.SaveState();

                var offset = IsFromTop ? -dirtyRect.Height + (dirtyRect.Height * Offset) : dirtyRect.Height * (1f - Offset);

                var paint = new LinearGradientPaint(new[]
                {
                    IsFromTop ? new PaintGradientStop(0f, Color) : new PaintGradientStop(0f, TransparentColor),
                    IsFromTop ? new PaintGradientStop(1f, TransparentColor) : new PaintGradientStop(1f, Color),
                }, new Point(0, 0), new Point(0, 1));

                canvas.SetFillPaint(paint, new RectF(dirtyRect.X, offset, dirtyRect.Width, dirtyRect.Height));
                canvas.FillRectangle(dirtyRect);

                canvas.RestoreState();
            }
        }
    }
}
