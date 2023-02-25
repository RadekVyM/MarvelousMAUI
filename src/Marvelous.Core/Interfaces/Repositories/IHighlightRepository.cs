using Marvelous.Core.Models;

namespace Marvelous.Core.Interfaces.Repositories
{
    public interface IHighlightRepository
    {
        Highlight GetHighlight(string artifactId);
        IList<Highlight> GetHighlights();
    }
}
