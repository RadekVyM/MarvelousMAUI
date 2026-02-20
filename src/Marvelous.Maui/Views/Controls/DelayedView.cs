namespace Marvelous.Maui.Views.Controls;

public class DelayedView<TView> : Grid where TView : View, new()
{
    public int Delay { get; set; } = 100;
    public bool IsContentLoaded { get; protected set; } = false;

    public DelayedView()
    {
    }

    public async void LoadView()
    {
        if (IsContentLoaded)
            return;

        View view = null;

#if ANDROID
        await Task.Run(() =>
        {
            view = new TView
            {
                BindingContext = BindingContext,
            };
        });
#else
        view = new TView
        {
            BindingContext = BindingContext,
        };
#endif

        await Task.Delay(Delay);

        IsContentLoaded = true;

        MainThread.BeginInvokeOnMainThread(() =>
        {
            Children.Clear();
            Children.Add(view);
        });
    }

    protected override void OnBindingContextChanged()
    {
        if (Children.Any())
        {
            foreach (var child in Children)
            {
                if (child is BindableObject bindable)
                    bindable.BindingContext = BindingContext;
            }
        }
    }
}