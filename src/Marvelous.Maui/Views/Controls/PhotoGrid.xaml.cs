using Marvelous.Maui.Blazor;
using Microsoft.AspNetCore.Components.WebView.Maui;

namespace Marvelous.Maui.Views.Controls;

public partial class PhotoGrid : BlazorWebView
{
	public PhotoGrid()
	{
		InitializeComponent();
    }

    protected override void OnBindingContextChanged()
    {
        RootComponents.FirstOrDefault().Parameters = new Dictionary<string, object>()
        {
            [nameof(PhotoGallery.ViewModel)] = BindingContext
        };
    }
}