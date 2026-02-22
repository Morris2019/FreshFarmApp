using FreshFarmApp.Data;
using FreshFarmApp.Services;
using FreshFarmApp.Views;

namespace FreshFarmApp;

public partial class App : Application
{
    // Add a field to hold the database if you need it later
    private readonly AppDatabase _database;

    public App(AppDatabase database) // Receive the database here
    {
        InitializeComponent();
        _database = database;

        if (UserSession.CurrentUser == null)
        {
            // Pass the database into the Userlogin constructor
            MainPage = new NavigationPage(new Userlogin(_database));
        }
        else
        {
            MainPage = new AppShell();
        }
    }
}