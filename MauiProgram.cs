using CommunityToolkit.Maui;

namespace GeoLab_Proj;
public static class MauiProgram
{
	public static MauiApp CreateMauiApp()
	{
		var builder = MauiApp.CreateBuilder();
		builder
			.UseMauiApp<App>()
            .UseMauiCommunityToolkit()
            .ConfigureFonts(fonts =>
			{
				fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
				fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
			});


        builder.Services.AddSingleton<MainPage>();
        builder.Services.AddSingleton<ResultPage>();
        builder.Services.AddSingleton<MainVM>();

        return builder.Build();
	}
}
