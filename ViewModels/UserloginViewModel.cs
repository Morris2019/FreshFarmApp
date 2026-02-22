using System;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using FreshFarmApp.Data;
using FreshFarmApp.Models;
using FreshFarmApp.Services;
using FreshFarmApp.Views;
using Microsoft.Maui.Controls;

namespace FreshFarmApp.ViewModels
{
    public class UserLoginViewModel : BindableObject
    {
        private string _email;
        private string _password;
        private bool _isBusy;
        private readonly AppDatabase _database;

        public UserLoginViewModel(AppDatabase database)
        {
            _database = database;
            LoginCommand = new Command(async () => await LoginAsync(), () => !IsBusy);
            CreateAccountCommand = new Command(async () => await NavigateToRegister());
            
        }

        public string Email
        {
            get => _email;
            set { _email = value; OnPropertyChanged(); }
        }

        public string Password
        {
            get => _password;
            set { _password = value; OnPropertyChanged(); }
        }

        public bool IsBusy
        {
            get => _isBusy;
            set { _isBusy = value; OnPropertyChanged(); ((Command)LoginCommand).ChangeCanExecute(); }
        }

        public ICommand LoginCommand { get; }
        public ICommand CreateAccountCommand { get; }

        private async Task LoginAsync()
        {
            if (IsBusy) return;
            IsBusy = true;

            try
            {
                var currentPage = Application.Current?.Windows[0]?.Page;
                if (currentPage == null) return;

                // 1️⃣ Validate input
                if (string.IsNullOrWhiteSpace(Email) || string.IsNullOrWhiteSpace(Password))
                {
                    await currentPage.DisplayAlert(
                        "Error",
                        "Email and Password can't be empty",
                        "OK");
                    return;
                }

                // 2️⃣ Get user
                var user = await _database.GetUserByEmailAsync(Email);
                if (user == null)
                {
                    await currentPage.DisplayAlert(
                        "Error",
                        "No account found for this email.",
                        "OK");
                    return;
                }

                // 3️⃣ Verify password
                if (!VerifyPassword(Password, user.PasswordHash, user.Salt))
                {
                    await currentPage.DisplayAlert(
                        "Error",
                        "Wrong password. Please try again.",
                        "OK");
                    return;
                }

                // 4️⃣ Save session
                UserSession.SetUser(user);

                // 5️⃣ Navigate
                Application.Current.MainPage = new AppShell();
            }
            catch (Exception ex)
            {
                var currentPage = Application.Current?.Windows[0]?.Page;
                await currentPage.DisplayAlert(
                    "Error",
                    $"Login failed: {ex.Message}",
                    "OK");
            }
            finally
            {
                IsBusy = false;
            }
        }


       private async Task NavigateToRegister()
        {
            try
            {
                await Application.Current.Windows[0].Page.Navigation.PushAsync(new Userregister(_database));
            }
            catch (Exception ex)
            {
                // Use the active window instead of MainPage
                var currentPage = Application.Current?.Windows[0]?.Page;
                if (currentPage != null)
                    await currentPage.DisplayAlert("Navigation Error", ex.Message, "OK");
            }
        }

        private bool VerifyPassword(string enteredPassword, byte[] storedHash, byte[] salt)
        {
            using var pbkdf2 = new Rfc2898DeriveBytes(enteredPassword, salt, 10000, HashAlgorithmName.SHA256);
            var hash = pbkdf2.GetBytes(32);
            return hash.SequenceEqual(storedHash);
        }
    }
}
