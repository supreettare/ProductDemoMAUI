namespace DemoAppOne
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .UseMauiCommunityToolkit()
                .RegisterAppServices()
                .RegisterViews()
                .RegisterViewModels()
                .RegisterRouting()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                });
            
#if DEBUG
            builder.Logging.AddDebug();
#endif
            return builder.Build();
        }

        public static MauiAppBuilder RegisterAppServices(this MauiAppBuilder builder)
        {
            //Register services
            builder.Services.AddTransient<IImageService, ImageService>();
            builder.Services.AddTransient<INavigationService, NavigationService>();
            builder.Services.AddSingleton<IDemoAppOneDatabase, DemoAppOneDatabase>();
            return builder;
        }

        public static MauiAppBuilder RegisterViews(this MauiAppBuilder builder)
        {
            builder.Services.AddSingleton<ProductListView>();
            builder.Services.AddSingleton<AddOrEditProductView>();
            builder.Services.AddSingleton<RemoteProductsView>();
            return builder;
        }

        public static MauiAppBuilder RegisterViewModels(this MauiAppBuilder builder)
        {
            builder.Services.AddTransient<ProductListViewModel>();
            builder.Services.AddTransient<AddOrEditProductViewModel>();
            builder.Services.AddTransient<RemoteProductsViewModel>();
            return builder;
        }

        public static MauiAppBuilder RegisterRouting(this MauiAppBuilder builder)
        {
            Routing.RegisterRoute(nameof(ProductListView), typeof(ProductListView));
            Routing.RegisterRoute(nameof(AddOrEditProductView), typeof(AddOrEditProductView));
            Routing.RegisterRoute(nameof(RemoteProductsView), typeof(RemoteProductsView));

            return builder;
        }
    }
}
