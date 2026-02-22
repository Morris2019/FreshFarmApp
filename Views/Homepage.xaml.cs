using FreshFarmApp.Data;
using FreshFarmApp.Models;
using FreshFarmApp.ViewModels;
using FreshFarmApp.Views;

namespace FreshFarmApp.Views;

public partial class Homepage : ContentPage
{
    public Homepage(AppDatabase database)
    {
        InitializeComponent();
        BindingContext = new ProductsViewModel(database);

        // Load products when page appears
        Appearing += (s, e) =>
        {
            if (BindingContext is ProductsViewModel vm)
                vm.LoadProductsCommand.Execute(null);
        };
    }

    private async void OnProductSelected(object sender, SelectionChangedEventArgs e)
    {
        if (e.CurrentSelection.FirstOrDefault() is FarmProduct selectedProduct)
        {
            // Navigate to the detail page
            await Navigation.PushAsync(new ProductDetailPage(selectedProduct));

            // Deselect the item
            ((CollectionView)sender).SelectedItem = null;
        }
    }
}
