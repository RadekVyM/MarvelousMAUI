namespace Marvelous.Core.Interfaces.Services
{
    public interface IBrowser
    {
        Task GoToAsync(string url);
        Task GoToYouTubeAsync(string videoId);
    }
}
