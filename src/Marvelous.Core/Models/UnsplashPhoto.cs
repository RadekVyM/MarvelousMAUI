namespace Marvelous.Core.Models
{
    public enum UnsplashPhotoSize { Med, Large, Xl }

    public class UnsplashPhoto
    {
        public string Id { get; init; }
        public string Url { get; init; }

        public string GetUnsplashUrl(int size) => $"{Url}?q=90&fm=jpg&w={size}&fit=max";

        public static string GetSelfHostedUrl(string id, UnsplashPhotoSize targetSize, double pixelDensity)
        {
            var size = targetSize switch
            {
                UnsplashPhotoSize.Med => 400,
                UnsplashPhotoSize.Large => 800,
                UnsplashPhotoSize.Xl => 1200,
                _ => 400
            };

            if (pixelDensity >= 1.5)
                size *= 2;

            return $"https://wonderous.info/unsplash/{id}-{size}.jpg";
        }
    }
}
