using Marvelous.Core.Interfaces.Services;
using Marvelous.Core.Interfaces.ViewModels;

namespace Marvelous.Maui.Views.Pages;

public class BaseContentPage : ContentPage, IQueryAttributable
{
    protected readonly INavigationService navigationService;
    private IParameters parameters;
    

    public BaseContentPage(INavigationService navigationService)
    {
        SafeAreaEdges = SafeAreaEdges.None;
        
        this.navigationService = navigationService;

        SimpleToolkit.SimpleShell.SimpleShell.Current.Navigating += CurrentNavigating;
    }


    private void CurrentNavigating(object sender, ShellNavigatingEventArgs e)
    {
        if (SimpleToolkit.SimpleShell.SimpleShell.Current.CurrentPage == this)
        {
            // Null parameters when navigating to a different ShellContent
            var targetString = e.Target.Location.ToString();

            if (targetString.StartsWith("//") && !targetString.Contains(e.Current.Location.ToString()))
                parameters = null;
        }
    }

    protected override void OnNavigatedTo(NavigatedToEventArgs args)
    {
        base.OnNavigatedTo(args);

        if (BindingContext is IBasePageViewModel viewModel)
        {
            viewModel.OnNavigatedTo();
        }
    }

    protected override void OnNavigatedFrom(NavigatedFromEventArgs args)
    {
        base.OnNavigatedFrom(args);

        if (BindingContext is IBasePageViewModel viewModel)
        {
            viewModel.OnNavigatedFrom();
        }
    }

    public void ApplyQueryAttributes(IDictionary<string, object> query)
    {
        var first = parameters is null;
        parameters = query.FirstOrDefault().Value as IParameters;

        if (BindingContext is IBasePageViewModel viewModel)
        {
            viewModel.OnApplyParameters(parameters);

            if (first)
                viewModel.OnApplyFirstParameters(parameters);
            else
                viewModel.OnApplyOtherParameters(parameters);
        }
    }
}