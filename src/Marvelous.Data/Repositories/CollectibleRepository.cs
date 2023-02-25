using Marvelous.Core.Interfaces.Repositories;
using Marvelous.Core.Models;

namespace Marvelous.Data.Repositories
{
    public class CollectibleRepository : ICollectibleRepository
    {
        // Note: look up a human readable page with:
        // https://www.metmuseum.org/art/collection/search/503940
        // where 503940 is the ID.
        private static readonly List<Collectible> collectibles = new List<Collectible> {
          // ChichenItza
          new Collectible {
            Title = "Pendant",
            Wonder = WonderType.ChichenItza,
            ArtifactId = "701645",
            ImageUrl = "https://images.metmuseum.org/CRDImages/ao/original/DP-25104-001.jpg",
            ImageUrlSmall = "https://images.metmuseum.org/CRDImages/ao/mobile-large/DP-25104-001.jpg",
            IconType = CollectibleIconType.Jewelry,
          },
          new Collectible {
            Title = "Bird Ornament",
            Wonder = WonderType.ChichenItza,
            ArtifactId = "310555",
            ImageUrl = "https://images.metmuseum.org/CRDImages/ao/original/DP-23474-001.jpg",
            ImageUrlSmall = "https://images.metmuseum.org/CRDImages/ao/mobile-large/DP-23474-001.jpg",
            IconType = CollectibleIconType.Jewelry,
          },
          new Collectible {
            Title = "La Prison, à Chichen-Itza",
            Wonder = WonderType.ChichenItza,
            ArtifactId = "286467",
            ImageUrl = "https://images.metmuseum.org/CRDImages/ph/original/DP132063.jpg",
            ImageUrlSmall = "https://images.metmuseum.org/CRDImages/ph/mobile-large/DP132063.jpg",
            IconType = CollectibleIconType.Picture,
          },

          // ChristRedeemer
          new Collectible {
            Title = "Engraved Horn",
            Wonder = WonderType.ChristRedeemer,
            ArtifactId = "501302",
            ImageUrl = "https://images.metmuseum.org/CRDImages/mi/original/MUS550A2.jpg",
            ImageUrlSmall = "https://images.metmuseum.org/CRDImages/mi/mobile-large/MUS550A2.jpg",
            IconType = CollectibleIconType.Statue,
          },
          new Collectible {
            Title = "Fixed fan",
            Wonder = WonderType.ChristRedeemer,
            ArtifactId = "157985",
            ImageUrl = "https://images.metmuseum.org/CRDImages/ci/original/48.60_front_CP4.jpg",
            ImageUrlSmall = "https://images.metmuseum.org/CRDImages/ci/mobile-large/48.60_front_CP4.jpg",
            IconType = CollectibleIconType.Jewelry,
          },
          new Collectible {
            Title = "Handkerchiefs (one of two)",
            Wonder = WonderType.ChristRedeemer,
            ArtifactId = "227759",
            ImageUrl = "https://images.metmuseum.org/CRDImages/ad/original/DP2896.jpg",
            ImageUrlSmall = "https://images.metmuseum.org/CRDImages/ad/mobile-large/DP2896.jpg",
            IconType = CollectibleIconType.Textile,
          },

          // Colosseum
          new Collectible {
            Title = "Glass hexagonal amphoriskos",
            Wonder = WonderType.Colosseum,
            ArtifactId = "245376",
            ImageUrl = "https://images.metmuseum.org/CRDImages/gr/original/DP124005.jpg",
            ImageUrlSmall = "https://images.metmuseum.org/CRDImages/gr/mobile-large/DP124005.jpg",
            IconType = CollectibleIconType.Vase,


          },
          new Collectible {
            Title = "Bronze plaque of Mithras slaying the bull",
            Wonder = WonderType.Colosseum,
            ArtifactId = "256570",
            ImageUrl = "https://images.metmuseum.org/CRDImages/gr/original/DP119236.jpg",
            ImageUrlSmall = "https://images.metmuseum.org/CRDImages/gr/mobile-large/DP119236.jpg",
            IconType = CollectibleIconType.Statue,


          },
          new Collectible {
            Title = "Interno del Colosseo",
            Wonder = WonderType.Colosseum,
            ArtifactId = "286136",
            ImageUrl = "https://images.metmuseum.org/CRDImages/ph/original/DP138036.jpg",
            ImageUrlSmall = "https://images.metmuseum.org/CRDImages/ph/mobile-large/DP138036.jpg",
            IconType = CollectibleIconType.Picture,


          },

          // GreatWall
          new Collectible {
            Title = "Biographies of Lian Po and Lin Xiangru",
            Wonder = WonderType.GreatWall,
            ArtifactId = "39918",
            ImageUrl = "https://images.metmuseum.org/CRDImages/as/original/DP153769.jpg",
            ImageUrlSmall = "https://images.metmuseum.org/CRDImages/as/mobile-large/DP153769.jpg",
            IconType = CollectibleIconType.Scroll,


          },
          new Collectible {
            Title = "Jar with Dragon",
            Wonder = WonderType.GreatWall,
            ArtifactId = "39666",
            ImageUrl = "https://images.metmuseum.org/CRDImages/as/original/DT5083.jpg",
            ImageUrlSmall = "https://images.metmuseum.org/CRDImages/as/mobile-large/DT5083.jpg",
            IconType = CollectibleIconType.Vase,


          },
          new Collectible {
            Title = "Panel with Peonies and Butterfly",
            Wonder = WonderType.GreatWall,
            ArtifactId = "39735",
            ImageUrl = "https://images.metmuseum.org/CRDImages/as/original/DT834.jpg",
            ImageUrlSmall = "https://images.metmuseum.org/CRDImages/as/mobile-large/DT834.jpg",
            IconType = CollectibleIconType.Textile,


          },

          // MachuPicchu
          new Collectible {
            Title = "Eight-Pointed Star Tunic",
            Wonder = WonderType.MachuPicchu,
            ArtifactId = "308120",
            ImageUrl = "https://images.metmuseum.org/CRDImages/ao/original/ra33.149.100.R.jpg",
            ImageUrlSmall = "https://images.metmuseum.org/CRDImages/ao/mobile-large/ra33.149.100.R.jpg",
            IconType = CollectibleIconType.Textile,


          },
          new Collectible {
            Title = "Camelid figurine",
            Wonder = WonderType.MachuPicchu,
            ArtifactId = "309960",
            ImageUrl = "https://images.metmuseum.org/CRDImages/ao/original/DP-13440-031.jpg",
            ImageUrlSmall = "https://images.metmuseum.org/CRDImages/ao/mobile-large/DP-13440-031.jpg",
            IconType = CollectibleIconType.Statue,


          },
          new Collectible {
            Title = "Double Bowl",
            Wonder = WonderType.MachuPicchu,
            ArtifactId = "313341",
            ImageUrl = "https://images.metmuseum.org/CRDImages/ao/original/DP-24356-001.jpg",
            ImageUrlSmall = "https://images.metmuseum.org/CRDImages/ao/mobile-large/DP-24356-001.jpg",
            IconType = CollectibleIconType.Vase,


          },

          // Petra
          new Collectible {
            Title = "Camel and riders",
            Wonder = WonderType.Petra,
            ArtifactId = "322592",
            ImageUrl = "https://images.metmuseum.org/CRDImages/an/original/DP-14352-001.jpg",
            ImageUrlSmall = "https://images.metmuseum.org/CRDImages/an/mobile-large/DP-14352-001.jpg",
            IconType = CollectibleIconType.Statue,


          },
          new Collectible {
            Title = "Vessel",
            Wonder = WonderType.Petra,
            ArtifactId = "325918",
            ImageUrl = "https://images.metmuseum.org/CRDImages/an/original/hb67_246_37.jpg",
            ImageUrlSmall = "https://images.metmuseum.org/CRDImages/an/mobile-large/hb67_246_37.jpg",
            IconType = CollectibleIconType.Vase,


          },
          new Collectible {
            Title = "Open bowl",
            Wonder = WonderType.Petra,
            ArtifactId = "326243",
            ImageUrl = "https://images.metmuseum.org/CRDImages/an/original/DT904.jpg",
            ImageUrlSmall = "https://images.metmuseum.org/CRDImages/an/mobile-large/DT904.jpg",
            IconType = CollectibleIconType.Vase,


          },

          // PyramidsGiza
          new Collectible {
            Title = "Two papyrus fragments",
            Wonder = WonderType.PyramidsGiza,
            ArtifactId = "546510",
            ImageUrl = "https://images.metmuseum.org/CRDImages/eg/original/09.180.537A_recto_0083.jpg",
            ImageUrlSmall = "https://images.metmuseum.org/CRDImages/eg/mobile-large/09.180.537A_recto_0083.jpg",
            IconType = CollectibleIconType.Scroll,
          },
          new Collectible {
            Title = "Fragmentary Face of King Khafre",
            Wonder = WonderType.PyramidsGiza,
            ArtifactId = "543896",
            ImageUrl = "https://images.metmuseum.org/CRDImages/eg/original/DT11751.jpg",
            ImageUrlSmall = "https://images.metmuseum.org/CRDImages/eg/mobile-large/DT11751.jpg",
            IconType = CollectibleIconType.Statue,
          },
          new Collectible {
            Title = "Jewelry Elements",
            Wonder = WonderType.PyramidsGiza,
            ArtifactId = "545728",
            ImageUrl = "https://images.metmuseum.org/CRDImages/eg/original/DP327402.jpg",
            ImageUrlSmall = "https://images.metmuseum.org/CRDImages/eg/mobile-large/DP327402.jpg",
            IconType = CollectibleIconType.Jewelry,
          },

          // TajMahal
          new Collectible {
            Title = "Dagger with Scabbard",
            Wonder = WonderType.TajMahal,
            ArtifactId = "24907",
            ImageUrl = "https://images.metmuseum.org/CRDImages/aa/original/DP157706.jpg",
            ImageUrlSmall = "https://images.metmuseum.org/CRDImages/aa/mobile-large/DP157706.jpg",
            IconType = CollectibleIconType.Jewelry,


          },
          new Collectible {
            Title = "The House of Bijapur",
            Wonder = WonderType.TajMahal,
            ArtifactId = "453183",
            ImageUrl = "https://images.metmuseum.org/CRDImages/is/original/DP231353.jpg",
            ImageUrlSmall = "https://images.metmuseum.org/CRDImages/is/mobile-large/DP231353.jpg",
            IconType = CollectibleIconType.Picture,


          },
          new Collectible {
            Title = "Panel of Nasta\"liq Calligraphy",
            Wonder = WonderType.TajMahal,
            ArtifactId = "453983",
            ImageUrl = "https://images.metmuseum.org/CRDImages/is/original/DP299944.jpg",
            ImageUrlSmall = "https://images.metmuseum.org/CRDImages/is/mobile-large/DP299944.jpg",
            IconType = CollectibleIconType.Scroll,
          },
        };

        public virtual IList<Collectible> GetCollectibles()
        {
            return collectibles;
        }

        public virtual IList<Collectible> GetCollectiblesForWonder(WonderType wonder)
        {
            return GetCollectibles()
                .Where(c => c.Wonder == wonder)
                .ToList();
        }

        public virtual void UpdateCollectibleState(Collectible collectible, CollectibleState state)
        {
            collectible.CollectibleState = state;
        }
    }
}
