namespace eCommerce.MAUI.Views;
using eCommerce.MAUI.ViewModels;

public partial class InventoryView : ContentPage
{
	public InventoryView()
	{
		InitializeComponent();
		BindingContext = new InventoryViewModel();
	}

	private void AddClicked(object sender, EventArgs e)
	{
        Shell.Current.GoToAsync("//Product");
    }

	private void EditClicked(object sender, EventArgs e)
	{

	}

	private void DeleteClicked(object sender, EventArgs e)
	{

	}

    private void CancelClicked(object sender, EventArgs e)
    {
		Shell.Current.GoToAsync("//MainPage");
    }

	private void ContentPage_NavigatedTo(object sender, NavigatedToEventArgs e)
	{
		(BindingContext as InventoryViewModel).RefreshProducts();
	}
}