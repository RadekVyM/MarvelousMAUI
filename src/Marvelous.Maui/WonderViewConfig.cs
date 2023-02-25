using Microsoft.Maui.Controls.Shapes;
using Marvelous.Core.Models;

namespace Marvelous.Maui
{
    public class WonderViewConfig
    {
        public string WonderButton { get; init; }
        public string SkySphere { get; init; }
        public string Photo1 { get; init; }
        public string Photo2 { get; init; }
        public string Photo3 { get; init; }
        public string Photo4 { get; init; }
        public string Map { get; init; }
        public string ForegroundLeft { get; init; }
        public string ForegroundRight { get; init; }
        public string TopLeft { get; init; }
        public string TopRight { get; init; }
        public string MainObject { get; init; }
        public string Flattened { get; init; }

        // width / height
        public double SkySphereRatio { get; init; } = 1;
        public double ForegroundLeftRatio { get; init; }
        public double ForegroundRightRatio { get; init; }
        public double TopLeftRatio { get; init; }
        public double TopRightRatio { get; init; }
        public double MainObjectRatio { get; init; }

        public Color PrimaryColor { get; init; }
        public Color SecondaryColor { get; init; }

        public Func<double, double, Geometry> GetPhoto1ClipGeometry { get; init; }

        public int MainPageCollectiblePosition { get; init; }
        public int PhotoPageCollectiblePosition { get; init; }

