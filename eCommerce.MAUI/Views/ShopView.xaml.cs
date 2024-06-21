namespace eCommerce.MAUI.Views;
using eCommerce.MAUI.ViewModels;

public partial class ShopView : ContentPage
{
    public ShopView()
    {
        InitializeComponent();
        BindingContext = new ShopViewModel();

    }

    private void CancelClicked(object sender, EventArgs e)
    {
        Shell.Current.GoToAsync("//MainPage");
    }

    private void ContentPage_NavigatedTo(object sender, NavigatedToEventArgs e)
    {
        (BindingContext as ShopViewModel).RefreshInventory();
        (BindingContext as ShopViewModel).RefreshCart();
    }

    private void AddToCartClicked(object sender, EventArgs e)
    {
        (BindingContext as ShopViewModel).RefreshInventory();
        (BindingContext as ShopViewModel).RefreshCart();
    }
}