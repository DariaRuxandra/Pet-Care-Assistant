using Microsoft.EntityFrameworkCore;
using Microsoft.Maui.Storage;
using Pet_Care_Assistant.Data;
using System.IO;
using Microsoft.Extensions.Logging;
using Pet_Care_Assistant.Models;
using Pet_Care_Assistant.Services;
using Pet_Care_Assistant.ViewModels;
using Pet_Care_Assistant.Views;

namespace Pet_Care_Assistant
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                });

#if DEBUG
    		builder.Logging.AddDebug();
#endif
            // database file in app data directory
            var dbPath = Path.Combine(FileSystem.AppDataDirectory, "pets.db");
            builder.Services.AddDbContext<AppDbContext>(options =>
                options.UseSqlite($"Data Source={dbPath}"));

            builder.Services.AddSingleton<HomeViewModel>();
            builder.Services.AddSingleton<PetListPage>();
            builder.Services.AddSingleton<DogBreedService>();
            builder.Services.AddTransient<Views.PetFormPage>();
            builder.Services.AddTransient<ViewModels.PetFormViewModel>();
            builder.Services.AddSingleton<SqliteConnectionFactory>();

            var app = builder.Build();

            return app;
        }
    }
}
