using Marvelous.Core.Interfaces.Repositories;
using Marvelous.Core.Models;

namespace Marvelous.Data.Repositories
{
    public class WonderRepository : IWonderRepository
    {
        private static readonly Wonder colosseum = new Wonder
        {
            Type = WonderType.Colosseum,
            Title = "colosseumTitle",
            SubTitle = "colosseumSubTitle",
            RegionTitle = "colosseumRegionTitle",
            VideoId = "GXoEpNjgKzg",
            StartYr = 70,
            EndYr = 80,
            ArtifactStartYr = 0,
            ArtifactEndYr = 500,
            ArtifactCulture = "colosseumArtifactCulture",
            ArtifactGeolocation = "colosseumArtifactGeolocation",
            Lat = 41.890242126393495,
            Lng = 12.492349361871392,
            UnsplashCollectionId = "VPdti8Kjq9o",
            PullQuote1Top = "colosseumPullQuote1Top",
            PullQuote1Bottom = "colosseumPullQuote1Bottom",
            PullQuote1Author = "",
            PullQuote2 = "colosseumPullQuote2",
            PullQuote2Author = "colosseumPullQuote2Author",
            Callout1 = "colosseumCallout1",
            Callout2 = "colosseumCallout2",
            VideoCaption = "colosseumVideoCaption",
            MapCaption = "colosseumMapCaption",
            HistoryInfo1 = "colosseumHistoryInfo1",
            HistoryInfo2 = "colosseumHistoryInfo2",
            ConstructionInfo1 = "colosseumConstructionInfo1",
            ConstructionInfo2 = "colosseumConstructionInfo2",
            LocationInfo1 = "colosseumLocationInfo1",
            LocationInfo2 = "colosseumLocationInfo2",
            HighlightArtifacts = new List<string>
            {
                "251350",
                "255960",
                "247993",
                "250464",
                "251476",
                "255960",
            },
            HiddenArtifacts = new List<string>
            {
                "245376",
                "256570",
                "286136",
            },
            Events = new Dictionary<int, string>
            {
                [70] = "colosseum70ce",
                [82] = "colosseum82ce",
                [1140] = "colosseum1140ce",
                [1490] = "colosseum1490ce",
                [1829] = "colosseum1829ce",
                [1990] = "colosseum1990ce",
            },
        };

        private static readonly Wonder greatWall = new Wonder
        {
            Type = WonderType.GreatWall,
            Title = "greatWallTitle",
            SubTitle = "greatWallSubTitle",
            RegionTitle = "greatWallRegionTitle",
            VideoId = "do1Go22Wu8o",
            StartYr = -700,
            EndYr = 1644,
            ArtifactStartYr = -700,
            ArtifactEndYr = 1650,
            ArtifactCulture = "greatWallArtifactCulture",
            ArtifactGeolocation = "greatWallArtifactGeolocation",
            Lat = 40.43199751120627,
            Lng = 116.57040708482984,
            UnsplashCollectionId = "Kg_h04xvZEo",
            PullQuote1Top = "greatWallPullQuote1Top",
            PullQuote1Bottom = "greatWallPullQuote1Bottom",
            PullQuote1Author = "",
            PullQuote2 = "greatWallPullQuote2",
            PullQuote2Author = "greatWallPullQuote2Author",
            Callout1 = "greatWallCallout1",
            Callout2 = "greatWallCallout2",
            VideoCaption = "greatWallVideoCaption",
            MapCaption = "greatWallMapCaption",
            HistoryInfo1 = "greatWallHistoryInfo1",
            HistoryInfo2 = "greatWallHistoryInfo2",
            ConstructionInfo1 = "greatWallConstructionInfo1",
            ConstructionInfo2 = "greatWallConstructionInfo2",
            LocationInfo1 = "greatWallLocationInfo1",
            LocationInfo2 = "greatWallLocationInfo2",
            HighlightArtifacts = new List<string>
            {
                "79091",
                "781812",
                "40213",
                "40765",
                "57612",
                "666573",
            },
            HiddenArtifacts = new List<string>
            {
                "39918",
                "39666",
                "39735",
            },
            Events = new Dictionary<int, string>
            {
                [-700] = "greatWall700bce",
                [-214] = "greatWall214bce",
                [-121] = "greatWall121bce",
                [556] = "greatWall556ce",
                [618] = "greatWall618ce",
                [1487] = "greatWall1487ce",
            },
        };

