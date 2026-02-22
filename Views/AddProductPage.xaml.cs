using FreshFarmApp.Data;
using FreshFarmApp.Models;
using FreshFarmApp.Services;
using FreshFarmApp.ViewModels;

namespace FreshFarmApp.Views;

public partial class AddProductPage : ContentPage
{

    public AddProductPage(AppDatabase database)
	{
		InitializeComponent();
        BindingContext = new AddProductViewModel(database);
    }

   
}
