using Marvelous.Core.Interfaces.Services;
using Marvelous.Core.Interfaces.ViewModels;

namespace Marvelous.Maui.Views.Pages;

public partial class UnsplashPhotoDetailPage : BaseContentPage
{
	private readonly IUnsplashPhotoDetailPageViewModel viewModel;

	public UnsplashPhotoDetailPage(INavigationService navigationService, IUnsplashPhotoDetailPageViewModel viewModel) : base(navigationService)
	{
		BindingContext = this.viewModel = viewModel;

		InitializeComponent();
	}

	private void CloseButtonClicked(object sender, EventArgs e)
	{
		navigationService.GoBack();
	}
}