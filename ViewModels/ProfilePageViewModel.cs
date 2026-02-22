using System.Collections.ObjectModel;
using System.Windows.Input;
using FreshFarmApp.Data;
using FreshFarmApp.Services;
using Microsoft.Maui.Controls;

namespace FreshFarmApp.ViewModels
{
    public class ProfilePageViewModel : BaseViewModel
   {
        public string FullName => UserSession.CurrentUser?.FullName ?? "";
        public string Email => UserSession.CurrentUser?.Email ?? "";
        private readonly AppDatabase _database;
        public ICommand LogoutCommand { get; }
       public ObservableCollection<ProfileMenuItem> MenuItems { get; }

       public ICommand SelectMenuCommand { get; }

       
        public ProfilePageViewModel(AppDatabase database)
        {
            _database = database;
            LogoutCommand = new Command(async () => await Logout());
            MenuItems = new ObservableCollection<ProfileMenuItem>
            {
                new ProfileMenuItem { Title = "Edit Profile", Body = "\uf044" },
                new ProfileMenuItem { Title = "Settings", Body = "\uf013" },
                new ProfileMenuItem { Title = "Logout", Body = "\uf2f5" }
            };

            // Initialize tap command
            SelectMenuCommand = new Command<ProfileMenuItem>(OnMenuSelected);
            CheckUserStatus();
        }
        private async void CheckUserStatus()
        {
            if (UserSession.CurrentUser == null)
            {
                // Use // to reset the navigation stack so they can't "Go Back" to the profile
                await Shell.Current.GoToAsync("Userlogin"); 
            }
        }
        public class ProfileMenuItem
        {
            public string Title { get; set; }
            public string Body { get; set; }
        }
        private async Task Logout()
        {
            UserSession.Logout();
             await Shell.Current.GoToAsync("Userlogin");
        }
       private async void OnMenuSelected(ProfileMenuItem item)
       {
            if (item.Title == "Logout")
            {
                await Logout();
            }
       }
    }
}
