using Marvelous.Core.Interfaces.Services;
using Marvelous.Core.Interfaces.ViewModels;

namespace Marvelous.Maui.Views.Pages;

public partial class ArtifactPage : BaseContentPage
{
    private const double MaxImageHeight = 400;
    private const double MinImageHeight = 250;

    private IArtifactPageViewModel viewModel => BindingContext as IArtifactPageViewModel;


    public ArtifactPage(INavigationService navigationService, IArtifactPageViewModel viewModel) : base(navigationService)
    {
        BindingContext = viewModel;

        InitializeComponent();

        imageGrid.HeightRequest = MaxImageHeight;
        horizontalSeparator.IsCollapsedAnimated = false;
        horizontalSeparator.Collapsed = true;
        horizontalSeparator.IsCollapsedAnimated = true;
    }


    protected override void OnNavigatedTo(NavigatedToEventArgs args)
    {
        base.OnNavigatedTo(args);
        horizontalSeparator.Expand();
    }

    protected override void OnSafeAreaChanged(Thickness safeArea)
    {
        this.Padding = new Thickness(safeArea.Left, safeArea.Top, safeArea.Right, 0);
        detailsStackLayout.Padding = new Thickness(0, MaxImageHeight, 0, safeArea.Bottom);
    }

    private void MenuBackButtonClicked(object sender, EventArgs e)
    {
        navigationService.GoBack();
    }

    void ScrollViewScrolled(object sender, ScrolledEventArgs e)
    {
        var newImageHeight = MaxImageHeight - Math.Clamp(e.ScrollY, 0, MaxImageHeight - MinImageHeight);

        if (newImageHeight != imageGrid.HeightRequest)
            imageGrid.HeightRequest = newImageHeight;
    }
}