using Marvelous.Core.Interfaces.Services;
using Marvelous.Core.Interfaces.ViewModels;
using Marvelous.Maui.Services;

namespace Marvelous.Maui.Views.Pages;

public partial class ArtifactsPage : BaseContentPage
{
    private const double PeriodArtifactViewHeight = 130;
    private IArtifactsPageViewModel viewModel => BindingContext as IArtifactsPageViewModel;


    public ArtifactsPage(INavigationService navigationService, IArtifactsPageViewModel viewModel) : base(navigationService)
    {
        BindingContext = viewModel;

        InitializeComponent();

        UpdateShadowOffset(0);
    }


    private void UpdateShadowOffset(double scrollY)
    {
        var offset = (float)Math.Min(0.1 + (scrollY / (darkShadow.HeightRequest * 2)), 1);
        darkShadow.Offset = offset;
    }

    protected override void OnSafeAreaChanged(Thickness safeArea)
    {
        Padding = new Thickness(safeArea.Left, safeArea.Top, safeArea.Right, 0);

        periodArtifactView.SafeArea = safeArea;
        periodArtifactView.HeightRequest = PeriodArtifactViewHeight + safeArea.Bottom;
        footerRectangle.HeightRequest = 80 + safeArea.Bottom;
    }

    private void MenuBackButtonClicked(object sender, EventArgs e)
    {
        navigationService.GoBack();
    }

    private void ResetSearchContentButtonClicked(object sender, EventArgs e)
    {
        viewModel.SearchTerm = "";
        viewModel.UpdateSearches();
    }

    private void SearchEntryFocused(object sender, FocusEventArgs e)
    {
        suggestionBorder.IsVisible = true;
    }

    private void SearchEntryUnfocused(object sender, FocusEventArgs e)
    {
        suggestionBorder.IsVisible = false;
    }

    private void SuggestionTapped(object sender, TappedEventArgs e)
    {
        var suggestion = e.Parameter.ToString();
        searchEntry.Unfocus();
        viewModel.SearchTerm = suggestion;
        viewModel.UpdateSearches();
    }

    private void SearchEntryCompleted(object sender, EventArgs e)
    {
        searchEntry.Unfocus();
        viewModel.UpdateSearches();
    }

    private void SearchEntryTextChanged(object sender, TextChangedEventArgs e)
    {
        var shouldBeVisible = !string.IsNullOrEmpty(e.NewTextValue);

        if (resetSearchButtonBorder.IsVisible != shouldBeVisible)
            resetSearchButtonBorder.IsVisible = shouldBeVisible;
    }

    private void TimeframeTapped(object sender, TappedEventArgs e)
    {
        periodArtifactView.ToggleCollapse();
    }

    private void CollectionViewScrolled(object sender, ItemsViewScrolledEventArgs e)
    {
        UpdateShadowOffset(e.VerticalOffset);
    }
}