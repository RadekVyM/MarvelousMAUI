namespace Marvelous.Maui.Views.Controls;

public partial class EventCard : ContentView
{
	private const string ShowAnimationKey = "ShowAnimation";
	private const string HideAnimationKey = "HideAnimation";
	
	private const uint AnimationLength = 500;
	private bool isHideAnimationRunning = false;


	public EventCard()
	{
		BindingContext = null;

		InitializeComponent();
	}


	public void Show(object newBindingContext)
	{
		if (BindingContext == newBindingContext)
			return;

		this.AbortAnimation(ShowAnimationKey);
		this.AbortAnimation(HideAnimationKey);

        var animation = new Animation(d =>
		{
			this.Opacity = d;
		});

		this.BindingContext = newBindingContext;
		this.IsVisible = true;

		animation.Commit(this, ShowAnimationKey, length: AnimationLength);
	}
	
	public void Hide()
    {
		if (BindingContext is null || isHideAnimationRunning)
			return;

        this.AbortAnimation(ShowAnimationKey);
        this.AbortAnimation(HideAnimationKey);

        var animation = new Animation(d =>
        {
			this.Opacity = d;
        }, 1, 0);

		isHideAnimationRunning = true;

        animation.Commit(this, HideAnimationKey, finished: (d, b) =>
		{
			if (!b)
			{
                this.IsVisible = false;
                BindingContext = null;
            }

            isHideAnimationRunning = false;
        }, length: AnimationLength);
    }
}