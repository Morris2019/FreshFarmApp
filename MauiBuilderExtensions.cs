using FreshFarmApp.Services;
using FreshFarmApp.ViewModels;
using FreshFarmApp.Views;

namespace FreshFarmApp;

public static class MauiBuilderExtensions
{
    public static MauiAppBuilder RegisterAppServices(this MauiAppBuilder builder)
    {
       // builder.Services.AddSingleton<INewsService, MockNewsService>();
        return builder;
    }

    public static MauiAppBuilder RegisterViewModels(this MauiAppBuilder builder)
    {
        builder.Services.AddTransient<HomeViewModel>();
        builder.Services.AddTransient<SectionsViewModel>();
        builder.Services.AddTransient<FarmViewModel>();
         builder.Services.AddTransient<UserLoginViewModel>();
        builder.Services.AddTransient<UserRegisterViewModel>();
        builder.Services.AddTransient<AddProductViewModel>();
        builder.Services.AddTransient<AddProductPage>();

        builder.Services.AddTransient<Homepage>();
        builder.Services.AddTransient<SectionsPage>();
        builder.Services.AddTransient<FarmInfo>();
        builder.Services.AddTransient<BookmarksPage>();
        builder.Services.AddTransient<ProfilePage>();
         builder.Services.AddTransient<Userlogin>();
        builder.Services.AddTransient<Userregister>();
        builder.Services.AddTransient<ProfilePageViewModel>();

        builder.Services.AddTransient<Userlogin>(); // Your login page
        builder.Services.AddTransient<UserLoginViewModel>();

        builder.Services.AddTransient<Userregister>(); // Your register page

        return builder;
    }
}
