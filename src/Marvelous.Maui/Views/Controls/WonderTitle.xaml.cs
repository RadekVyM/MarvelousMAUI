using Marvelous.Core;

namespace Marvelous.Maui.Views.Controls;

public partial class WonderTitle : ContentView
{
    private const string FontFamily = "YesevaOne";
    private const string FillingWordsKey = "fillingWords";
    private const int FillingWordSize = 16;

    public static readonly BindableProperty TitleProperty =
        BindableProperty.Create(nameof(Title), typeof(string), typeof(WonderTitle), propertyChanged: OnTitleChanged);

    public virtual string Title
    {
        get => (string)GetValue(TitleProperty);
        set => SetValue(TitleProperty, value);
    }


    public WonderTitle()
	{
		InitializeComponent();
	}


    private void UpdateTitle(string titleKey)
    {
        var title = Localization.ResourceManager.GetString(titleKey);
        var fillingWords = Localization.ResourceManager.GetString(FillingWordsKey).ToLower();

        if (title is null)
            return;

        illustrationTitle.Margin = Thickness.Zero;

        var formattedString = new FormattedString();
        var lines = title.Split("\n", StringSplitOptions.RemoveEmptyEntries);

        for (int i = 0; i < lines.Length; i++)
        {
            var line = lines[i];
            var words = line.Split(" ", StringSplitOptions.RemoveEmptyEntries);

            for (int j = 0; j < words.Length; j++)
            {
                var word = words[j].Trim();
                var text = j != words.Length - 1 ? $"{word} " : word;

                if (fillingWords.Contains(word.ToLower()))
                {
                    formattedString.Spans.Add(new Span
                    {
                        Text = text.ToLower(),
                        FontSize = FillingWordSize,
                        FontFamily = FontFamily
                    });

#if ANDROID
                    // TODO: On Android, the height of all spans is the same, the biggest one
                    if (words.Length == 1 && i == 0)
                        illustrationTitle.Margin = new Thickness(illustrationTitle.Margin.Left, -40, illustrationTitle.Margin.Right, illustrationTitle.Margin.Bottom);
#endif
                }
                else
                {
                    formattedString.Spans.Add(new Span { Text = text, FontFamily = FontFamily });
                }
            }

            if (i != lines.Length - 1)
                formattedString.Spans.Add(new Span { Text = "\n" });
        }

        illustrationTitle.FormattedText = formattedString;
    }

    private static void OnTitleChanged(BindableObject bindable, object oldValue, object newValue)
    {
        var wonderTitle = bindable as  WonderTitle;
        wonderTitle.UpdateTitle(newValue.ToString());
    }
}