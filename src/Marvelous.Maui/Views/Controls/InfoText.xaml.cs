namespace Marvelous.Maui.Views.Controls;

public partial class InfoText : ContentView
{
    public static readonly BindableProperty TextProperty = BindableProperty.Create(nameof(Text), typeof(string), typeof(InfoText), propertyChanged: OnTextChanged);

    public virtual string Text
    {
        get => (string)GetValue(TextProperty);
        set => SetValue(TextProperty, value);
    }


    public InfoText()
	{
		InitializeComponent();
	}


    private static void OnTextChanged(BindableObject bindable, object oldValue, object newValue)
    {
        var view = bindable as InfoText;
        view.label.Text = newValue?.ToString();
    }
}