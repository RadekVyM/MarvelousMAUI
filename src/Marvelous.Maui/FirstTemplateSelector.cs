namespace Marvelous.Maui
{
    public class FirstTemplateSelector : DataTemplateSelector
    {
        public DataTemplate FirstTemplate { get; set; }
        public DataTemplate DefaultTemplate { get; set; }

        protected override DataTemplate OnSelectTemplate(object item, BindableObject container)
        {
            if (container is CollectionView collectionView)
            {
                if (item == collectionView.ItemsSource.Cast<object>().FirstOrDefault())
                    return FirstTemplate;
                else
                    return DefaultTemplate;
            }
            else if (container is Layout layout)
            {
                if (item == layout.FirstOrDefault())
                    return FirstTemplate;
                else
                    return DefaultTemplate;
            }

            return DefaultTemplate;
        }
    }
}
