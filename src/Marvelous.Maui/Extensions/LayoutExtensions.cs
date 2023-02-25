namespace Marvelous.Maui.Extensions
{
    public static class LayoutExtensions
    {
        public static void LayoutChildTo(this AbsoluteLayout absoluteLayout, View view, Rect bounds)
        {
            if (view is null || bounds == default(Rect) || bounds.IsEmpty)
            {
                return;
            }

            AbsoluteLayout.SetLayoutBounds(view, bounds);
        }
    }
}
