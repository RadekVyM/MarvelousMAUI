using CommunityToolkit.Maui.Views;
using SimpleToolkit.Core;
using SimpleToolkit.SimpleShell.Extensions;
using SimpleToolkit.SimpleShell.Transitions;
using Marvelous.Core.Interfaces.Services;
using Marvelous.Core.Interfaces.ViewModels;
using Marvelous.Core.ViewModels.Parameters;
using Marvelous.Maui.Views.Controls;
using SimpleToolkit.SimpleShell;

namespace Marvelous.Maui.Views.Pages;

public partial class MainMenuPage : BaseContentPage
{
    private bool isMenuHidden = true;
    private bool isFirstNavigationTo = true;
    private Thickness defaultMenuButtonBorder = Thickness.Zero;

    private readonly IMainViewModel viewModel;


    public MainMenuPage(INavigationService navigationService, IMainViewModel mainViewModel) : base(navigationService)
	{
		BindingContext = viewModel = mainViewModel;

		InitializeComponent();

        this.SetTransition(new SimpleShellTransition(
            callback: static args => { },
            starting: static async args =>
            {
                if (args.TransitionType != SimpleShellTransitionType.Popping && args.DestinationPage is MainMenuPage page)
                    await page.illustrationsCarouselView.Show();
            },
            destinationPageInFront: args => false,
            duration: static args => 0)
            .CombinedWith(
                transition: SimpleShell.Current.GetTransition(),
                when: args => args.TransitionType == SimpleShellTransitionType.Popping));
    }


    public async void TransitionOut()
    {
        await illustrationsCarouselView.Hide();
    }

    protected override async void OnNavigatedTo(NavigatedToEventArgs args)
    {
        base.OnNavigatedTo(args);

        if (!isFirstNavigationTo)
            return;

        await Task.Delay(1000);

        ShowAboutThisAppPopup();

        isFirstNavigationTo = false;
    }

    protected override void OnSafeAreaChanged(Thickness safeArea)
    {
        if (defaultMenuButtonBorder == Thickness.Zero)
            defaultMenuButtonBorder = menuButtonBorder.Margin;

        menuButtonBorder.Margin = new Thickness(
            defaultMenuButtonBorder.Left + safeArea.Left,
            defaultMenuButtonBorder.Top + safeArea.Top,
            defaultMenuButtonBorder.Right,
            defaultMenuButtonBorder.Bottom);
    }

    protected override bool OnBackButtonPressed()
    {
        if (isMenuHidden)
        {
            return base.OnBackButtonPressed();
        }
        else
        {
            appMenu.Hide();
            return true;
        }
    }

    private void IllustrationsCarouselViewClosing(object sender, EventArgs e)
    {
        var carousel = sender as IllustrationsCarouselView;

        navigationService.GoTo(PageType.WonderMainPage, new WonderPageParameters(carousel.CurrentWonder.Type));
    }

    private void IllustrationsCarouselViewHiding(object sender, EventArgs e)
    {
        var carousel = sender as IllustrationsCarouselView;

        menuButtonBorder.IsVisible = false;
    }

    private void IllustrationsCarouselViewPresenting(object sender, EventArgs e)
    {
        var carousel = sender as IllustrationsCarouselView;

        menuButtonBorder.IsVisible = isMenuHidden;
    }

    private void MenuButtonClicked(object sender, EventArgs e)
    {
        var button = sender as ContentButton;

        isMenuHidden = false;

        appMenu.Show();
    }

    private void AppMenuHidden(object sender, EventArgs e)
    {
        isMenuHidden = true;
        menuButtonBorder.IsVisible = true;
    }

    private void AppMenuPresenting(object sender, EventArgs e)
    {
        isMenuHidden = false;
        menuButtonBorder.IsVisible = false;
    }

    private void AboutThisAppClicked(object sender, EventArgs e)
    {
        ShowAboutThisAppPopup();
    }

    private void ShowAboutThisAppPopup()
    {
        var popup = new AboutThisAppPopup(Math.Min(this.Width - 60d, 400d));
        this.ShowPopup(popup);
    }
}