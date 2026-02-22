using FreshFarmApp.Models;

namespace FreshFarmApp.Views;

public partial class ProductDetailPage : ContentPage
{
    private readonly FarmProduct _product;

    public ProductDetailPage(FarmProduct product)
    {
        InitializeComponent();
        _product = product;

        // Bind product info to the UI
        ProductNameLabel.Text = _product.Name;
        ProductCategoryLabel.Text = _product.Category;
        ProductPriceLabel.Text = $"₵ {_product.Price:F2}";
        ProductFarmerLabel.Text = _product.FarmerName;
        ProductImage.Source = _product.ImagePath;
        ProductDescriptionLabel.Text = _product.Description;
    }
}
