namespace eCommerce.MAUI.Views;

public partial class ProductView : ContentPage
{
	public ProductView()
	{
		InitializeComponent();
	}

	public void OkClicked(object sender, EventArgs e)
	{
		Shell.Current.GoToAsync("//MainPage");
	}

	public void CancelClicked(object sender, EventArgs e)
	{
		Shell.Current.GoToAsync("//MainPage");
	}
}