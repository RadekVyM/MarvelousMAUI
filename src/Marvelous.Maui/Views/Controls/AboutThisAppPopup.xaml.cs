using CommunityToolkit.Maui.Views;
using Microsoft.Maui.ApplicationModel;

namespace Marvelous.Maui.Views.Controls;

public partial class AboutThisAppPopup : Popup
{
	public AboutThisAppPopup(double width)
	{
        InitializeComponent();

        rootGrid.WidthRequest = width;
        versionLabel.Text = AppInfo.Current.VersionString;
    }

    private void CloseButtonClicked(object sender, EventArgs e)
    {
        Close();
    }

    private void GitHubTapped(object sender, TappedEventArgs e)
    {
        Browser.Default.OpenAsync("https://github.com/RadekVyM/maui-wonderous-app");
    }

    private void MuseumTapped(object sender, TappedEventArgs e)
    {
        Browser.Default.OpenAsync("https://www.metmuseum.org/");
    }

    private void UnsplashTapped(object sender, TappedEventArgs e)
    {
        Browser.Default.OpenAsync("https://unsplash.com/@gskinner/collections");
    }

    private void MauiTapped(object sender, TappedEventArgs e)
    {
        Browser.Default.OpenAsync("https://dotnet.microsoft.com/apps/maui");
    }

    private void WonderousSiteTapped(object sender, TappedEventArgs e)
    {
        Browser.Default.OpenAsync("https://wonderous.app");
    }

    private void GskinnerTapped(object sender, TappedEventArgs e)
    {
        Browser.Default.OpenAsync("https://gskinner.com/flutter");
    }
}
