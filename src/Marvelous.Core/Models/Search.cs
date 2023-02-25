namespace Marvelous.Core.Models
{
    public class Search
    {
        const string BaseImagePath = "https://images.metmuseum.org/CRDImages/";

        public int Id { get; init; }
        public int Year { get; init; }
        public string Title { get; init; }
        public string Keywords { get; init; }
        public string ImagePath { get; init; }
        public double AspectRatio { get; init; }

        public string ImageUrl => BaseImagePath + ImagePath;


        public Search(int year, int id, string title, string keywords, string imagePath, double aspectRatio = 0)
        {
            Year = year;
            Id = id;
            Title = title;
            Keywords = keywords;
            ImagePath = imagePath;
            AspectRatio = aspectRatio;
        }


        public string Write()
        {
            return $"SearchData({Year}, {Id}, {Title}, {Keywords}, {ImagePath}, {GetFormatedAspectRatio})";
        }

        private string GetFormatedAspectRatio()
        {
            return AspectRatio == 0 ? "" : AspectRatio.ToString("0.00");
        }
    }
}
