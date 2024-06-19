namespace eCommerce.MAUI.Views;
using eCommerce.MAUI.ViewModels;

public partial class ProductView : ContentPage
{
	public ProductView()
	{
		InitializeComponent();
	}

	private void ContentPage_NavigatedTo(object sender, NavigatedToEventArgs e)
	{
		BindingContext = new ProductViewModel();
	}

	public void OkClicked(object sender, EventArgs e)
	{
		//want to somehow add the object here. How can I do that?
		(BindingContext as ProductViewModel).AddToInventory();
		Shell.Current.GoToAsync("//Inventory");
	}

	public void CancelClicked(object sender, EventArgs e)
	{
		Shell.Current.GoToAsync("//Inventory");
	}
}