        private static readonly Wonder chichenItza = new Wonder
        {
            Type = WonderType.ChichenItza,
            Title = "chichenItzaTitle",
            SubTitle = "chichenItzaSubTitle",
            RegionTitle = "chichenItzaRegionTitle",
            VideoId = "Q6eBJjdca14",
            StartYr = 550,
            EndYr = 1550,
            ArtifactStartYr = 500,
            ArtifactEndYr = 1600,
            ArtifactCulture = "chichenItzaArtifactCulture",
            ArtifactGeolocation = "chichenItzaArtifactGeolocation",
            Lat = 20.68346184201756,
            Lng = -88.56769676930931,
            UnsplashCollectionId = "SUK0tuMnLLw",
            PullQuote1Top = "chichenItzaPullQuote1Top",
            PullQuote1Bottom = "chichenItzaPullQuote1Bottom",
            PullQuote1Author = "",
            PullQuote2 = "chichenItzaPullQuote2",
            PullQuote2Author = "chichenItzaPullQuote2Author",
            Callout1 = "chichenItzaCallout1",
            Callout2 = "chichenItzaCallout2",
            VideoCaption = "chichenItzaVideoCaption",
            MapCaption = "chichenItzaMapCaption",
            HistoryInfo1 = "chichenItzaHistoryInfo1",
            HistoryInfo2 = "chichenItzaHistoryInfo2",
            ConstructionInfo1 = "chichenItzaConstructionInfo1",
            ConstructionInfo2 = "chichenItzaConstructionInfo2",
            LocationInfo1 = "chichenItzaLocationInfo1",
            LocationInfo2 = "chichenItzaLocationInfo2",
            HighlightArtifacts = new List<string>
            {
                "503940",
                "312595",
                "310551",
                "316304",
                "313151",
                "313256",
            },
            HiddenArtifacts = new List<string>
            {
                "701645",
                "310555",
                "286467",
            },
            Events = new Dictionary<int, string>
            {
                [600] = "chichenItza600ce",
                [832] = "chichenItza832ce",
                [998] = "chichenItza998ce",
                [1100] = "chichenItza1100ce",
                [1527] = "chichenItza1527ce",
                [1535] = "chichenItza1535ce",
            },
        };

        private static readonly Wonder christRedeemer = new Wonder
        {
            Type = WonderType.ChristRedeemer,
            Title = "christRedeemerTitle",
            SubTitle = "christRedeemerSubTitle",
            RegionTitle = "christRedeemerRegionTitle",
            VideoId = "k_615AauSds",
            StartYr = 1922,
            EndYr = 1931,
            ArtifactStartYr = 1600,
            ArtifactEndYr = 2100,
            ArtifactCulture = "",
            ArtifactGeolocation = "christRedeemerArtifactGeolocation",
            Lat = -22.95238891944396,
            Lng = -43.21045520611561,
            UnsplashCollectionId = "dPgX5iK8Ufo",
            PullQuote1Top = "christRedeemerPullQuote1Top",
            PullQuote1Bottom = "christRedeemerPullQuote1Bottom",
            PullQuote1Author = "",
            PullQuote2 = "christRedeemerPullQuote2",
            PullQuote2Author = "christRedeemerPullQuote2Author",
            Callout1 = "christRedeemerCallout1",
            Callout2 = "christRedeemerCallout2",
            VideoCaption = "christRedeemerVideoCaption",
            MapCaption = "christRedeemerMapCaption",
            HistoryInfo1 = "christRedeemerHistoryInfo1",
            HistoryInfo2 = "christRedeemerHistoryInfo2",
            ConstructionInfo1 = "christRedeemerConstructionInfo1",
            ConstructionInfo2 = "christRedeemerConstructionInfo2",
            LocationInfo1 = "christRedeemerLocationInfo1",
            LocationInfo2 = "christRedeemerLocationInfo2",
            HighlightArtifacts = new List<string>
            {
                "501319",
                "764815",
                "502019",
                "764814",
                "764816",
            },
            HiddenArtifacts = new List<string>
            {
                "501302",
                "157985",
                "227759",
            },
            Events = new Dictionary<int, string>
            {
                [1850] = "christRedeemer1850ce",
                [1921] = "christRedeemer1921ce",
                [1922] = "christRedeemer1922ce",
                [1926] = "christRedeemer1926ce",
                [1931] = "christRedeemer1931ce",
                [2006] = "christRedeemer2006ce",
            },
        };

