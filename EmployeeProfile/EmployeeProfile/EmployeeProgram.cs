using Microsoft.Extensions.Logging;

using EmployeeProfile.DataAccess;
using EmployeeProfile.ViewModels;
using EmployeeProfile.Views;

namespace EmployeeProfile
{
    public static class EmployeeProgram

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

            var dbContext = new EmployeeDbContext();
            dbContext.Database.EnsureCreated();
            dbContext.Dispose();

            builder.Services.AddDbContext<EmployeeDbContext>();

            builder.Services.AddTransient<EmployeePage>();
            builder.Services.AddTransient<EmployeeViewModel>();

            builder.Services.AddTransient<MainPage>();
            builder.Services.AddTransient<MainViewModel>();

            Routing.RegisterRoute(nameof(EmployeePage), typeof(EmployeePage));
                

#if DEBUG
            builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}