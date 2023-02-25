using Marvelous.Maui.Blazor;

namespace Marvelous.Maui.Views.Controls;

public partial class PhotoGrid : ContentView
{
	public PhotoGrid()
	{
		InitializeComponent();
    }

    protected override void OnBindingContextChanged()
    {
        blazorWebView.RootComponents.FirstOrDefault().Parameters = new Dictionary<string, object>()
        {
            [nameof(PhotoGallery.ViewModel)] = BindingContext
        };
    }
}
