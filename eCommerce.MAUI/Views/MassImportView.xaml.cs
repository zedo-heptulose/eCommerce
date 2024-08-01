using eCommerce.MAUI.ViewModels;

namespace eCommerce.MAUI.Views;

public partial class MassImportView : ContentPage
{
	public MassImportView()
	{
		InitializeComponent();
        BindingContext = new MassImportViewModel();
	}
    private void CancelClicked(object sender, EventArgs e)
    {
        Shell.Current.GoToAsync("//Inventory");
    }
    private async void ImportClicked(object sender, EventArgs e)
    {
        (BindingContext as MassImportViewModel).ImportFile();
    }
}