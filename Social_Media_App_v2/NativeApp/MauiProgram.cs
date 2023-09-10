using CommunityToolkit.Maui;
using Infrastructure.Interfaces;
using Microsoft.Extensions.Logging;
using NativeApp.Factories;
using NativeApp.Infrastructure.Data.Databases.InMemory;
using NativeApp.Infrastructure.Repositories;
using NativeApp.Interfaces;
using NativeApp.MVVM.ViewModels;
using NativeApp.MVVM.ViewModels.Settings.AccountInfo;
using NativeApp.MVVM.ViewModels.Settings.MutedAndBlocked;
using NativeApp.MVVM.ViewModels.Settings.Notifications;
using NativeApp.MVVM.ViewModels.Settings.PrivacyAndSecurity;
using NativeApp.MVVM.Views;
using NativeApp.MVVM.Views.Settings;
using NativeApp.MVVM.Views.Settings.AccountInformation;
using NativeApp.MVVM.Views.Settings.MutedAndBlocked;
using NativeApp.MVVM.Views.Settings.Notifications;
using NativeApp.Services;

namespace NativeApp;
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

        DependencyService.RegisterSingleton<IHumanizerService>(new HumanizerService());

        builder.Services.AddSingleton<IDatabase, FakeInMemoryDatabase>();
        builder.Services.AddSingleton<ITimelineRepository, FakeTimelineRepository>();
        builder.Services.AddSingleton<ICommentsRepository, FakeCommentsRepository>();
        builder.Services.AddSingleton<IRepliesRepository, FakeRepliesRepository>();
        builder.Services.AddSingleton<IPostRepository, FakePostsRepository>();
        builder.Services.AddSingleton<IFollowRequestsRepository, FakeFollowRequestsRepository>();
        builder.Services.AddSingleton<IUserLookupRepository, FakeUserLookupRepository>();
        builder.Services.AddSingleton<IAlertsRepository, FakeAlertsRepository>();

        builder.Services.AddSingleton<IRouteParametersFactory, RouteParametersFactory>();
        builder.Services.AddSingleton<INavigationService, MauiNavigationService>();
        builder.Services.AddSingleton<INavigateCommandFactory, NavigateCommandFactory>();
        builder.Services.AddSingleton<ILookupHandler, LookupHandler>();
        
        builder.Services.AddTransient<PostViewModel>();
        builder.Services.AddTransient<CommentViewModel>();
        builder.Services.AddTransient<ReplyViewModel>();
        builder.Services.AddTransient<UserAlertViewModel>();
        builder.Services.AddTransient<AccountAlertViewModel>();

        builder.Services.AddTransient<TimelineViewModel>();
        builder.Services.AddTransient<PostCommentsViewModel>();
        builder.Services.AddTransient<CommentRepliesViewModel>();
        builder.Services.AddTransient<FollowRequestViewModel>();
        builder.Services.AddTransient<ProfileViewModel>();
        builder.Services.AddTransient<UserViewModel>();
        builder.Services.AddSingleton<UserSearchViewModel>();
        builder.Services.AddSingleton<AlertsViewModel>();
        builder.Services.AddSingleton<ShellViewModel>();

        builder.Services.AddSingleton<AccountInfoViewModel>();
        builder.Services.AddSingleton<NotificationSettingsViewModel>();
        builder.Services.AddSingleton<MutedAndBlockedViewModel>();
        builder.Services.AddSingleton<MutedAccountsViewModel>();
        builder.Services.AddSingleton<BlockedAccountsViewModel>();
        builder.Services.AddSingleton<PrivacyAndSecurityViewModel>();
        builder.Services.AddSingleton<SettingsViewModel>();
        builder.Services.AddSingleton<ChangeUsernameViewModel>();
        builder.Services.AddSingleton<ChangeNicknameViewModel>();
        builder.Services.AddSingleton<ChangeDateOfBirthViewModel>();
        builder.Services.AddSingleton<NotificationFiltersViewModel>();
        builder.Services.AddSingleton<NotificationMethodsViewModel>();
        builder.Services.AddSingleton<MutedWordsViewModel>();
        builder.Services.AddSingleton<AddMutedWordViewModel>();

        builder.Services.AddSingleton<HomePage>();
        builder.Services.AddSingleton<SearchPage>();
        builder.Services.AddSingleton<AlertsPage>();
        builder.Services.AddTransient<CommentsPage>();
        builder.Services.AddTransient<RepliesPage>();
        builder.Services.AddTransient<PostMediaViewer>();
        builder.Services.AddTransient<FollowRequestsPage>();
        builder.Services.AddTransient<ProfilePage>();
        builder.Services.AddSingleton<SettingsPage>();
        builder.Services.AddSingleton<AccountInfoPage>();
        builder.Services.AddSingleton<ChangeUsernamePage>();
        builder.Services.AddSingleton<ChangeNicknamePage>();
        builder.Services.AddSingleton<ChangeDateOfBirthPage>();
        builder.Services.AddSingleton<NotificationFiltersPage>();
        builder.Services.AddSingleton<NotificationMethodsPage>();
        builder.Services.AddSingleton<MutedAccountsPage>();
        builder.Services.AddSingleton<BlockedAccountsPage>();
        builder.Services.AddSingleton<MutedWordsPage>();
        builder.Services.AddSingleton<AddMutedWordPage>();


        builder.Services.AddSingleton<App>();

#if DEBUG
        builder.Logging.AddDebug();
#endif

        return builder.Build();
    }
}
