using Marvelous.Maui.Models;

namespace Marvelous.Maui
{
    public class WonderSectionTemplateSelector : DataTemplateSelector
    {
        public DataTemplate FactsHistoryWonderSectionTemplate { get; set; }
        public DataTemplate ConstructionWonderSectionTemplate { get; set; }
        public DataTemplate LocationWonderSectionTemplate { get; set; }

        protected override DataTemplate OnSelectTemplate(object item, BindableObject container)
        {
            return item switch
            {
                FactsHistoryWonderSectionViewModel => FactsHistoryWonderSectionTemplate,
                ConstructionWonderSectionViewModel => ConstructionWonderSectionTemplate,
                LocationWonderSectionViewModel => LocationWonderSectionTemplate,
                _ => null
            };
        }
    }
}
