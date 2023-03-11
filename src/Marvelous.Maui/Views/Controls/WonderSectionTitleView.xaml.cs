using Marvelous.Maui.Models;
using Font = Microsoft.Maui.Graphics.Font;

namespace Marvelous.Maui.Views.Controls;

public partial class WonderSectionTitleView : ContentView
{
    private const string TitleAnimationKey = "TitleAnimation";
    private const string IconAnimationKey = "IconAnimation";
    private const uint TitleAnimationLength = 600;
    private const uint IconAnimationLength = 600;

    private readonly WonderSectionTitleViewDrawable drawable;

    public static readonly BindableProperty WonderSectionProperty =
        BindableProperty.Create(nameof(WonderSection), typeof(WonderSectionViewModel), typeof(WonderSectionTitleView), propertyChanged: OnWonderSectionChanged);
    public static readonly BindableProperty TitleSwitchDirectionProperty =
        BindableProperty.Create(nameof(TitleSwitchDirection), typeof(bool), typeof(WonderSectionTitleView), propertyChanged: OnTitleSwitchDirectionChanged);

    public virtual WonderSectionViewModel WonderSection
    {
        get => (WonderSectionViewModel)GetValue(WonderSectionProperty);
        set => SetValue(WonderSectionProperty, value);
    }

    public virtual bool TitleSwitchDirection
    {
        get => (bool)GetValue(TitleSwitchDirectionProperty);
        set => SetValue(TitleSwitchDirectionProperty, value);
    }


    public WonderSectionTitleView()
	{
		InitializeComponent();

        App.Current.Resources.TryGetValue("TertiaryColor", out object backgroundColor);
        App.Current.Resources.TryGetValue("PrimaryColor", out object primaryColor);

        graphicsView.Drawable = drawable = new WonderSectionTitleViewDrawable
        {
            Background = backgroundColor as Color,
            TextColor = primaryColor as Color
        };
    }


    private void UpdateTitle()
    {
        this.AbortAnimation(TitleAnimationKey);

        var animation = new Animation(d =>
        {
            drawable.Progress = d;
            graphicsView.Invalidate();
        });

        drawable.OldText = drawable.Text;
        drawable.Text = WonderSection?.Title ?? "";

        animation.Commit(this, TitleAnimationKey, length: TitleAnimationLength, easing: Easing.CubicInOut);
    }

    private void UpdateIcon()
    {
        this.AbortAnimation(IconAnimationKey);

        var animation = new Animation();

        animation.Add(0, 0.5, new Animation(d => icon.Scale = d, 1, 0, finished: () => icon.Source = WonderSection.Icon));
        animation.Add(0.5, 1, new Animation(d => icon.Scale = d, 0, 1, Easing.SpringOut));

        animation.Commit(this, IconAnimationKey, length: IconAnimationLength);
    }

    private static void OnWonderSectionChanged(BindableObject bindable, object oldValue, object newValue)
    {
        if (newValue == oldValue)
            return;

        var titleView = bindable as WonderSectionTitleView;
        if (newValue is not WonderSectionViewModel viewModel)
            return;

        titleView.UpdateIcon();
        titleView.UpdateTitle();
    }

    private static void OnTitleSwitchDirectionChanged(BindableObject bindable, object oldValue, object newValue)
    {
        var titleView = bindable as WonderSectionTitleView;

        titleView.drawable.Direction = (bool)newValue;
    }

    private class WonderSectionTitleViewDrawable : IDrawable
    {
        private const float FontSize = 14.2f;
        private const float CharacterWidth = 13.5f;

        public Color Background { get; set; }
        public Color TextColor { get; set; }
        public string Text { get; set; }
        public string OldText { get; set; }
        public double Progress { get; set; } = 1;
        public bool Direction { get; set; }

        public void Draw(ICanvas canvas, RectF dirtyRect)
        {
            canvas.SaveState();

            var backPaint = new SolidPaint(Background);
            var radius = dirtyRect.Height * 0.95f;
            var rectTop = dirtyRect.Height * 0.4f;

            canvas.ClipRectangle(dirtyRect);
            canvas.SetFillPaint(backPaint, dirtyRect);
            canvas.FillRectangle(0, rectTop, dirtyRect.Width, dirtyRect.Height - rectTop);
            canvas.SetFillPaint(backPaint, dirtyRect);
            canvas.FillArc((dirtyRect.Width / 2) - radius, 0, radius * 2, radius * 2, 0, 180, false);

            DrawText(canvas, dirtyRect, radius);

            canvas.RestoreState();
        }

        private void DrawText(ICanvas canvas, RectF dirtyRect, float radius)
        {
            if (Text is null)
                return;
            
            canvas.Font = new Font("B612Mono-Regular.ttf");
            canvas.FontSize = FontSize;
            canvas.FontColor = TextColor;

            var textVerticalOffset = 17f;
            var text = Text.ToUpper();
            var oldText = OldText?.ToUpper() ?? "";
            var left = (dirtyRect.Width - CharacterWidth) / 2f;
            var perimeter = (float)(Math.PI * radius) - textVerticalOffset;
            var characterDegrees = 180f / (perimeter / CharacterWidth);
            var textsAngleOffset = (360f - (characterDegrees * text.Length) - (characterDegrees * oldText.Length)) * 0.5f;
            var progressAngleOffset = (Direction ? -1 : 1) * 180f * (1f - (float)Progress);
            var rotationCenter = new PointF(dirtyRect.Width / 2f, radius);

            canvas.Rotate((-characterDegrees * text.Length * 0.5f) + (characterDegrees / 2f) + progressAngleOffset, rotationCenter.X, rotationCenter.Y);

            foreach (var character in text)
            {
                canvas.DrawString(character.ToString(), left, textVerticalOffset, CharacterWidth, FontSize * 2, HorizontalAlignment.Center, VerticalAlignment.Top, textFlow: TextFlow.OverflowBounds);
                canvas.Rotate(characterDegrees, rotationCenter.X, rotationCenter.Y);
            }

            if (Progress == 1)
                return;

            canvas.Rotate(textsAngleOffset, rotationCenter.X, rotationCenter.Y);

            foreach (var character in oldText)
            {
                canvas.DrawString(character.ToString(), left, textVerticalOffset, CharacterWidth, FontSize * 2, HorizontalAlignment.Center, VerticalAlignment.Top, textFlow: TextFlow.OverflowBounds);
                canvas.Rotate(characterDegrees, rotationCenter.X, rotationCenter.Y);
            }
        }
    }
}