namespace GeoLab_Proj.Utils
{
    public static class ServiceHelper
    {
        public static TService GetService<TService>() => Current.GetService<TService>();

        public static IServiceProvider Current => IPlatformApplication.Current.Services;
    }
}