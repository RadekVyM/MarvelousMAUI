namespace Marvelous.Maui.Views.Controls;

public partial class ConfirmCollectionResetPopup : ContentPage
{
    public TaskCompletionSource<bool> Result { get; } = new();

	public ConfirmCollectionResetPopup()
	{
		InitializeComponent();

        Unloaded += OnUnloaded;
    }

    private void OnUnloaded(object sender, EventArgs e)
    {
        if (!Result.Task.IsCompleted)
            Result.SetResult(false);
    }

    private async void OkClicked(object sender, EventArgs e)
    {
        Result.SetResult(true);
        // TODO: It crashes on iOS. It may be a problem in SimpleShell
        await Navigation.PopModalAsync();
    }

    private async void CancelClicked(object sender, EventArgs e)
    {
        Result.SetResult(false);
        // TODO: It crashes on iOS. It may be a problem in SimpleShell
        await Navigation.PopModalAsync();
    }
}