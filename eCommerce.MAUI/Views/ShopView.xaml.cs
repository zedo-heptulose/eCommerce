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

    private void SearchClicked(object sender, EventArgs e)
    {
        (BindingContext as ShopViewModel).RefreshInventory();
    }

    private void RemoveItemClicked(object sender, EventArgs e)
    {
        (BindingContext as ShopViewModel).RefreshInventory();
        (BindingContext as ShopViewModel).RefreshCart();
        (BindingContext as ShopViewModel).RefreshPrices();
    }
    private void ContentPage_NavigatedTo(object sender, NavigatedToEventArgs e)
    {
        (BindingContext as ShopViewModel).RefreshInventory();
        (BindingContext as ShopViewModel).RefreshCart();
        (BindingContext as ShopViewModel).RefreshPrices();
    }

    private void AddToCartClicked(object sender, EventArgs e)
    {
        (BindingContext as ShopViewModel).RefreshInventory();
        (BindingContext as ShopViewModel).RefreshCart();
        (BindingContext as ShopViewModel).RefreshPrices();
    }

    private void CartSelected(object sender, EventArgs e)
    {
        (BindingContext as ShopViewModel).RefreshCart();
        (BindingContext as ShopViewModel).RefreshPrices();
    }
}