using FreshFarmApp.Models;

namespace FreshFarmApp.Services
{
    public static class UserSession
    {
        public static User? CurrentUser { get; private set; }

        public static bool IsLoggedIn => CurrentUser != null;

        public static void SetUser(User user)
        {
            CurrentUser = user;
        }

        public static void Logout()
        {
            CurrentUser = null;
        }
    }
}
