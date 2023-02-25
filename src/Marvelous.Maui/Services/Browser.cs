namespace Marvelous.Maui.Services
{
    public class Browser : Marvelous.Core.Interfaces.Services.IBrowser
    {
        public async Task GoToAsync(string url)
        {
            await Microsoft.Maui.ApplicationModel.Browser.OpenAsync(url, BrowserLaunchMode.SystemPreferred);
        }

        public async Task GoToYouTubeAsync(string videoId)
        {
            var launcherUrl = $"vnd.youtube://{videoId}";

            if (await Launcher.CanOpenAsync(launcherUrl))
                await Launcher.OpenAsync(launcherUrl);
            else
                await Microsoft.Maui.ApplicationModel.Browser.OpenAsync($"https://www.youtube.com/watch?v={videoId}", BrowserLaunchMode.SystemPreferred);
        }
    }
}
