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

	//this is activated when navigated to, can remove breakpoint
	private void ContentPage_NavigatedTo(object sender, NavigatedToEventArgs e)
	{
		(BindingContext as InventoryViewModel).RefreshProducts();
	}

	private void InlineDelete_Clicked(object sender, EventArgs e)
	{
		(BindingContext as InventoryViewModel).RefreshProducts();
	}
}