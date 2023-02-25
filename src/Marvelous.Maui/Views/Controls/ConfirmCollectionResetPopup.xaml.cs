using CommunityToolkit.Maui.Views;

namespace Marvelous.Maui.Views.Controls;

public partial class ConfirmCollectionResetPopup : Popup
{
	public ConfirmCollectionResetPopup(double width)
	{
		InitializeComponent();

        rootGrid.WidthRequest = width;
    }

    private void OkClicked(object sender, EventArgs e)
    {
        Close(true);
    }

    private void CancelClicked(object sender, EventArgs e)
    {
        Close(false);
    }
}
