using Marvelous.Core.Interfaces.Services;

namespace Marvelous.Maui.Extensions
{
    public class ShellProperties
    {
        public static readonly BindableProperty ActiveIconProperty =
            BindableProperty.CreateAttached("ActiveIcon", typeof(ImageSource), typeof(BaseShellItem), null);

        public static readonly BindableProperty PageTypeProperty =
            BindableProperty.CreateAttached("PageType", typeof(PageType), typeof(BaseShellItem), PageType.WonderMainPage);

        public static ImageSource GetActiveIcon(BindableObject view)
        {
            return (ImageSource)view.GetValue(ActiveIconProperty);
        }

        public static void SetActiveIcon(BindableObject view, ImageSource icon)
        {
            view.SetValue(ActiveIconProperty, icon);
        }

        public static PageType GetPageType(BindableObject view)
        {
            return (PageType)view.GetValue(PageTypeProperty);
        }

        public static void SetPageType(BindableObject view, PageType page)
        {
            view.SetValue(PageTypeProperty, page);
        }
    }
}
