using FreshFarmApp.Data;
using FreshFarmApp.Services;
using FreshFarmApp.ViewModels;

namespace FreshFarmApp.Views;

public partial class ProfilePage : ContentPage
{
    public ProfilePage(AppDatabase database)
    {
        InitializeComponent();
        BindingContext = new ProfilePageViewModel(database);
    }
   
}
  
