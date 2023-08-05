using Infrastructure.Interfaces;
using Microsoft.Extensions.Logging;
using NativeApp.Factories;
using NativeApp.Infrastructure.Data.Databases.InMemory;
using NativeApp.Infrastructure.Data.FileManagers;
using NativeApp.Infrastructure.Repositories;
using NativeApp.Interfaces;
using NativeApp.MVVM.Converters;
using NativeApp.MVVM.ViewModels;
using NativeApp.MVVM.Views;
using NativeApp.Services;
using NativeApp.Templates.DataTemplateSelectors;

namespace NativeApp;
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

        DependencyService.RegisterSingleton<IHumanizerService>(new HumanizerService());

        builder.Services.AddSingleton<IDatabase, FakeInMemoryDatabase>();
        builder.Services.AddSingleton<ITimelineRepository, FakeTimelineRepository>();
        builder.Services.AddSingleton<IRouteParametersFactory, RouteParametersFactory>();
        builder.Services.AddSingleton<INavigationService, MauiNavigationService>();
        builder.Services.AddTransient<PostViewModel>();
        builder.Services.AddTransient<TimelineViewModel>();
        builder.Services.AddSingleton<PostDataTemplateSelector>();

        builder.Services.AddSingleton<HomePage>();


#if DEBUG
        builder.Logging.AddDebug();
#endif

        return builder.Build();
    }
}
