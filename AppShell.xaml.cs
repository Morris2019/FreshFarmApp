namespace FreshFarmApp;

using FreshFarmApp.Services;
using FreshFarmApp.Views;

public partial class AppShell : Shell
{
	public AppShell()
	{
		InitializeComponent();

        // Register your pages
        Routing.RegisterRoute("home", typeof(Homepage));

        Routing.RegisterRoute(nameof(ProductDetailPage), typeof(ProductDetailPage));
        Routing.RegisterRoute("Userlogin", typeof(Userlogin));
        Routing.RegisterRoute("Userregister", typeof(Userregister));

	}
   
	

}
