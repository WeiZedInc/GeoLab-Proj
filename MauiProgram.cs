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

        Routing.RegisterRoute("ResultPage", typeof(ResultPage));

        builder.Services.AddSingleton<MainPage>();
        builder.Services.AddSingleton<MainVM>();
        builder.Services.AddTransient<ResultPage>();
        builder.Services.AddTransient<ResultVM>();

        return builder.Build();
	}
}
