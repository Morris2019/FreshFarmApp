using System;
using FreshFarmApp.Models;

namespace FreshFarmApp.Services
{
    public static class AuthService
    {
        public static bool IsLoggedIn =>
            Preferences.Get("IsLoggedIn", false);

        public static void Login(string userId)
        {
            Preferences.Set("IsLoggedIn", true);
            Preferences.Set("UserId", userId);
        }

        public static void Logout()
        {
            Preferences.Clear();
        }
    }
}