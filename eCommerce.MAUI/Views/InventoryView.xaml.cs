namespace eCommerce.MAUI.Views;
using eCommerce.MAUI.ViewModels;

public partial class InventoryView : ContentPage
{
	public InventoryView()
	{
		InitializeComponent();
		BindingContext = new InventoryViewModel();
	}
	
	//this is successfully reached, don't need a breakpoint
	private void AddClicked(object sender, EventArgs e)
	{
        Shell.Current.GoToAsync("//Product");
    }

	//this also works.
    private void CancelClicked(object sender, EventArgs e)
    {
		Shell.Current.GoToAsync("//MainPage");
    }

	private void SearchClicked(object sender, EventArgs e)
	{
		(BindingContext as InventoryViewModel).RefreshProducts();
	}

	//this is activated when navigated to, can remove breakpoint
	private void ContentPage_NavigatedTo(object sender, NavigatedToEventArgs e)
	{
		(BindingContext as InventoryViewModel).RefreshProducts();
	}

	private async void InlineDelete_Clicked(object sender, EventArgs e)
	{
		Thread.Sleep(50);
		(BindingContext as InventoryViewModel).RefreshProducts();
	}

	private async void ImportClicked(object sender, EventArgs e)
	{
		Shell.Current.GoToAsync("//MassImport");
	}

}