using Marvelous.Core.Interfaces.Services;
using Marvelous.Core.Interfaces.ViewModels;
// TODO: Error: The type or namespace name 'Blazor' does not exist in the namespace 'Marvelous.Maui'
// Only on iOS and MacCatalyst
// https://github.com/dotnet/maui/issues/12124

namespace Marvelous.Maui.Views.Pages;

public partial class WonderPhotoPage : BaseContentPage
{
	private readonly IWonderPhotoPageViewModel viewModel;

	public WonderPhotoPage(
		INavigationService navigationService,
		IWonderPhotoPageViewModel viewModel) : base(navigationService)
    {
		BindingContext = this.viewModel = viewModel;

		InitializeComponent();

		delayed.LoadView();
    }
}