        private static readonly Wonder machuPicchu = new Wonder
        {
            Type = WonderType.MachuPicchu,
            Title = "machuPicchuTitle",
            SubTitle = "machuPicchuSubTitle",
            RegionTitle = "machuPicchuRegionTitle",
            VideoId = "cnMa-Sm9H4k",
            StartYr = 1450,
            EndYr = 1572,
            ArtifactStartYr = 1200,
            ArtifactEndYr = 1700,
            ArtifactCulture = "machuPicchuArtifactCulture",
            ArtifactGeolocation = "machuPicchuArtifactGeolocation",
            Lat = -13.162690683637758,
            Lng = -72.54500778824891,
            UnsplashCollectionId = "wUhgZTyUnl8",
            PullQuote1Top = "machuPicchuPullQuote1Top",
            PullQuote1Bottom = "machuPicchuPullQuote1Bottom",
            PullQuote1Author = "machuPicchuPullQuote1Author",
            PullQuote2 = "machuPicchuPullQuote2",
            PullQuote2Author = "machuPicchuPullQuote2Author",
            Callout1 = "machuPicchuCallout1",
            Callout2 = "machuPicchuCallout2",
            VideoCaption = "machuPicchuVideoCaption",
            MapCaption = "machuPicchuMapCaption",
            HistoryInfo1 = "machuPicchuHistoryInfo1",
            HistoryInfo2 = "machuPicchuHistoryInfo2",
            ConstructionInfo1 = "machuPicchuConstructionInfo1",
            ConstructionInfo2 = "machuPicchuConstructionInfo2",
            LocationInfo1 = "machuPicchuLocationInfo1",
            LocationInfo2 = "machuPicchuLocationInfo2",
            HighlightArtifacts = new List<string>
            {
                "313295",
                "316926",
                "309944",
                "309436",
                "309960",
                "316873",
            },
            HiddenArtifacts = new List<string>
            {
                "308120",
                "309960",
                "313341",
            },
            Events = new Dictionary<int, string>
            {
                [1438] = "machuPicchu1438ce",
                [1572] = "machuPicchu1572ce",
                [1867] = "machuPicchu1867ce",
                [1911] = "machuPicchu1911ce",
                [1964] = "machuPicchu1964ce",
                [1997] = "machuPicchu1997ce",
            },
        };

        private static readonly Wonder petra = new Wonder
        {
            Type = WonderType.Petra,
            Title = "petraTitle",
            SubTitle = "petraSubTitle",
            RegionTitle = "petraRegionTitle",
            VideoId = "ezDiSkOU0wc",
            StartYr = -312,
            EndYr = 100,
            ArtifactStartYr = -500,
            ArtifactEndYr = 500,
            ArtifactCulture = "petraArtifactCulture",
            ArtifactGeolocation = "petraArtifactGeolocation",
            Lat = 30.328830750209903,
            Lng = 35.44398203484667,
            UnsplashCollectionId = "qWQJbDvCMW8",
            PullQuote1Top = "petraPullQuote1Top",
            PullQuote1Bottom = "petraPullQuote1Bottom",
            PullQuote1Author = "petraPullQuote1Author",
            PullQuote2 = "petraPullQuote2",
            PullQuote2Author = "petraPullQuote2Author",
            Callout1 = "petraCallout1",
            Callout2 = "petraCallout2",
            VideoCaption = "petraVideoCaption",
            MapCaption = "petraMapCaption",
            HistoryInfo1 = "petraHistoryInfo1",
            HistoryInfo2 = "petraHistoryInfo2",
            ConstructionInfo1 = "petraConstructionInfo1",
            ConstructionInfo2 = "petraConstructionInfo2",
            LocationInfo1 = "petraLocationInfo1",
            LocationInfo2 = "petraLocationInfo2",
            HighlightArtifacts = new List<string>
            {
                "325900",
                "325902",
                "325919",
                "325884",
                "325887",
                "325891",
            },
            HiddenArtifacts = new List<string>
            {
                "322592",
                "325918",
                "326243",
            },
            Events = new Dictionary<int, string>
            {
                [-1200] = "petra1200bce",
                [-106] = "petra106bce",
                [551] = "petra551ce",
                [1812] = "petra1812ce",
                [1958] = "petra1958ce",
                [1989] = "petra1989ce",
            },
        };

