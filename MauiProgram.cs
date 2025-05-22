using CommunityToolkit.Maui;
using CommunityToolkit.Maui.Markup;
using Gespraechsnotiz_App.ViewModels;
using Gespraechsnotiz_App.Views;
using MauiIcons.Material;
using Microsoft.Extensions.Logging;

namespace Gespraechsnotiz_App
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .UseMauiCommunityToolkitMarkup()
                .UseMaterialMauiIcons()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                }).UseMauiCommunityToolkit();

            builder.Services.AddTransient<MainPage>();
            builder.Services.AddTransient<MainViewModel>();
            builder.Services.AddTransient<NoteDetailPage>();
            builder.Services.AddTransient<NoteDetailViewModel>();
            builder.Services.AddTransient<NoteEditPage>();

#if DEBUG
            builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}
