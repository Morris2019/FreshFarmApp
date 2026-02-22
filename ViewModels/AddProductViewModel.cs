using System.Collections.ObjectModel;
using System.Windows.Input;
using FreshFarmApp.Data;
using FreshFarmApp.Models;
using Microsoft.Maui.Controls;

namespace FreshFarmApp.ViewModels
{
    public class AddProductViewModel : BindableObject
    {
        private readonly AppDatabase _database;

        public AddProductViewModel(AppDatabase database)
        {
            _database = database;

            Categories = new ObservableCollection<string>
            {
                "Vegetables",
                "Fruits",
                "Grains",
                "Tubers",
                "Livestock",
                "Others"
            };

            Unit = "kg";

            SaveCommand = new Command(async () => await SaveProductAsync());
            PickImageCommand = new Command(async () => await PickImageAsync());
        }

        /* -------------------- Commands -------------------- */

        public ICommand SaveCommand { get; }
        public ICommand PickImageCommand { get; }

        /* -------------------- Collections -------------------- */

        public ObservableCollection<string> Categories { get; }

        /* -------------------- Properties -------------------- */

        private string _name = string.Empty;
        public string Name
        {
            get => _name;
            set
            {
                _name = value;
                OnPropertyChanged();
            }
        }

        private string _category = string.Empty;
        public string Category
        {
            get => _category;
            set
            {
                _category = value;
                OnPropertyChanged();
            }
        }

         private string _imagePath = string.Empty;
        public string ImagePath
        {
            get => _imagePath;
            set { _imagePath = value; OnPropertyChanged(); }
        }
        private string _description = string.Empty;
        public string Description
        {
            get => _description;
            set
            {
                _description = value;
                OnPropertyChanged();
            }
        }

        private string _unit = "kg";
        public string Unit
        {
            get => _unit;
            set
            {
                _unit = value;
                OnPropertyChanged();
            }
        }

        private string _farmerName = string.Empty;
        public string FarmerName
        {
            get => _farmerName;
            set
            {
                _farmerName = value;
                OnPropertyChanged();
            }
        }

        private string _location = string.Empty;
        public string Location
        {
            get => _location;
            set
            {
                _location = value;
                OnPropertyChanged();
            }
        }

        private string _price = "0";
        public string Price
        {
            get => _price;
            set
            {
                _price = value;
                OnPropertyChanged();
            }
        }

        private string _quantityAvailable = "0";
        public string QuantityAvailable
        {
            get => _quantityAvailable;
            set
            {
                _quantityAvailable = value;
                OnPropertyChanged();
            }
        }

        private bool _isOrganic;
        public bool IsOrganic
        {
            get => _isOrganic;
            set
            {
                _isOrganic = value;
                OnPropertyChanged();
            }
        }

        /* -------------------- Logic -------------------- */

        private async Task SaveProductAsync()
        {
            if (string.IsNullOrWhiteSpace(Name) || string.IsNullOrWhiteSpace(Category))
            {
                await Shell.Current.DisplayAlert(
                    "Validation Error",
                    "Product name and category are required.",
                    "OK");
                return;
            }

            if (!decimal.TryParse(Price, out var parsedPrice))
                parsedPrice = 0;

            if (!double.TryParse(QuantityAvailable, out var parsedQuantity))
                parsedQuantity = 0;

            var product = new FarmProduct
            {
                Name = Name,
                Category = Category,
                Description = Description,
                Unit = Unit,
                FarmerName = FarmerName,
                Location = Location,
                IsOrganic = IsOrganic,
                Price = parsedPrice,
                QuantityAvailable = parsedQuantity,
                ImagePath = ImagePath
            };

            await _database.AddProductAsync(product);

            await Shell.Current.DisplayAlert(
                "Success",
                "Product saved successfully!",
                "OK");

            ClearForm();
        }
        private async Task PickImageAsync()
        {
            try
            {
                var result = await FilePicker.Default.PickAsync(new PickOptions
                {
                    PickerTitle = "Select a product image",
                    FileTypes = FilePickerFileType.Images
                });

                if (result != null)
                {
                    ImagePath = result.FullPath;
                }
            }
            catch (Exception ex)
            {
                await Shell.Current.DisplayAlert("Error", $"Image picking failed: {ex.Message}", "OK");
            }
        }

        private void ClearForm()
        {
            Name = string.Empty;
            Category = string.Empty;
            Description = string.Empty;
            Price = "0";
            QuantityAvailable = "0";
            FarmerName = string.Empty;
            Location = string.Empty;
            IsOrganic = false;
        }
    }
}