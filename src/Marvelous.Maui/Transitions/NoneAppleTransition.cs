#if IOS || MACCATALYST

namespace Marvelous.Maui.Transitions;

public class NoneAppleTransition : Foundation.NSObject, UIKit.IUIViewControllerAnimatedTransitioning
{
    private readonly int duration;

    public NoneAppleTransition(int duration)
    {
        this.duration = duration;
    }

    public async void AnimateTransition(UIKit.IUIViewControllerContextTransitioning transitionContext)
    {
        var toView = transitionContext.GetViewFor(UIKit.UITransitionContext.ToViewKey);
        var container = transitionContext.ContainerView;
        var duration = TransitionDuration(transitionContext);

        container.AddSubview(toView);

        await Task.Delay((int)(duration * 1000));

        transitionContext.CompleteTransition(!transitionContext.TransitionWasCancelled);
    }

    public double TransitionDuration(UIKit.IUIViewControllerContextTransitioning transitionContext)
    {
        return duration / 1000d;
    }
}

#endif