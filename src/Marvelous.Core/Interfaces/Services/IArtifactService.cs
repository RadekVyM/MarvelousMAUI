using Marvelous.Core.Models;

namespace Marvelous.Core.Interfaces.Services
{
    public interface IArtifactService
    {
        Artifact GetArtifact(Wonder wonder, string artifactId);
        Artifact GetArtifact(WonderType wonderType, string artifactId);
        IList<Artifact> GetArtifactsForWonder(WonderType wonderType, int fromYear = int.MinValue, int toYear = int.MaxValue);
        IList<Artifact> GetHiddenArtifactsForWonder(WonderType wonderType, int fromYear = int.MinValue, int toYear = int.MaxValue);
    }
}
