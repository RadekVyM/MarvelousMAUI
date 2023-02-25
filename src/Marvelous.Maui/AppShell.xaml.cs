using SimpleToolkit.Core;
using SimpleToolkit.SimpleShell;
using SimpleToolkit.SimpleShell.Extensions;
using SimpleToolkit.SimpleShell.Transitions;
using Marvelous.Core.Interfaces.Services;
using Marvelous.Core.Interfaces.ViewModels;
using Marvelous.Core.ViewModels.Parameters;
using Marvelous.Maui.Extensions;
using Marvelous.Maui.Views.Illustrations;
using Marvelous.Maui.Views.Pages;

namespace Marvelous.Maui;

public partial class AppShell : SimpleShell
{
	private readonly INavigationService navigationService;
	private readonly IMainViewModel viewModel;


	public AppShell(INavigationService navigationService, IMainViewModel mainViewModel)
	{
		BindingContext = viewModel = mainViewModel;

		InitializeComponent();

		this.navigationService = navigationService;

		(RootPageOverlay as View).BindingContext = BindingContext;
        (Content as View).BindingContext = BindingContext;

		Loaded += AppShellLoaded;

		RegisterRoutes();
		SetTransition();

		Navigating += AppShellNavigating;
	}


	private static void RegisterRoutes()
	{
        RegisterRoute(PageType.GlobalTimelinePage, typeof(GlobalTimelinePage));
        RegisterRoute(PageType.UnsplashPhotoDetailPage, typeof(UnsplashPhotoDetailPage));
        RegisterRoute(PageType.ArtifactPage, typeof(ArtifactPage));
        RegisterRoute(PageType.ArtifactsPage, typeof(ArtifactsPage));
        RegisterRoute(PageType.CollectionPage, typeof(CollectionPage));
        RegisterRoute(PageType.DiscoveredArtifactPage, typeof(DiscoveredArtifactPage));

        static void RegisterRoute(PageType pageType, Type page) =>
            Routing.RegisterRoute(pageType.ToString(), page);
    }

    private void SetTransition()
	{
		this.SetTransition(
            static args =>
			{
				switch (args.TransitionType)
				{
					case SimpleShellTransitionType.Pushing:
						args.DestinationPage.Opacity = args.DestinationPage.Width < 0 ? 0 : 1;
#if IOS || MACCATALYST
						args.DestinationPage.Scale = 0.99 + (0.01 * args.Progress);
#endif
                        args.DestinationPage.TranslationX = Math.Max((1 - args.Progress) * args.DestinationPage.Width, 0);
						break;
					case SimpleShellTransitionType.Popping:
#if IOS || MACCATALYST
						args.OriginPage.Scale = 0.99 + (0.01 * (1 - args.Progress));
#endif
						args.OriginPage.TranslationX = args.Progress * args.OriginPage.Width;
						break;
				}
			},
			starting: static args =>
			{
				if (args.OriginPage is MainMenuPage menuPage && args.TransitionType != SimpleShellTransitionType.Pushing)
					menuPage.TransitionOut();
			},
			finished: static args =>
			{
				args.DestinationPage.Opacity = 1;
				args.DestinationPage.Scale = 1;
				args.DestinationPage.TranslationX = 0;
				args.OriginPage.Opacity = 1;
				args.OriginPage.Scale = 1;
				args.OriginPage.TranslationX = 0;
			},
			duration: static args =>
			{
				if (args.OriginPage is MainMenuPage && args.TransitionType != SimpleShellTransitionType.Pushing)
					return BaseIllustration.AnimateOutLength;
				else if (args.TransitionType == SimpleShellTransitionType.Switching)
					return 0;
				else
					return 250u;
			},
			destinationPageInFront: static args =>
			{
				return args.TransitionType switch
				{
					SimpleShellTransitionType.Switching => args.OriginPage is not MainMenuPage,
					SimpleShellTransitionType.Popping => false,
					_ => true
				};
			});
	}

	protected override bool OnBackButtonPressed()
	{
		if (CurrentPage is WonderMainPage || CurrentPage is WonderPhotoPage || CurrentPage is WonderArtifactsPage || CurrentPage is WonderHistoryPage)
		{
			navigationService.GoTo(PageType.MainMenuPage);
			return true;
		}
		else if (CurrentPage is CollectionPage)
        {
			Shell.Current.Navigation.PopToRootAsync();
            return true;
        }
		else
		{
            return base.OnBackButtonPressed();
        }
	}

	private static void AppShellLoaded(object sender, EventArgs e)
	{
		var shell = sender as AppShell;

        shell.Window.SubscribeToSafeAreaChanges(safeArea =>
		{
			var margin = new Thickness(safeArea.Left, 0, safeArea.Right, safeArea.Bottom);
            shell.statusBarBackground.HeightRequest = safeArea.Top;
            shell.tabBar.OnSafeAreaChanged(safeArea);
		});
	}

    private static void AppShellNavigating(object sender, ShellNavigatingEventArgs e)
    {
        var shell = sender as AppShell;
        var route = shell.CurrentItem.CurrentItem.CurrentItem.Route;

        if (route == shell.mainMenuShellContent.Route)
        {
            shell.tabBar.IsVisible = false;
        }
    }

    private void TabBarItemSelected(object sender, EventArgs e)
	{
        var button = sender as View;
        var shellItem = button.BindingContext as BaseShellItem;

		navigationService.GoTo(ShellProperties.GetPageType(shellItem), new WonderPageParameters(viewModel.CurrentWonder.Type));
    }

	private void TabBarWonderClicked(object sender, EventArgs e)
	{
		navigationService.GoTo(PageType.MainMenuPage);
    }
}
