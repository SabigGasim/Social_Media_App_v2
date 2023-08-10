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
        builder.Services.AddSingleton<ICommentsRepository, FakeCommentsRepository>();
        builder.Services.AddSingleton<IRepliesRepository, FakeRepliesRepository>();
        builder.Services.AddSingleton<IFollowRequestsRepository, FakeFollowRequestsRepository>();
        builder.Services.AddSingleton<IUserLookupRepository, FakeUserLookupRepository>();

        builder.Services.AddSingleton<IRouteParametersFactory, RouteParametersFactory>();
        builder.Services.AddSingleton<INavigationService, MauiNavigationService>();
        builder.Services.AddSingleton<INavigateCommandFactory, NavigateCommandFactory>();
        builder.Services.AddSingleton<ILookupHandler, LookupHandler>();
        
        builder.Services.AddTransient<PostViewModel>();
        builder.Services.AddTransient<CommentViewModel>();
        builder.Services.AddTransient<ReplyViewModel>();

        builder.Services.AddTransient<TimelineViewModel>();
        builder.Services.AddTransient<PostCommentsViewModel>();
        builder.Services.AddTransient<CommentRepliesViewModel>();
        builder.Services.AddTransient<FollowRequestViewModel>();
        builder.Services.AddSingleton<UserSearchViewModel>();

        builder.Services.AddSingleton<HomePage>();
        builder.Services.AddSingleton<SearchPage>();
        builder.Services.AddTransient<CommentsPage>();
        builder.Services.AddTransient<RepliesPage>();
        builder.Services.AddTransient<PostMediaViewer>();
        builder.Services.AddTransient<FollowRequestsPage>();

#if DEBUG
        builder.Logging.AddDebug();
#endif

        return builder.Build();
    }
}
