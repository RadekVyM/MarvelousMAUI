using CommunityToolkit.Maui;
using Microsoft.Maui.Handlers;
using SimpleToolkit.Core;
using SimpleToolkit.SimpleShell;
using Marvelous.Core.Interfaces.Repositories;
using Marvelous.Core.Interfaces.Services;
using Marvelous.Core.Interfaces.ViewModels;
using Marvelous.Core.Services;
using Marvelous.Core.ViewModels;
using Marvelous.Data.Repositories;
using Marvelous.Maui.Repositories;
using Marvelous.Maui.Services;
using Marvelous.Maui.Views.Pages;

namespace Marvelous.Maui;

public static class MauiProgram
{
	public static MauiApp CreateMauiApp()
	{
		var builder = MauiApp.CreateBuilder();
		builder
			.UseMauiApp<App>()
			.ConfigureFonts(fonts =>
			{
				fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
				fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");

				fonts.AddFont("B612Mono-Regular.ttf", "Mono");
				fonts.AddFont("CinzelDecorative-Black.ttf", "CinzelDecorativeBlack");
				fonts.AddFont("CinzelDecorative-Bold.ttf", "CinzelDecorativeBold");
				fonts.AddFont("CinzelDecorative-Regular.ttf", "CinzelDecorativeRegular");
				fonts.AddFont("Raleway-Bold.ttf", "RalewayBold");
				fonts.AddFont("Raleway-BoldItalic.ttf", "RalewayBoldItalic");
				fonts.AddFont("Raleway-ExtraBold.ttf", "RalewayExtraBold");
				fonts.AddFont("Raleway-ExtraBoldItalic.ttf", "RalewayExtraBoldItalic");
				fonts.AddFont("Raleway-Italic.ttf", "RalewayItalic");
				fonts.AddFont("Raleway-Medium.ttf", "RalewayMedium");
				fonts.AddFont("Raleway-MediumItalic.ttf", "RalewayMediumItalic");
				fonts.AddFont("Raleway-Regular.ttf", "RalewayRegular");
				fonts.AddFont("TenorSans-Regular.ttf", "TenorSans");
				fonts.AddFont("YesevaOne-Regular.ttf", "YesevaOne");
            });

        builder.UseMauiCommunityToolkit();

        builder.UseSimpleShell();
        builder.UseSimpleToolkit();

        EntryHandler.Mapper.AppendToMapping("RemoveUnderline", (handler, entry) =>
		{
#if ANDROID
			handler.PlatformView.SetBackgroundColor(Android.Graphics.Color.Transparent);
#elif IOS || MACCATALYST
			handler.PlatformView.BorderStyle = UIKit.UITextBorderStyle.None;
#endif
        });

        builder.DisplayContentBehindBars();
#if ANDROID
        builder.SetDefaultStatusBarAppearance(Colors.Transparent, true);
#endif

        builder.Services.AddSingleton<ICollectibleRepository, CollectibleStorageRepository>();
		builder.Services.AddSingleton<IHighlightRepository, HighlightRepository>();
		builder.Services.AddSingleton<ISearchRepository, SearchRepository>();
		builder.Services.AddSingleton<ITimelineEventRepository, TimelineEventRepository>();
		builder.Services.AddSingleton<IUnsplashPhotoRepository, UnsplashPhotoRepository>();
		builder.Services.AddSingleton<IWonderRepository, WonderRepository>();
		builder.Services.AddSingleton<ITimelineEraRepository, TimelineEraRepository>();

        builder.Services.AddSingleton<ICollectibleService, CollectibleService>();
		builder.Services.AddSingleton<ISearchService, SearchService>();
		builder.Services.AddSingleton<IWonderService, WonderService>();
		builder.Services.AddSingleton<ITimelineEventService, TimelineEventService>();
		builder.Services.AddSingleton<ITimelineEraService, TimelineEraService>();
		builder.Services.AddSingleton<IUnsplashPhotoService, UnsplashPhotoService>();
		builder.Services.AddSingleton<IArtifactService, ArtifactService>();
        builder.Services.AddSingleton<INavigationService, NavigationService>();
        builder.Services.AddSingleton<Marvelous.Core.Interfaces.Services.IBrowser, Marvelous.Maui.Services.Browser>();

        builder.Services.AddSingleton<WonderLayerService>();

		builder.Services.AddSingleton<AppShell>();

		builder.Services.AddSingleton<IMainViewModel, MainViewModel>();
		builder.Services.AddTransient<IWonderArtifactsPageViewModel, WonderArtifactsPageViewModel>();
		builder.Services.AddTransient<IWonderHistoryPageViewModel, WonderHistoryPageViewModel>();
		builder.Services.AddTransient<IWonderMainPageViewModel, WonderMainPageViewModel>();
		builder.Services.AddTransient<IWonderPhotoPageViewModel, WonderPhotoPageViewModel>();
		builder.Services.AddTransient<IGlobalTimelinePageViewModel, GlobalTimelinePageViewModel>();
		builder.Services.AddTransient<IUnsplashPhotoDetailPageViewModel, UnsplashPhotoDetailPageViewModel>();
		builder.Services.AddTransient<IArtifactPageViewModel, ArtifactPageViewModel>();
		builder.Services.AddTransient<IArtifactsPageViewModel, ArtifactsPageViewModel>();
		builder.Services.AddTransient<ICollectionPageViewModel, CollectionPageViewModel>();
		builder.Services.AddTransient<IDiscoveredArtifactPageViewModel, DiscoveredArtifactPageViewModel>();

        builder.Services.AddSingleton<MainMenuPage>();
        builder.Services.AddSingleton<WonderArtifactsPage>();
		builder.Services.AddSingleton<WonderHistoryPage>();
		builder.Services.AddSingleton<WonderMainPage>();
		builder.Services.AddSingleton<WonderPhotoPage>();
		builder.Services.AddTransient<GlobalTimelinePage>();
		builder.Services.AddTransient<UnsplashPhotoDetailPage>();
		builder.Services.AddTransient<ArtifactPage>();
		builder.Services.AddTransient<ArtifactsPage>();
		builder.Services.AddTransient<CollectionPage>();
		builder.Services.AddTransient<DiscoveredArtifactPage>();

        builder.Services.AddMauiBlazorWebView();

        return builder.Build();
	}
}
