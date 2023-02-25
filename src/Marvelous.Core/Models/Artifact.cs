namespace Marvelous.Core.Models
{
    public class Artifact
    {
        public string ObjectId { get; init; } // Artifact ID, used to identify through MET server calls.
        public string Title { get; init; } // Artifact title / name
        public string Image { get; init; } // Artifact primary image URL (can have multiple)
        public string ImageSmall { get; init; } // Artifact primary image URL (can have multiple)
        public int ObjectBeginYear { get; init; } // Artifact creation year start.
        public int ObjectEndYear { get; init; } // Artifact creation year end.
        public string ObjectType { get; init; } // Type of thing (coin, basic, cup etc)

        public string Date { get; init; } // Date of creation
        public string Period { get; init; } // Time period of creation
        public string Country { get; init; } // Country of origin
        public string Medium { get; init; } // Art medium
        public string Dimension { get; init; } // Width and height of physical artifact
        public string Classification { get; init; } // Type of artifact
        public string Culture { get; init; } // Culture of artifact
    }
}
