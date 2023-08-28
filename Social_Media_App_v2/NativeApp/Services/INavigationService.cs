using CommunityToolkit.Maui.Views;
using NativeApp.Interfaces;

namespace NativeApp.Services;

public class MauiNavigationService : INavigationService
{
    public Task NavigateToAsync(string route, IDictionary<string, object> routeParameters = null) =>
        routeParameters is not null
            ? Shell.Current.GoToAsync(route, routeParameters)
            : Shell.Current.GoToAsync(route);

    public Task PopAsync(string route = "..") => Shell.Current.GoToAsync(route);

    public Task<object?> ShowPopupAsync<TPopup>(TPopup popup) where TPopup : Popup
    {
        return Shell.Current.ShowPopupAsync(popup);
    }
}
