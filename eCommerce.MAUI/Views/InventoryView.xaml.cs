namespace eCommerce.MAUI.Views;
using eCommerce.MAUI.ViewModels;

public partial class InventoryView : ContentPage
{
	public InventoryView()
	{
		InitializeComponent();
		BindingContext = new InventoryViewModel();
	}

	private void EditClicked(object sender, EventArgs e)
	{
		(BindingContext as InventoryViewModel).UpdateContact();
	}

	private void DeleteClicked(object sender, EventArgs e)
	{

	}

    private void CancelClicked(object sender, EventArgs e)
    {
		Shell.Current.GoToAsync("//MainPage");
    }
}