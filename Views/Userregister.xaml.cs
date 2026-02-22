using FreshFarmApp.Data;
using FreshFarmApp.Services;
using FreshFarmApp.ViewModels;

namespace FreshFarmApp.Views;

public partial class Userregister : ContentPage
{
    public Userregister(AppDatabase database)
    {
        InitializeComponent();
        this.BindingContext = new UserRegisterViewModel(database);
    }
}
