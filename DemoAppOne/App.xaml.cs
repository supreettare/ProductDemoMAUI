using System.Diagnostics;

namespace DemoAppOne
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
            MainPage = new AppShell();
            CreateAppHandlers();
        }

        private void CreateAppHandlers()
        {
            Microsoft.Maui.Handlers.EntryHandler.Mapper.AppendToMapping(nameof(BorderLessEntry), (handler, view) =>
            {
#if ANDROID
                handler.PlatformView.SetBackgroundColor(Android.Graphics.Color.Transparent);
#elif IOS
                handler.PlatformView.BackgroundColor = UIKit.UIColor.Clear;
                handler.PlatformView.BorderStyle = UIKit.UITextBorderStyle.None;
#endif
            });
        }

        public static bool IsInternetConnected()
        {
            return (Connectivity.NetworkAccess == NetworkAccess.Internet) ? true : false;
        }

        public static async Task CheckInternetConnectivity()
        {
            if (!IsInternetConnected())
            {
                var result = await App.Current.MainPage.DisplayAlert(StringResources.BrandName, StringResources.NoInternetConnection, StringResources.Retry, StringResources.Exit);
                if (result)
                {
                    await CheckInternetConnectivity();
                }
                else
                {
                    Process.GetCurrentProcess().Kill();
                }
            }
        }
    }
}
