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
    public class UserRegisterViewModel : BindableObject
    {
        private string _fullName = string.Empty;
        private string _email = string.Empty;
        private string _password = string.Empty;
        private bool _isBusy;

        private readonly AppDatabase _database;

        public UserRegisterViewModel(AppDatabase database)
        {
            _database = database;
            RegisterCommand = new Command(async () => await RegisterAsync(), () => !IsBusy);
            NavigateToLoginCommand = new Command(async () => await NavigateToLogin());
        }

        public string FullName
        {
            get => _fullName;
            set { _fullName = value; OnPropertyChanged(); }
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
            set { _isBusy = value; OnPropertyChanged(); ((Command)RegisterCommand).ChangeCanExecute(); }
        }

        public ICommand RegisterCommand { get; }
        public ICommand NavigateToLoginCommand { get; }

        private async Task RegisterAsync()
        {
            if (IsBusy) return;
            IsBusy = true;

            try
            {
                if (string.IsNullOrWhiteSpace(FullName) || string.IsNullOrWhiteSpace(Email) || string.IsNullOrWhiteSpace(Password))
                {
                    await Application.Current.MainPage.DisplayAlert("Error", "All fields are required.", "OK");
                    return;
                }

                var existingUser = await _database.GetUserByEmailAsync(Email);
                if (existingUser != null)
                {
                    await Application.Current.MainPage.DisplayAlert("Error", "Email already exist.", "OK");
                    return;
                }

                // Create salt & hash
                var salt = GenerateSalt();
                var hash = HashPassword(Password, salt);

                var user = new User
                {
                    FullName = FullName,
                    Email = Email,
                    Salt = salt,
                    PasswordHash = hash
                };

                await _database.SaveUserAsync(user);

                await Application.Current.MainPage.DisplayAlert("Success", "Account created successfully!", "OK");

                // Navigate to login page
                UserSession.SetUser(user);
                Application.Current.MainPage = new AppShell();
            }
            catch (Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert("Error", $"Registration failed: {ex.Message}", "OK");
            }
            finally
            {
                IsBusy = false;
            }
        }

        private async Task NavigateToLogin()
        {
            try
            {
                await Application.Current.Windows[0].Page.Navigation.PushAsync(new Userlogin(_database));
            }
            catch (Exception ex)
            {
                // Use the active window instead of MainPage
                var currentPage = Application.Current?.Windows[0]?.Page;
                if (currentPage != null)
                    await currentPage.DisplayAlert("Navigation Error", ex.Message, "OK");
            }
        }

        private byte[] GenerateSalt()
        {
            var salt = new byte[16];
            using var rng = RandomNumberGenerator.Create();
            rng.GetBytes(salt);
            return salt;
        }

        private byte[] HashPassword(string password, byte[] salt)
        {
            using var pbkdf2 = new Rfc2898DeriveBytes(password, salt, 10000, HashAlgorithmName.SHA256);
            return pbkdf2.GetBytes(32);
        }
    }
}
