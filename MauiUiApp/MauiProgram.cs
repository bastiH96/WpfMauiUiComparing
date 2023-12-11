using CommunityToolkit.Maui;
using MauiUiApp.View;
using MauiUiApp.View.PopUp;
using MauiUiApp.HelperClasses;
using Microsoft.Extensions.Logging;
using SQLite;
using WpfMauiLibrary.Services;
using MauiUiApp.ViewModel;

namespace MauiUiApp
{
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

                    fonts.AddFont("Brands-Regular-400.otf", "FAB");
                    fonts.AddFont("Free-Regular-400.otf", "FAR");
                    fonts.AddFont("Free-Solid-900.otf", "FAS");
                });
            if (!Directory.Exists(Constants.DirectoryPath)) {
                Directory.CreateDirectory(Constants.DirectoryPath);
            }

            new CategoryDataAccess(Constants.DbFullPath).CreateTable();
            new ToDoTaskDataAccess(Constants.DbFullPath).CreateTable();

            builder.Services.AddTransient<AllTasksView>();
            builder.Services.AddTransient<AllTasksViewModel>();
            builder.Services.AddTransient<SearchResultView>();
#if DEBUG
    		builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}
