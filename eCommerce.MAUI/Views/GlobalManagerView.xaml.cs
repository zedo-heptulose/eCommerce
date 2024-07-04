using eCommerce.MAUI.ViewModels;

namespace eCommerce.MAUI.Views;

public partial class GlobalManagerView : ContentPage
{
	public GlobalManagerView()
	{
		InitializeComponent();
		BindingContext = new GlobalManagerViewModel();
	}

	private void GoBackClicked(object sender, EventArgs e)
	{
		Shell.Current.GoToAsync("//MainPage");
	}



}