        private static WonderViewConfig colosseum = new WonderViewConfig
        {
            ForegroundLeft = "colosseum_foreground_left.png",
            ForegroundRight = "colosseum_foreground_right.png",
            TopLeft = "",
            TopRight = "",
            MainObject = "colosseum_colosseum.png",
            Photo1 = "colosseum_photo_1.jpg",
            Photo2 = "colosseum_photo_2.jpg",
            Photo3 = "colosseum_photo_3.jpg",
            Photo4 = "colosseum_photo_4.jpg",
            Map = "colosseum_map.jpg",
            SkySphere = "colosseum_sun.png",
            WonderButton = "colosseum_wonder_button.png",
            Flattened = "colosseum_flattened.jpg",
            ForegroundLeftRatio = 0.51754,
            ForegroundRightRatio = 0.56085,
            MainObjectRatio = 1.48223,
            PrimaryColor = Color.FromArgb("#1c746b"),
            SecondaryColor = Color.FromArgb("#47a49b"),
            MainPageCollectiblePosition = 0,
            PhotoPageCollectiblePosition = 0,
            GetPhoto1ClipGeometry = (width, height) => new PathGeometry(new PathFigureCollection
            {
                new PathFigure
                {
                    Segments = new PathSegmentCollection
                    {
                        new QuadraticBezierSegment(new Point(width, 0), new Point(width, width / 2)),
                        new LineSegment(new Point(width, height)),
                        new LineSegment(new Point(0, height)),
                        new LineSegment(new Point(0, width / 2)),
                        new QuadraticBezierSegment(new Point(0, 0), new Point(width / 2, 0)),
                    },
                    StartPoint = new Point(width / 2, 0),
                    IsClosed = true
                }
            })
        };
        private static WonderViewConfig chichenItza = new WonderViewConfig
        {
            ForegroundLeft = "chichen_itza_foreground_left.png",
            ForegroundRight = "chichen_itza_foreground_right.png",
            TopLeft = "chichen_itza_top_left.png",
            TopRight = "chichen_itza_top_right.png",
            MainObject = "chichen_itza_chichen.png",
            Photo1 = "chichen_itza_photo_1.jpg",
            Photo2 = "chichen_itza_photo_2.jpg",
            Photo3 = "chichen_itza_photo_3.jpg",
            Photo4 = "chichen_itza_photo_4.jpg",
            Map = "chichen_itza_map.jpg",
            SkySphere = "chichen_itza_sun.png",
            WonderButton = "chichen_itza_wonder_button.png",
            Flattened = "chichen_itza_flattened.jpg",
            ForegroundLeftRatio = 1.04988,
            ForegroundRightRatio = 1.05166,
            TopLeftRatio = 0.93902,
            TopRightRatio = 0.80098,
            MainObjectRatio = 3.32203,
            PrimaryColor = Color.FromArgb("#144f27"),
            SecondaryColor = Color.FromArgb("#e0cfb7"),
            MainPageCollectiblePosition = 0,
            PhotoPageCollectiblePosition = 1,
            GetPhoto1ClipGeometry = (width, height) => new PathGeometry(new PathFigureCollection
            {
                new PathFigure
                {
                    Segments = new PathSegmentCollection
                    {
                        new LineSegment(new Point((width * 0.5) + (width * 0.2 * 0.5), 0)),
                        new LineSegment(new Point(width, height * 0.22)),
                        new LineSegment(new Point(width, height)),
                        new LineSegment(new Point(0, height)),
                        new LineSegment(new Point(0, height * 0.22)),
                    },
                    StartPoint = new Point((width * 0.5) - (width * 0.2 * 0.5), 0),
                    IsClosed = true
                }
            })
        };
        private static WonderViewConfig christRedeemer = new WonderViewConfig
        {
            ForegroundLeft = "christ_the_redeemer_foreground_left.png",
            ForegroundRight = "christ_the_redeemer_foreground_right.png",
            TopLeft = "",
            TopRight = "",
            MainObject = "christ_the_redeemer_redeemer.png",
            Photo1 = "christ_the_redeemer_photo_1.jpg",
            Photo2 = "christ_the_redeemer_photo_2.jpg",
            Photo3 = "christ_the_redeemer_photo_3.jpg",
            Photo4 = "christ_the_redeemer_photo_4.jpg",
            Map = "christ_redeemer_map.jpg",
            SkySphere = "christ_the_redeemer_sun.png",
            WonderButton = "christ_the_redeemer_wonder_button.png",
            Flattened = "christ_the_redeemer_flattened.jpg",
            ForegroundLeftRatio = 1.26374,
            ForegroundRightRatio = 1.33721,
            MainObjectRatio = 1.14095,
            PrimaryColor = Color.FromArgb("#194d43"),
            SecondaryColor = Color.FromArgb("#eb7a65"),
            MainPageCollectiblePosition = 2,
            PhotoPageCollectiblePosition = 0,
            GetPhoto1ClipGeometry = (width, height) => new PathGeometry(new PathFigureCollection
            {
                new PathFigure
                {
                    Segments = new PathSegmentCollection
                    {
                        new BezierSegment(new Point(width * 0.95, 0), new Point(width, width * 0.1), new Point(width, width * 0.35)),
                        new LineSegment(new Point(width, height)),
                        new LineSegment(new Point(0, height)),
                        new LineSegment(new Point(0, width * 0.35)),
                        new BezierSegment(new Point(0, width * 0.1), new Point(width * 0.05, 0), new Point(width / 2, 0)),
                    },
                    StartPoint = new Point(width / 2, 0),
                    IsClosed = true
                }
            })
        };
        private static WonderViewConfig greatWall = new WonderViewConfig
        {
            ForegroundLeft = "great_wall_of_china_foreground_left.png",
            ForegroundRight = "great_wall_of_china_foreground_right.png",
            TopLeft = "",
            TopRight = "",
            MainObject = "great_wall_of_china_great_wall.png",
            Photo1 = "great_wall_of_china_photo_1.jpg",
            Photo2 = "great_wall_of_china_photo_2.jpg",
            Photo3 = "great_wall_of_china_photo_3.jpg",
            Photo4 = "great_wall_of_china_photo_4.jpg",
            Map = "great_wall_map.jpg",
            SkySphere = "great_wall_of_china_sun.png",
            WonderButton = "great_wall_of_china_wonder_button.png",
            Flattened = "great_wall_of_china_flattened.jpg",
            ForegroundLeftRatio = 0.88089,
            ForegroundRightRatio = 0.89241,
            MainObjectRatio = 1.07659,
            PrimaryColor = Color.FromArgb("#612925"),
            SecondaryColor = Color.FromArgb("#8faa7b"),
            MainPageCollectiblePosition = 3,
            PhotoPageCollectiblePosition = 2,
            GetPhoto1ClipGeometry = (width, height) => new PathGeometry(new PathFigureCollection
            {
                new PathFigure
                {
                    Segments = new PathSegmentCollection
                    {
                        new QuadraticBezierSegment(new Point(width, 0), new Point(width, width / 2)),
                        new LineSegment(new Point(width, height)),
                        new LineSegment(new Point(0, height)),
                        new LineSegment(new Point(0, width / 2)),
                        new QuadraticBezierSegment(new Point(0, 0), new Point(width / 2, 0)),
                    },
                    StartPoint = new Point(width / 2, 0),
                    IsClosed = true
                }
            })
        };
        private static WonderViewConfig machuPicchu = new WonderViewConfig
        {
            ForegroundLeft = "machu_picchu_foreground_back.png",
            ForegroundRight = "machu_picchu_foreground_front.png",
            TopLeft = "",
            TopRight = "",
            MainObject = "machu_picchu_machu_picchu.png",
            Photo1 = "machu_picchu_photo_1.jpg",
            Photo2 = "machu_picchu_photo_2.jpg",
            Photo3 = "machu_picchu_photo_3.jpg",
            Photo4 = "machu_picchu_photo_4.jpg",
            Map = "machu_picchu_map.jpg",
            SkySphere = "machu_picchu_sun.png",
            WonderButton = "machu_picchu_wonder_button.png",
            Flattened = "machu_picchu_flattened.jpg",
            ForegroundLeftRatio = 1.52088,
            ForegroundRightRatio = 1.18810,
            MainObjectRatio = 1.75102,
            PrimaryColor = Color.FromArgb("#0b4161"),
            SecondaryColor = Color.FromArgb("#bfdace"),
            MainPageCollectiblePosition = 2,
            PhotoPageCollectiblePosition = 2,
            GetPhoto1ClipGeometry = (width, height) => new PathGeometry(new PathFigureCollection
            {
                new PathFigure
                {
                    Segments = new PathSegmentCollection
                    {
                        new LineSegment(new Point(width, height * 0.22)),
                        new LineSegment(new Point(width, height)),
                        new LineSegment(new Point(0, height)),
                        new LineSegment(new Point(0, height * 0.22)),
                    },
                    StartPoint = new Point(width * 0.5, 0),
                    IsClosed = true
                }
            })
        };
        private static WonderViewConfig petra = new WonderViewConfig
        {
            ForegroundLeft = "petra_foreground_left.png",
            ForegroundRight = "petra_foreground_right.png",
            TopLeft = "",
            TopRight = "",
            MainObject = "petra_petra.png",
            Photo1 = "petra_photo_1.jpg",
            Photo2 = "petra_photo_2.jpg",
            Photo3 = "petra_photo_3.jpg",
            Photo4 = "petra_photo_4.jpg",
            Map = "petra_map.jpg",
            SkySphere = "petra_moon.png",
            WonderButton = "petra_wonder_button.png",
            Flattened = "petra_flattened.jpg",
            ForegroundLeftRatio = 0.30549,
            ForegroundRightRatio = 0.33088,
            MainObjectRatio = 1.42751,
            PrimaryColor = Color.FromArgb("#424c98"),
            SecondaryColor = Color.FromArgb("#181a62"),
            MainPageCollectiblePosition = 1,
            PhotoPageCollectiblePosition = 0,
            GetPhoto1ClipGeometry = (width, height) => new PathGeometry(new PathFigureCollection
            {
                new PathFigure
                {
                    Segments = new PathSegmentCollection
                    {
                        new BezierSegment(new Point(width * 0.95, 0), new Point(width, width * 0.1), new Point(width, width * 0.35)),
                        new LineSegment(new Point(width, height)),
                        new LineSegment(new Point(0, height)),
                        new LineSegment(new Point(0, width * 0.35)),
                        new BezierSegment(new Point(0, width * 0.1), new Point(width * 0.05, 0), new Point(width / 2, 0)),
                    },
                    StartPoint = new Point(width / 2, 0),
                    IsClosed = true
                }
            })
        };
        private static WonderViewConfig pyramidsGiza = new WonderViewConfig
        {
            ForegroundLeft = "pyramids_foreground_back.png",
            ForegroundRight = "pyramids_foreground_front.png",
            TopLeft = "",
            TopRight = "",
            MainObject = "pyramids_pyramids.png",
            Photo1 = "pyramids_photo_1.jpg",
            Photo2 = "pyramids_photo_2.jpg",
            Photo3 = "pyramids_photo_3.jpg",
            Photo4 = "pyramids_photo_4.jpg",
            Map = "pyramids_giza_map.jpg",
            SkySphere = "pyramids_moon.png",
            WonderButton = "pyramids_wonder_button.png",
            Flattened = "pyramids_flattened.jpg",
            ForegroundLeftRatio = 2.52980,
            ForegroundRightRatio = 1.24295,
            MainObjectRatio = 2.00829,
            PrimaryColor = Color.FromArgb("#14194c"),
            SecondaryColor = Color.FromArgb("#424c98"),
            MainPageCollectiblePosition = 1,
            PhotoPageCollectiblePosition = 2,
            GetPhoto1ClipGeometry = (width, height) => new PathGeometry(new PathFigureCollection
            {
                new PathFigure
                {
                    Segments = new PathSegmentCollection
                    {
                        new LineSegment(new Point(width, height * 0.22)),
                        new LineSegment(new Point(width, height)),
                        new LineSegment(new Point(0, height)),
                        new LineSegment(new Point(0, height * 0.22)),
                    },
                    StartPoint = new Point(width * 0.5, 0),
                    IsClosed = true
                }
            })
        };
        private static WonderViewConfig tajMahal = new WonderViewConfig
        {
            ForegroundLeft = "taj_mahal_foreground_left.png",
            ForegroundRight = "taj_mahal_foreground_right.png",
            TopLeft = "taj_mahal_pool.png",
            TopRight = "",
            MainObject = "taj_mahal_taj_mahal.png",
            Photo1 = "taj_mahal_photo_1.jpg",
            Photo2 = "taj_mahal_photo_2.jpg",
            Photo3 = "taj_mahal_photo_3.jpg",
            Photo4 = "taj_mahal_photo_4.jpg",
            Map = "taj_mahal_map.jpg",
            SkySphere = "taj_mahal_sun.png",
            WonderButton = "taj_mahal_wonder_button.png",
            Flattened = "taj_mahal_flattened.jpg",
            ForegroundLeftRatio = 0.65574,
            ForegroundRightRatio = 0.74652,
            MainObjectRatio = 1.66667,
            TopLeftRatio = 2.35909,
            PrimaryColor = Color.FromArgb("#c86552"),
            SecondaryColor = Color.FromArgb("#612925"),
            MainPageCollectiblePosition = 3,
            PhotoPageCollectiblePosition = 1,
            GetPhoto1ClipGeometry = (width, height) => new PathGeometry(new PathFigureCollection
            {
                new PathFigure
                {
                    Segments = new PathSegmentCollection
                    {
                        new QuadraticBezierSegment(new Point(width, width * 0.15), new Point(width, width * 0.3)),
                        new LineSegment(new Point(width, height)),
                        new LineSegment(new Point(0, height)),
                        new LineSegment(new Point(0, width * 0.3)),
                        new QuadraticBezierSegment(new Point(0, width * 0.15), new Point(width * 0.5, 0)),
                    },
                    StartPoint = new Point(width * 0.5, 0),
                    IsClosed = true
                }
            })
        };

        public static WonderViewConfig GetWonderViewConfig(WonderType wonder)
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
    }
}
