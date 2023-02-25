using Marvelous.Core.Interfaces.Repositories;
using Marvelous.Core.Models;

namespace Marvelous.Data.Repositories
{
    public class HighlightRepository : IHighlightRepository
    {
        // Note: look up a human readable page with:
        // https://www.metmuseum.org/art/collection/search/503940
        // where 503940 is the ID.
        private static readonly List<Highlight> highlights = new List<Highlight>
        {
            // ChichenItza
            new Highlight {
                Title = "Double Whistle",
                Wonder = WonderType.ChichenItza,
                ArtifactId = "503940",
                Culture = "Mayan",
                ImageUrlSmall =
                    "https://images.metmuseum.org/CRDImages/mi/web-large/DT4624a.jpg",
                ImageUrl =
                    "https://images.metmuseum.org/CRDImages/mi/original/DT4624a.jpg",
                Date = "7th–9th century"
            },
            new Highlight {
                Title = "Seated Female Figure",
                Wonder = WonderType.ChichenItza,
                ArtifactId = "312595",
                Culture = "Maya",
                ImageUrlSmall =
                    "https://images.metmuseum.org/CRDImages/ao/web-large/DP-12659-001.jpg",
                ImageUrl =
                    "https://images.metmuseum.org/CRDImages/ao/original/DP-12659-001.jpg",
                Date = "6th–9th century"
            },
            new Highlight {
                Title = "Censer Support",
                Wonder = WonderType.ChichenItza,
                ArtifactId = "310551",
                Culture = "Maya",
                ImageUrlSmall =
                    "https://images.metmuseum.org/CRDImages/ao/web-large/DP102949.jpg",
                ImageUrl =
                    "https://images.metmuseum.org/CRDImages/ao/original/DP102949.jpg",
                Date = "mid-7th–9th century"
            },
            new Highlight {
                Title = "Tripod Plate",
                Wonder = WonderType.ChichenItza,
                ArtifactId = "316304",
                Culture = "Maya",
                ImageUrlSmall =
                    "https://images.metmuseum.org/CRDImages/ao/web-large/DP219258.jpg",
                ImageUrl =
                    "https://images.metmuseum.org/CRDImages/ao/original/DP219258.jpg",
                Date = "9th–10th century"
            },
            new Highlight {
                Title = "Costumed Figure",
                Wonder = WonderType.ChichenItza,
                ArtifactId = "313151",
                Culture = "Maya",
                ImageUrlSmall =
                    "https://images.metmuseum.org/CRDImages/ao/web-large/1979.206.953_a.JPG",
                ImageUrl =
                    "https://images.metmuseum.org/CRDImages/ao/original/1979.206.953_a.JPG",
                Date = "7th–8th century"
            },
            new Highlight {
                Title = "Mirror-Bearer",
                Wonder = WonderType.ChichenItza,
                ArtifactId = "313256",
                Culture = "Maya",
                ImageUrlSmall =
                    "https://images.metmuseum.org/CRDImages/ao/web-large/DP-24340-001.jpg",
                ImageUrl =
                    "https://images.metmuseum.org/CRDImages/ao/original/DP-24340-001.jpg",
                Date = "6th century"
            },
            // ChristRedeemer
            new Highlight {
                Title =
                    "Studio Portrait: Male Street Vendor Holding Box of Flowers, Brazil",
                Wonder = WonderType.ChristRedeemer,
                ArtifactId = "764815",
                Culture = "",
                ImageUrlSmall =
                    "https://images.metmuseum.org/CRDImages/ph/web-large/DP-15801-131.jpg",
                ImageUrl =
                    "https://images.metmuseum.org/CRDImages/ph/original/DP-15801-131.jpg",
                Date = "1864–66"
            },
            new Highlight {
                Title = "Rattle",
                Wonder = WonderType.ChristRedeemer,
                ArtifactId = "502019",
                Culture = "Native American (Brazilian)",
                ImageUrlSmall =
                    "https://images.metmuseum.org/CRDImages/mi/web-large/midp89.4.1453.jpg",
                ImageUrl =
                    "https://images.metmuseum.org/CRDImages/mi/original/midp89.4.1453.jpg",
                Date = "19th century"
            },
            new Highlight {
                Title =
                    "Studio Portrait: Two Males Wearing Hats and Ponchos, Brazil",
                Wonder = WonderType.ChristRedeemer,
                ArtifactId = "764814",
                Culture = "",
                ImageUrlSmall =
                    "https://images.metmuseum.org/CRDImages/ph/web-large/DP-15801-129.jpg",
                ImageUrl =
                    "https://images.metmuseum.org/CRDImages/ph/original/DP-15801-129.jpg",
                Date = "1864–66"
            },
            new Highlight {
                Title =
                    "Studio Portrait: Female Street Vendor Seated Wearing Turban, Brazil",
                Wonder = WonderType.ChristRedeemer,
                ArtifactId = "764816",
                Culture = "",
                ImageUrlSmall =
                    "https://images.metmuseum.org/CRDImages/ph/web-large/DP-15801-133.jpg",
                ImageUrl =
                    "https://images.metmuseum.org/CRDImages/ph/original/DP-15801-133.jpg",
                Date = "1864–66"
            },
            new Highlight {
                Title = "Pluriarc",
                Wonder = WonderType.ChristRedeemer,
                ArtifactId = "501319",
                Culture = "African American (Brazil - Afro-Brazilian)",
                ImageUrlSmall =
                    "https://images.metmuseum.org/CRDImages/mi/web-large/midp89.4.703.jpg",
                ImageUrl =
                    "https://images.metmuseum.org/CRDImages/mi/original/midp89.4.703.jpg",
                Date = "late 19th century"
            },
            // Colosseum
            new Highlight {
                Title = "Marble portrait of a young woman",
                Wonder = WonderType.Colosseum,
                ArtifactId = "251350",
                Culture = "Roman",
                ImageUrlSmall =
                    "https://images.metmuseum.org/CRDImages/gr/web-large/DP331280.jpg",
                ImageUrl =
                    "https://images.metmuseum.org/CRDImages/gr/original/DP331280.jpg",
                Date = "A.D. 150–175"
            },
            new Highlight {
                Title = "Silver mirror",
                Wonder = WonderType.Colosseum,
                ArtifactId = "255960",
                Culture = "Roman",
                ImageUrlSmall =
                    "https://images.metmuseum.org/CRDImages/gr/web-large/DP145605.jpg",
                ImageUrl =
                    "https://images.metmuseum.org/CRDImages/gr/original/DP145605.jpg",
                Date = "4th century A.D."
            },
            new Highlight {
                Title = "Marble portrait of the emperor Augustus",
                Wonder = WonderType.Colosseum,
                ArtifactId = "247993",
                Culture = "Roman",
                ImageUrlSmall =
                    "https://images.metmuseum.org/CRDImages/gr/web-large/DP337220.jpg",
                ImageUrl =
                    "https://images.metmuseum.org/CRDImages/gr/original/DP337220.jpg",
                Date = "ca. A.D. 14–37"
            },
            new Highlight {
                Title = "Terracotta medallion",
                Wonder = WonderType.Colosseum,
                ArtifactId = "250464",
                Culture = "Roman",
                ImageUrlSmall =
                    "https://images.metmuseum.org/CRDImages/gr/web-large/DP105842.jpg",
                ImageUrl =
                    "https://images.metmuseum.org/CRDImages/gr/original/DP105842.jpg",
                Date = "late 2nd–early 3rd century A.D."
            },
            new Highlight {
                Title = "Marble head and torso of Athena",
                Wonder = WonderType.Colosseum,
                ArtifactId = "251476",
                Culture = "Roman",
                ImageUrlSmall =
                    "https://images.metmuseum.org/CRDImages/gr/web-large/DP357289.jpg",
                ImageUrl =
                    "https://images.metmuseum.org/CRDImages/gr/original/DP357289.jpg",
                Date = "1st–2nd century A.D."
            },
            new Highlight {
                Title = "Silver mirror",
                Wonder = WonderType.Colosseum,
                ArtifactId = "255960",
                Culture = "Roman",
                ImageUrlSmall =
                    "https://images.metmuseum.org/CRDImages/gr/web-large/DP145605.jpg",
                ImageUrl =
                    "https://images.metmuseum.org/CRDImages/gr/original/DP145605.jpg",
                Date = "4th century A.D."
            },
            // GreatWall
            new Highlight {
                Title = "Cape",
                Wonder = WonderType.GreatWall,
                ArtifactId = "79091",
                Culture = "French",
                ImageUrlSmall =
                    "https://images.metmuseum.org/CRDImages/ci/web-large/DT2183.jpg",
                ImageUrl =
                    "https://images.metmuseum.org/CRDImages/ci/original/DT2183.jpg",
                Date = "second half 16th century"
            },
            new Highlight {
                Title = "Censer in the form of a mythical beast",
                Wonder = WonderType.GreatWall,
                ArtifactId = "781812",
                Culture = "China",
                ImageUrlSmall =
                    "https://images.metmuseum.org/CRDImages/as/web-large/DP-17100-001.jpg",
                ImageUrl =
                    "https://images.metmuseum.org/CRDImages/as/original/DP-17100-001.jpg",
                Date = "early 17th century"
            },
            new Highlight {
                Title = "Dish with peafowls and peonies",
                Wonder = WonderType.GreatWall,
                ArtifactId = "40213",
                Culture = "China",
                ImageUrlSmall =
                    "https://images.metmuseum.org/CRDImages/as/web-large/DP704217.jpg",
                ImageUrl =
                    "https://images.metmuseum.org/CRDImages/as/original/DP704217.jpg",
                Date = "early 15th century"
            },
            new Highlight {
                Title = "Base for a mandala",
                Wonder = WonderType.GreatWall,
                ArtifactId = "40765",
                Culture = "China",
                ImageUrlSmall =
                    "https://images.metmuseum.org/CRDImages/as/web-large/DP229015.jpg",
                ImageUrl =
                    "https://images.metmuseum.org/CRDImages/as/original/DP229015.jpg",
                Date = "15th century"
            },
            new Highlight {
                Title =
                    "Bodhisattva Manjushri as Tikshna-Manjushri (Minjie Wenshu)",
                Wonder = WonderType.GreatWall,
                ArtifactId = "57612",
                Culture = "China",
                ImageUrlSmall =
                    "https://images.metmuseum.org/CRDImages/as/web-large/DP164061.jpg",
                ImageUrl =
                    "https://images.metmuseum.org/CRDImages/as/original/DP164061.jpg",
                Date = ""
            },
            new Highlight {
                Title = "Tripod incense burner with lid",
                Wonder = WonderType.GreatWall,
                ArtifactId = "666573",
                Culture = "China",
                ImageUrlSmall =
                    "https://images.metmuseum.org/CRDImages/as/web-large/DP356342.jpg",
                ImageUrl =
                    "https://images.metmuseum.org/CRDImages/as/original/DP356342.jpg",
                Date = "early 15th century"
            },
            // MachuPicchu
            new Highlight {
                Title = "Face Beaker",
                Wonder = WonderType.MachuPicchu,
                ArtifactId = "313295",
                Culture = "Inca",
                ImageUrlSmall =
                    "https://images.metmuseum.org/CRDImages/ao/web-large/DT9410.jpg",
                ImageUrl =
                    "https://images.metmuseum.org/CRDImages/ao/original/DT9410.jpg",
                Date = "14th–early 16th century"
            },
            new Highlight {
                Title = "Feathered Bag",
                Wonder = WonderType.MachuPicchu,
                ArtifactId = "316926",
                Culture = "Inca",
                ImageUrlSmall =
                    "https://images.metmuseum.org/CRDImages/ao/web-large/DP158704.jpg",
                ImageUrl =
                    "https://images.metmuseum.org/CRDImages/ao/original/DP158704.jpg",
                Date = "15th–early 16th century"
            },
            new Highlight {
                Title = "Female Figurine",
                Wonder = WonderType.MachuPicchu,
                ArtifactId = "309944",
                Culture = "Inca",
                ImageUrlSmall =
                    "https://images.metmuseum.org/CRDImages/ao/web-large/DP-13440-023.jpg",
                ImageUrl =
                    "https://images.metmuseum.org/CRDImages/ao/original/DP-13440-023.jpg",
                Date = "1400–1533"
            },
            new Highlight {
                Title = "Stirrup Spout Bottle with Felines",
                Wonder = WonderType.MachuPicchu,
                ArtifactId = "309436",
                Culture = "Moche",
                ImageUrlSmall =
                    "https://images.metmuseum.org/CRDImages/ao/web-large/67.92.jpg",
                ImageUrl =
                    "https://images.metmuseum.org/CRDImages/ao/original/67.92.jpg",
                Date = "4th–7th century"
            },
            new Highlight {
                Title = "Camelid figurine",
                Wonder = WonderType.MachuPicchu,
                ArtifactId = "309960",
                Culture = "Inca",
                ImageUrlSmall =
                    "https://images.metmuseum.org/CRDImages/ao/web-large/DP-13440-031.jpg",
                ImageUrl =
                    "https://images.metmuseum.org/CRDImages/ao/original/DP-13440-031.jpg",
                Date = "1400–1533"
            },
            new Highlight {
                Title = "Temple Model",
                Wonder = WonderType.MachuPicchu,
                ArtifactId = "316873",
                Culture = "Aztec",
                ImageUrlSmall =
                    "https://images.metmuseum.org/CRDImages/ao/web-large/DP341942.jpg",
                ImageUrl =
                    "https://images.metmuseum.org/CRDImages/ao/original/DP341942.jpg",
                Date = "1400–1521"
            },
            // Petra
            new Highlight {
                Title = "Unguentarium",
                Wonder = WonderType.Petra,
                ArtifactId = "325900",
                Culture = "Nabataean",
                ImageUrlSmall =
                    "https://images.metmuseum.org/CRDImages/an/web-large/ME67_246_19.jpg",
                ImageUrl =
                    "https://images.metmuseum.org/CRDImages/an/original/ME67_246_19.jpg",
                Date = "ca. 1st century A.D."
            },
            new Highlight {
                Title = "Cooking pot",
                Wonder = WonderType.Petra,
                ArtifactId = "325902",
                Culture = "Nabataean",
                ImageUrlSmall =
                    "https://images.metmuseum.org/CRDImages/an/web-large/ME67_246_21.jpg",
                ImageUrl =
                    "https://images.metmuseum.org/CRDImages/an/original/ME67_246_21.jpg",
                Date = "ca. 1st century A.D."
            },
            new Highlight {
                Title = "Lamp",
                Wonder = WonderType.Petra,
                ArtifactId = "325919",
                Culture = "Nabataean",
                ImageUrlSmall =
                    "https://images.metmuseum.org/CRDImages/an/web-large/ME67_246_38.jpg",
                ImageUrl =
                    "https://images.metmuseum.org/CRDImages/an/original/ME67_246_38.jpg",
                Date = "ca. 1st century A.D."
            },
            new Highlight {
                Title = "Bowl",
                Wonder = WonderType.Petra,
                ArtifactId = "325884",
                Culture = "Nabataean",
                ImageUrlSmall =
                    "https://images.metmuseum.org/CRDImages/an/web-large/ME67_246_3.jpg",
                ImageUrl =
                    "https://images.metmuseum.org/CRDImages/an/original/ME67_246_3.jpg",
                Date = "ca. 1st century A.D."
            },
            new Highlight {
                Title = "Small lamp",
                Wonder = WonderType.Petra,
                ArtifactId = "325887",
                Culture = "Nabataean",
                ImageUrlSmall =
                    "https://images.metmuseum.org/CRDImages/an/web-large/ME67_246_6.jpg",
                ImageUrl =
                    "https://images.metmuseum.org/CRDImages/an/original/ME67_246_6.jpg",
                Date = "ca. 1st century A.D."
            },
            new Highlight {
                Title = "Male figurine",
                Wonder = WonderType.Petra,
                ArtifactId = "325891",
                Culture = "Nabataean",
                ImageUrlSmall =
                    "https://images.metmuseum.org/CRDImages/an/web-large/ME67_246_10.jpg",
                ImageUrl =
                    "https://images.metmuseum.org/CRDImages/an/original/ME67_246_10.jpg",
                Date = "ca. 1st century A.D."
            },
            // PyramidsGiza
            new Highlight {
                Title = "Guardian Figure",
                Wonder = WonderType.PyramidsGiza,
                ArtifactId = "543864",
                Culture = "",
                ImageUrlSmall =
                    "https://images.metmuseum.org/CRDImages/eg/web-large/DP330260.jpg",
                ImageUrl =
                    "https://images.metmuseum.org/CRDImages/eg/original/DP330260.jpg",
                Date = "ca. 1919–1885 B.C."
            },
            new Highlight {
                Title = "Relief fragment",
                Wonder = WonderType.PyramidsGiza,
                ArtifactId = "546488",
                Culture = "",
                ImageUrlSmall =
                    "https://images.metmuseum.org/CRDImages/eg/web-large/LC-34_1_183_EGDP033257.jpg",
                ImageUrl =
                    "https://images.metmuseum.org/CRDImages/eg/original/LC-34_1_183_EGDP033257.jpg",
                Date = "ca. 1981–1640 B.C."
            },
            new Highlight {
                Title = "Ring with Uninscribed Scarab",
                Wonder = WonderType.PyramidsGiza,
                ArtifactId = "557137",
                Culture = "",
                ImageUrlSmall =
                    "https://images.metmuseum.org/CRDImages/eg/web-large/15.3.205_EGDP015425.jpg",
                ImageUrl =
                    "https://images.metmuseum.org/CRDImages/eg/original/15.3.205_EGDP015425.jpg",
                Date = "ca. 1850–1640 B.C."
            },
            new Highlight {
                Title = "Nikare as a scribe",
                Wonder = WonderType.PyramidsGiza,
                ArtifactId = "543900",
                Culture = "",
                ImageUrlSmall =
                    "https://images.metmuseum.org/CRDImages/eg/web-large/DP240451.jpg",
                ImageUrl =
                    "https://images.metmuseum.org/CRDImages/eg/original/DP240451.jpg",
                Date = "ca. 2420–2389 B.C. or later"
            },
            new Highlight {
                Title = "Seated Statue of King Menkaure",
                Wonder = WonderType.PyramidsGiza,
                ArtifactId = "543935",
                Culture = "",
                ImageUrlSmall =
                    "https://images.metmuseum.org/CRDImages/eg/web-large/DP109397.jpg",
                ImageUrl =
                    "https://images.metmuseum.org/CRDImages/eg/original/DP109397.jpg",
                Date = "ca. 2490–2472 B.C."
            },
            new Highlight {
                Title =
                    "Floral collar from Tutankhamun's Embalming Cache",
                Wonder = WonderType.PyramidsGiza,
                ArtifactId = "544782",
                Culture = "",
                ImageUrlSmall =
                    "https://images.metmuseum.org/CRDImages/eg/web-large/DP225343.jpg",
                ImageUrl =
                    "https://images.metmuseum.org/CRDImages/eg/original/DP225343.jpg",
                Date = "ca. 1336–1327 B.C."
            },
            // TajMahal
            new Highlight {
                Title = "Mango-Shaped Flask",
                Wonder = WonderType.TajMahal,
                ArtifactId = "453341",
                Culture = "",
                ImageUrlSmall =
                    "https://images.metmuseum.org/CRDImages/is/web-large/DP240307.jpg",
                ImageUrl =
                    "https://images.metmuseum.org/CRDImages/is/original/DP240307.jpg",
                Date = "mid-17th century"
            },
            new Highlight {
                Title = "Base for a Water Pipe (Huqqa) with Irises",
                Wonder = WonderType.TajMahal,
                ArtifactId = "453243",
                Culture = "",
                ImageUrlSmall =
                    "https://images.metmuseum.org/CRDImages/is/web-large/DP214317.jpg",
                ImageUrl =
                    "https://images.metmuseum.org/CRDImages/is/original/DP214317.jpg",
                Date = "late 17th century"
            },
            new Highlight {
                Title = "Plate",
                Wonder = WonderType.TajMahal,
                ArtifactId = "73309",
                Culture = "India (Gujarat)",
                ImageUrlSmall =
                    "https://images.metmuseum.org/CRDImages/as/web-large/DP138506.jpg",
                ImageUrl =
                    "https://images.metmuseum.org/CRDImages/as/original/DP138506.jpg",
                Date = "mid-16th–17th century"
            },
            new Highlight {
                Title = "Helmet",
                Wonder = WonderType.TajMahal,
                ArtifactId = "24932",
                Culture = "Indian, Mughal",
                ImageUrlSmall =
                    "https://images.metmuseum.org/CRDImages/aa/web-large/1988.147_007mar2015.jpg",
                ImageUrl =
                    "https://images.metmuseum.org/CRDImages/aa/original/1988.147_007mar2015.jpg",
                Date = "18th century"
            },
            new Highlight {
                Title = "Jewelled plate",
                Wonder = WonderType.TajMahal,
                ArtifactId = "56230",
                Culture = "India",
                ImageUrlSmall =
                    "https://images.metmuseum.org/CRDImages/as/web-large/DP-14153-029.jpg",
                ImageUrl =
                    "https://images.metmuseum.org/CRDImages/as/original/DP-14153-029.jpg",
                Date = "18th–19th century"
            },
            new Highlight {
                Title =
                    "Shirt of Mail and Plate of Emperor Shah Jahan (reigned 1624–58)",
                Wonder = WonderType.TajMahal,
                ArtifactId = "35633",
                Culture = "Indian",
                ImageUrlSmall =
                    "https://images.metmuseum.org/CRDImages/aa/web-large/DP219616.jpg",
                ImageUrl =
                    "https://images.metmuseum.org/CRDImages/aa/original/DP219616.jpg",
                Date = "dated A.H. 1042/A.D. 1632–33"
            }
        };

        public IList<Highlight> GetHighlights()
        {
            return highlights;
        }

        public Highlight GetHighlight(string artifactId)
        {
            return highlights.FirstOrDefault(a => a.ArtifactId == artifactId);
        }
    }
}
