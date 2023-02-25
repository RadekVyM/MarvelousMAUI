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

	protected override void OnSafeAreaChanged(Thickness safeArea)
	{
#if ANDROID
		this.Padding = new Thickness(safeArea.Left, 0, safeArea.Right, safeArea.Bottom);
#else
		this.Padding = new Thickness(safeArea.Left, 0, safeArea.Right, 0);
#endif
    }
}