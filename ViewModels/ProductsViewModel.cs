using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using FreshFarmApp.Data;
using FreshFarmApp.Models;
using FreshFarmApp.Views;
using Microsoft.Maui.Controls;

namespace FreshFarmApp.ViewModels
{
    public class ProductsViewModel : BindableObject
    {
        private readonly AppDatabase _database;
        public ICommand ProductTappedCommand { get; }
        public ProductsViewModel(AppDatabase database)
        {
            _database = database;
            Products = new ObservableCollection<FarmProduct>();
            FilteredProducts = new ObservableCollection<FarmProduct>();
            
            LoadProductsCommand = new Command(async () => await LoadProductsAsync());
            ProductTappedCommand = new Command<FarmProduct>(async product =>
            {
                if (product != null)
                {
                    // Navigate to detail page
                    await Shell.Current.Navigation.PushAsync(new ProductDetailPage(product));
                }
            });
        }

        /* -------------------- Collections -------------------- */
        public ObservableCollection<FarmProduct> Products { get; }
        public ObservableCollection<FarmProduct> FilteredProducts { get; }

        /* -------------------- Search -------------------- */
        private string _searchText = string.Empty;
        public string SearchText
        {
            get => _searchText;
            set
            {
                _searchText = value;
                OnPropertyChanged();
                ApplyFilter();
            }
        }

        /* -------------------- Commands -------------------- */
        public ICommand LoadProductsCommand { get; }

        /* -------------------- Methods -------------------- */
         private async Task LoadProductsAsync()
        {
            // Load all products from database
            var productsFromDb = await _database.GetAllProductsAsync();

            // Clear current collections
            Products.Clear();
            FilteredProducts.Clear();

            // Add products to collections
            foreach (var p in productsFromDb)
                Products.Add(p);

            foreach (var p in productsFromDb)
                FilteredProducts.Add(p);
        }

        private void ApplyFilter()
        {
            FilteredProducts.Clear();

            var filtered = string.IsNullOrWhiteSpace(SearchText)
                ? Products
                : new ObservableCollection<FarmProduct>(
                    Products.Where(p =>
                        (p.Name?.ToLower().Contains(SearchText.ToLower()) ?? false) ||
                        (p.Category?.ToLower().Contains(SearchText.ToLower()) ?? false) ||
                        (p.FarmerName?.ToLower().Contains(SearchText.ToLower()) ?? false)
                    )
                );

            foreach (var item in filtered)
                FilteredProducts.Add(item);
        }
    
    }
}
