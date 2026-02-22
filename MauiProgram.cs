using Microsoft.Extensions.Logging;
using SkiaSharp.Views.Maui.Controls.Hosting;
using FreshFarmApp.Views;
using FreshFarmApp.Services;
using FreshFarmApp.ViewModels;
using FreshFarmApp.Data;
namespace FreshFarmApp;

public static class MauiProgram
{
	public static MauiApp CreateMauiApp()
	{
		var builder = MauiApp.CreateBuilder();
		builder
			.UseMauiApp<App>()
			.UseSkiaSharp()
            .RegisterAppServices()
            .RegisterViewModels()
			.ConfigureFonts(fonts =>
			{
				fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                fonts.AddFont("FontAwesomeSolid.otf", "AwesomeSolid");
                fonts.AddFont("latoblack.TTF", "black");
                fonts.AddFont("latobold.TTF", "bold");
                fonts.AddFont("latoitalic.TTF", "italic");
                fonts.AddFont("latoregular.TTF", "regular");
				fonts.AddFont("Material-Icon.ttf", "MaterialIcon");
			    fonts.AddFont("MaterialIcons-Regular.ttf", "Materialicons");
				
				});
             string dbPath = Path.Combine(FileSystem.AppDataDirectory, "freshfarm.db3");
             builder.Services.AddSingleton(new AppDatabase(dbPath));

#if DEBUG
		builder.Logging.AddDebug();
#endif

		return builder.Build();
	}
	
}
