using FreshFarmApp.Data;
using FreshFarmApp.Services;
using FreshFarmApp.ViewModels;

namespace FreshFarmApp.Views;

public partial class Userlogin : ContentPage
{
    public Userlogin(AppDatabase database)
    {
        InitializeComponent();
        BindingContext = new UserLoginViewModel(database);
    }
       
}
