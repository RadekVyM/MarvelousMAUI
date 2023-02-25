using Marvelous.Core.Interfaces.Repositories;
using Marvelous.Core.Interfaces.Services;
using Marvelous.Core.Models;

namespace Marvelous.Core.Services
{
    public class ArtifactService : IArtifactService
    {
        private const string DefaultString = "--";

        private readonly IWonderRepository wonderRepository;
        private readonly IHighlightRepository highlightRepository;
        private readonly ISearchRepository searchRepository;


        public ArtifactService(IWonderRepository wonderRepository, IHighlightRepository highlightRepository, ISearchRepository searchRepository)
        {
            this.wonderRepository = wonderRepository;
            this.highlightRepository = highlightRepository;
            this.searchRepository = searchRepository;
        }


        public Artifact GetArtifact(WonderType wonderType, string artifactId)
        {
            var wonder = wonderRepository.GetWonder(wonderType);
            return GetArtifact(wonder, artifactId);
        }

        public Artifact GetArtifact(Wonder wonder, string artifactId)
        {
            var search = searchRepository.GetSearch(wonder.Type, artifactId);
            var highlight = highlightRepository.GetHighlight(artifactId);

            var splitted = search?.Keywords
                .Split('|')
                .Select(s => s.Length >= 2 ? string.Concat(char.ToUpper(s[0]), s[1..]) : s)
                .ToList() ?? new List<string> { DefaultString, DefaultString, DefaultString };

            return new Artifact
            {
                Classification = splitted.Count >= 3 ? (string.IsNullOrWhiteSpace(splitted[2]) ? DefaultString : splitted[2]) : DefaultString,
                Country = wonder.RegionTitle,
                Culture = string.IsNullOrWhiteSpace(highlight?.Culture) ? wonder.RegionTitle : highlight.Culture,
                Date = string.IsNullOrWhiteSpace(highlight?.Date) ? DefaultString : highlight.Date,
                Dimension = DefaultString,
                Image = string.IsNullOrWhiteSpace(highlight?.ImageUrl) ? search?.ImageUrl : highlight.ImageUrl,
                ImageSmall = string.IsNullOrWhiteSpace(highlight?.ImageUrlSmall) ? search?.ImageUrl : highlight.ImageUrlSmall,
                ObjectBeginYear = wonder.ArtifactStartYr,
                ObjectEndYear = wonder.ArtifactEndYr,
                ObjectId = artifactId,
                ObjectType = DefaultString,
                Period = DefaultString,
                Title = string.IsNullOrWhiteSpace(highlight?.Title) ? search?.Title : highlight.Title,
                Medium = splitted.Count >= 2 ? (string.IsNullOrWhiteSpace(splitted[1]) ? DefaultString : splitted[1]) : DefaultString
            };
        }

        public IList<Artifact> GetArtifactsForWonder(WonderType wonderType, int fromYear = int.MinValue, int toYear = int.MaxValue)
        {
            var wonder = wonderRepository.GetWonder(wonderType);
            return GetArtifacts(wonder, wonder.HighlightArtifacts, fromYear, toYear);
        }

        public IList<Artifact> GetHiddenArtifactsForWonder(WonderType wonderType, int fromYear = int.MinValue, int toYear = int.MaxValue)
        {
            var wonder = wonderRepository.GetWonder(wonderType);
            return GetArtifacts(wonder, wonder.HiddenArtifacts, fromYear, toYear);
        }

        private IList<Artifact> GetArtifacts(Wonder wonder, IEnumerable<string> artifactIds, int fromYear = int.MinValue, int toYear = int.MaxValue)
        {
            var list = new List<Artifact>();

            foreach (var artifactId in artifactIds)
            {
                var artifact = GetArtifact(wonder, artifactId);
                if ((artifact.ObjectBeginYear >= fromYear && artifact.ObjectBeginYear <= toYear) ||
                    (artifact.ObjectEndYear <= toYear && artifact.ObjectEndYear >= fromYear))
                {
                    list.Add(artifact);
                }
            }

            return list;
        }
    }
}