        private static readonly Wonder pyramidsGiza = new Wonder
        {
            Type = WonderType.PyramidsGiza,
            Title = "pyramidsGizaTitle",
            SubTitle = "pyramidsGizaSubTitle",
            RegionTitle = "pyramidsGizaRegionTitle",
            VideoId = "lJKX3Y7Vqvs",
            StartYr = -2600,
            EndYr = -2500,
            ArtifactStartYr = -2800,
            ArtifactEndYr = -2300,
            ArtifactCulture = "pyramidsGizaArtifactCulture",
            ArtifactGeolocation = "pyramidsGizaArtifactGeolocation",
            Lat = 29.9792,
            Lng = 31.1342,
            UnsplashCollectionId = "CSEvB5Tza9E",
            PullQuote1Top = "pyramidsGizaPullQuote1Top",
            PullQuote1Bottom = "pyramidsGizaPullQuote1Bottom",
            PullQuote1Author = "",
            PullQuote2 = "pyramidsGizaPullQuote2",
            PullQuote2Author = "pyramidsGizaPullQuote2Author",
            Callout1 = "pyramidsGizaCallout1",
            Callout2 = "pyramidsGizaCallout2",
            VideoCaption = "pyramidsGizaVideoCaption",
            MapCaption = "pyramidsGizaMapCaption",
            HistoryInfo1 = "pyramidsGizaHistoryInfo1",
            HistoryInfo2 = "pyramidsGizaHistoryInfo2",
            ConstructionInfo1 = "pyramidsGizaConstructionInfo1",
            ConstructionInfo2 = "pyramidsGizaConstructionInfo2",
            LocationInfo1 = "pyramidsGizaLocationInfo1",
            LocationInfo2 = "pyramidsGizaLocationInfo2",
            HighlightArtifacts = new List<string>
            {
                "543864",
                "546488",
                "557137",
                "543900",
                "543935",
                "544782",
            },
            HiddenArtifacts = new List<string>
            {
                "546510",
                "543896",
                "545728",
            },
            Events = new Dictionary<int, string>
            {
                [-2575] = "pyramidsGiza2575bce",
                [-2465] = "pyramidsGiza2465bce",
                [-443] = "pyramidsGiza443bce",
                [1925] = "pyramidsGiza1925ce",
                [1979] = "pyramidsGiza1979ce",
                [1990] = "pyramidsGiza1990ce",
            },
        };

        private static readonly Wonder tajMahal = new Wonder
        {
            Type = WonderType.TajMahal,
            Title = "tajMahalTitle",
            SubTitle = "tajMahalSubTitle",
            RegionTitle = "tajMahalRegionTitle",
            VideoId = "EWkDzLrhpXI",
            StartYr = 1632,
            EndYr = 1653,
            ArtifactStartYr = 1600,
            ArtifactEndYr = 1700,
            ArtifactCulture = "tajMahalArtifactCulture",
            ArtifactGeolocation = "tajMahalArtifactGeolocation",
            Lat = 27.17405039840427,
            Lng = 78.04211890065208,
            UnsplashCollectionId = "684IRta86_c",
            PullQuote1Top = "tajMahalPullQuote1Top",
            PullQuote1Bottom = "tajMahalPullQuote1Bottom",
            PullQuote1Author = "tajMahalPullQuote1Author",
            PullQuote2 = "tajMahalPullQuote2",
            PullQuote2Author = "tajMahalPullQuote2Author",
            Callout1 = "tajMahalCallout1",
            Callout2 = "tajMahalCallout2",
            VideoCaption = "tajMahalVideoCaption",
            MapCaption = "tajMahalMapCaption",
            HistoryInfo1 = "tajMahalHistoryInfo1",
            HistoryInfo2 = "tajMahalHistoryInfo2",
            ConstructionInfo1 = "tajMahalConstructionInfo1",
            ConstructionInfo2 = "tajMahalConstructionInfo2",
            LocationInfo1 = "tajMahalLocationInfo1",
            LocationInfo2 = "tajMahalLocationInfo2",
            HighlightArtifacts = new List<string>
            {
                "453341",
                "453243",
                "73309",
                "24932",
                "56230",
                "35633",
            },
            HiddenArtifacts = new List<string>
            {
                "24907",
                "453183",
                "453983",
            },
            Events = new Dictionary<int, string>
            {
                [1631] = "tajMahal1631ce",
                [1647] = "tajMahal1647ce",
                [1658] = "tajMahal1658ce",
                [1901] = "tajMahal1901ce",
                [1984] = "tajMahal1984ce",
                [1998] = "tajMahal1998ce",
            },
        };

        public Wonder GetWonder(WonderType wonder)
        {
            return wonder switch
            {
                WonderType.ChichenItza => chichenItza,
                WonderType.ChristRedeemer => christRedeemer,
                WonderType.Colosseum => colosseum,
                WonderType.GreatWall => greatWall,
                WonderType.MachuPicchu => machuPicchu,
                WonderType.Petra => petra,
                WonderType.PyramidsGiza => pyramidsGiza,
                WonderType.TajMahal => tajMahal,
                _ => null
            };
        }

        public IList<Wonder> GetWonders()
        {
            return new List<Wonder>
            {
                chichenItza, christRedeemer, colosseum, greatWall, machuPicchu, petra, pyramidsGiza, tajMahal
            };
        }
    }
}
